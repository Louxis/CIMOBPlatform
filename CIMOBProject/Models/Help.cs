using System.ComponentModel.DataAnnotations;

namespace CIMOBProject.Models
{
    ///<summary>
    ///This class represents the helps that will be displayed to the users in case that they need some help with certain activities.
    /// </summary>
    public class Help
    {
        public int Id { get; set; }

        [Required]
        public string HelpName { get; set; }

        [Required]
        public string HelpDescription { get; set; }

        

    }
}
