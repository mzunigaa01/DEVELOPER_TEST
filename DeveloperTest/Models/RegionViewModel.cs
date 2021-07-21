using System.ComponentModel.DataAnnotations;

namespace DeveloperTest.Models
{
    public class RegionViewModel
    {
        [Key]
        [Display(Name = "Iso")]
        public string Iso { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}