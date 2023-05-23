using NewsWebsite.Attributes.ValidationAttributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsWebsite.Models
{
    public class Information
    {
        public int CategoryId { get; set; }
        public int AuthorId { get; set; }
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow.AddHours(4);
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(150)]
        public string? Title { get; set; }
        [MaxLength(250)]
        public string? Description { get; set; }
        public bool IsTravel { get; set; }
        public bool IsHealth { get; set; }
        public bool IsPolitic { get; set; }
        public bool IsTechnology { get; set; }
        public bool IsFood { get; set; }
        public bool IsArchitecture { get; set; }
        [NotMapped]
        [MaxFileSize(2)]
        [AllowedFileTypes("image/jpeg", "image/png")]
        public IFormFile? PosterFile { get; set; }
        [NotMapped]
        [MaxFileSize(2)]
        public List<IFormFile>? ImageFiles { get; set; } = new List<IFormFile>();
        [NotMapped]
        public List<int>? InformationImageIds { get; set; } = new List<int>();
        public Category? Categories { get; set; }
        public Author? Authors { get; set; }
        public List<InformationImage>? InformationImages { get; set; }
    }
}
