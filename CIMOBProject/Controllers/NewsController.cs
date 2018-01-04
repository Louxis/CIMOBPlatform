using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CIMOBProject.Data;
using CIMOBProject.Models;
using System.Security.Claims;

namespace CIMOBProject.Controllers
{
    public class NewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: News
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

        // GET: News/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News.Include(e => e.Employee).Include(d =>d.Document)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // GET: News/Create
        public IActionResult Create(string userId)
        {
            ViewData["EmployeeId"] = userId;
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmployeeId,Title,TextContent,IsPublished,DocumentId")] News news, string link)
        {
            if (ModelState.IsValid)
            {
                if (!String.IsNullOrEmpty(link))
                {
                    news.Document = createAndValidateDocument(news,link);
                }
                    //news.DocumentId = doc.DocumentId;
                    _context.Add(news);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
            }
            return View(news);
        }

        private Document createAndValidateDocument (News news, string link) 
        {
            Document urlDoc = _context.Documents.Where(d => d.FileUrl.Equals(link)).FirstOrDefault();
            if(urlDoc == null) 
            {
                urlDoc = new Document {
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

        // GET: News/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var news = await _context.News.Include(n => n.Employee).Include(n => n.Document).SingleOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }
            return View(news);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId,Title,TextContent,IsPublished,DocumentId")] News news, string link)
        {
            if (id != news.Id)
            {
                return NotFound();
            }
            var newsToUpdate = await _context.News.Include(n => n.Document).Include(n => n.Employee).SingleOrDefaultAsync(n => n.Id == id);
            newsToUpdate.Title = news.Title;
            //If it's desired to change employee id to the one updating it
            //newsToUpdate.EmployeeId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Document editedDoc = createAndValidateDocument(news, link);
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

        // GET: News/Delete/5
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

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var news = await _context.News.SingleOrDefaultAsync(m => m.Id == id);
            _context.News.Remove(news);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsExists(int id)
        {
            return _context.News.Any(e => e.Id == id);
        }
    }
}
