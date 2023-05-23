using NewsWebsite.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NewsWebsite.Models
{
    public class Author
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(35)]
        public string FullName { get; set; }
        public List<Information>? Informations { get; set; }
    }
}
