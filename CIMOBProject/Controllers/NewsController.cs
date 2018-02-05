using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CIMOBProject.Data;
using CIMOBProject.Models;
using System.Security.Claims;

namespace CIMOBProject.Controllers
{
    /// <summary>
    /// This controller is responsible for all the actions related to news.
    /// </summary>
    public class NewsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private const int RECENT_NEWS = 3;

        /// <summary>
        /// Initializes controller with the pretended context.
        /// </summary>
        /// <param name="context"></param>
        public NewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Indexes all the published news. If a new isn't published it will not be present.
        /// </summary>
        /// <returns>View with the published news.</returns>
        public async Task<IActionResult> Index()
        {
            var news = _context.News;
            if (User.IsInRole("Employee"))
            {
                return View(await news.ToListAsync());
            }
            var publishedNews = news.Where(n => n.IsPublished == true);
            return View(await publishedNews.ToListAsync());
        }

        /// <summary>
        /// Displays in a cleaner way the recent news. 
        /// By default will display the last 3 news published.
        /// This can be changed with the constant <c>RECENT_NEWS</c>.
        /// </summary>
        /// <returns>View with the recent news.</returns>
        public async Task<IActionResult> RecentNews()
        {
            var publishedNews = _context.News.Where(n => n.IsPublished == true)
                                .Include(n => n.Document)
                                .Include(n => n.Employee)
                                .OrderByDescending(n => n.Id).Take(RECENT_NEWS);
            return View(await publishedNews.ToListAsync());
        }

        /// <summary>
        /// This action publishes a new, enabling it to be visible to the users.
        /// </summary>
        /// <param name="id">Id of the new to be published.</param>
        /// <returns>Index view.</returns>
        public async Task<IActionResult> Publish(int id)
        {
            var news = _context.News.Where(n => n.Id == id).SingleOrDefault();
            if (id != news.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    news.IsPublished = true;
                    _context.Update(news);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "News");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsExists(news.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            }
            return RedirectToAction("Index", "News");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var news = await _context.News.Include(e => e.Employee).Include(d => d.Document)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (news == null)
            {
                return NotFound();
            }
            return View(news);
        }

        /// <summary>
        /// Actions that leads to the News creation view.
        /// The news can take an optional url to a document the employee wishes to attach to the news.
        /// It's up to the employee to validate such url.
        /// </summary>
        /// <param name="userId">Logged in employee id.</param>
        /// <returns>News creation view.</returns>
        public IActionResult Create(string userId)
        {
            ViewData["EmployeeId"] = userId;
            loadHelp();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmployeeId,Title,TextContent,IsPublished,DocumentId")] News news, string link)
        {
            if (ModelState.IsValid)
            {
                if (!String.IsNullOrEmpty(link))
                {
                    news.Document = CreateAndValidateDocument(news, link);
                }
                //news.DocumentId = doc.DocumentId;
                _context.Add(news);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(news);
        }

        /// <summary>
        /// Validates if a document with a link already exists in the database, if it exists will obtain that document and return it.
        /// Otherwise it will create a new document and return the new one.
        /// </summary>
        /// <param name="news">Current news.</param>
        /// <param name="link">Link to validate.</param>
        /// <returns>Valid document.</returns>
        private Document CreateAndValidateDocument(News news, string link)
        {
            Document urlDoc = _context.Documents
                .Where(d => d.FileUrl.Equals(link)).FirstOrDefault();
            if (urlDoc == null)
            {
                urlDoc = new Document
                {
                    EmployeeId = news.EmployeeId,
                    Description = "Documento de " + news.Title,
                    FileUrl = link,
                    UploadDate = DateTime.Now
                };
                _context.Add(urlDoc);
                _context.SaveChanges();
            }
            return urlDoc;
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var news = await _context.News.Include(n => n.Employee)
                .Include(n => n.Document).SingleOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }
            loadHelp();
            return View(news);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId,Title,TextContent,IsPublished,DocumentId")] News news, string link)
        {
            if (id != news.Id)
            {
                return NotFound();
            }
            var newsToUpdate = await _context.News.Include(n => n.Document)
                .Include(n => n.Employee).SingleOrDefaultAsync(n => n.Id == id);
            newsToUpdate.Title = news.Title;
            //If it's desired to change employee id to the one updating it
            //newsToUpdate.EmployeeId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Document editedDoc = CreateAndValidateDocument(news, link);
            editedDoc.EmployeeId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            newsToUpdate.Document = editedDoc;
            newsToUpdate.TextContent = news.TextContent;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(newsToUpdate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsExists(newsToUpdate.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(newsToUpdate);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .SingleOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var news = await _context.News.SingleOrDefaultAsync(m => m.Id == id);
            _context.News.Remove(news);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private void loadHelp()
        {
            ViewData["TitleTip"] = (_context.Helps.FirstOrDefault(h => h.HelpName == "Title") as Help).HelpDescription;
            ViewData["TextContentTip"] = (_context.Helps.FirstOrDefault(h => h.HelpName == "TextContent") as Help).HelpDescription;
            ViewData["DocumentTip"] = (_context.Helps.FirstOrDefault(h => h.HelpName == "FileURL") as Help).HelpDescription;
        }

        private bool NewsExists(int id)
        {
            return _context.News.Any(e => e.Id == id);
        }
    }
}
