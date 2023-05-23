using NewsWebsite.Models;

namespace NewsWebsite.ViewModels
{
    public class NewsDetailViewModel
    {
        public Information Information { get; set; }
        public List<Author> Authors { get; set; }
        public List<Category> Categories { get; set; }
        public List<Information> Informations { get; set; }
        public List<Information> RelatedInformations { get; set; }
       
    }
}
