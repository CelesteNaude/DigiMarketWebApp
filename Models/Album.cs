using DigiMarketWebApp.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DigiMarketWebApp.Models
{
    public class Album
    {
        [Key]
        public int AlbumID { get; set; }
        [DisplayFormat(NullDisplayText = "User Album")]
        public string AlbumName { get; set; }

        // Navigation Properties
        public int PictureID { get; set; }
        public Picture Picture { get; set; }

        public string IdentityUserID { get; set; }
        public IdentityUser IdentityUser { get; set; }
    }
}
