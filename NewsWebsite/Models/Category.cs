using System.ComponentModel.DataAnnotations;

namespace NewsWebsite.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public List<Information>? Informations { get; set; }
    }
}
