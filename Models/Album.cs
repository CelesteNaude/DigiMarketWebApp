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
        public int PhotoID { get; set; }
        public Photo Photo { get; set; }

        public string Id { get; set; }
        public WebAppUser WebAppUser { get; set; }
    }
}
