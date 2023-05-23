using NewsWebsite.Models;
using System.Collections.Generic;
using System.Xml.Linq;

namespace NewsWebsite.ViewModels
{
    public class HomeViewModel
    {
        public List<Information> Travels { get; set; }
        public List<Information> Healths { get; set; }
        public List<Information> Politics { get; set; }
        public List<Information> Technologies { get; set; }
        public List<Information> Foods { get; set; }
        public List<Information> Architectures { get; set; }
        public List<Information> Informations { get; set; }
        public List<AppUser> AppUsers { get; set; }
        public List<InformationImage> InformationImages { get; set; }
    }
}
