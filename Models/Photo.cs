using DigiMarketWebApp.Areas.Identity.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DigiMarketWebApp.Models
{
    public class Photo
    {
        [Key]
        public int PhotoID { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Title { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string ImageName { get; set; }

        [NotMapped]
        [DisplayName("Upload Photo")]
        public IFormFile ImageFile { get; set; }

        // Navigation Properties
        public string Id { get; set; }
        public WebAppUser WebAppUser { get; set; }
        public List<Metadata> Metadatas { get; set; }
        public List<Album> Albums { get; set; }
        public List<UserAccess> UserAccesses { get; set; }
    }
}
