using DigiMarketWebApp.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DigiMarketWebApp.Models
{
    public class UserAccess
    {
        [Key]
        public int UserAccessID { get; set; }
        [Required]
        public string UserEmail { get; set; }


        // Navigation Properties
        public int PictureID { get; set; }
        public Picture Picture { get; set; }

        public int UserID { get; set; }
        public WebAppUser WebAppUser { get; set; }
    }
}
