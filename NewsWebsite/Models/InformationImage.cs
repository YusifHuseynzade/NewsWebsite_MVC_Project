using System.ComponentModel.DataAnnotations;

namespace NewsWebsite.Models
{
    public class InformationImage
    {
        public int Id { get; set; }
        [MaxLength(75)]
        public string Image { get; set; }
        public int InformationId { get; set; }
        public bool? PosterStatus { get; set; }
        public Information Informations { get; set; }
    }
}
