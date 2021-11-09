using DigiMarketWebApp.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DigiMarketWebApp.Models
{
    public class Picture
    {
        [Key]
        public int PictureID { get; set; }
        public string Title { get; set; }

        // Navigation Properties
        public string IdentityUserID { get; set; }
        public IdentityUser IdentityUser { get; set; }
        public List<Metadata> Metadatas { get; set; }
        public List<Album> Albums { get; set; }
        public List<UserAccess> UserAccesses { get; set; }
    }
}
