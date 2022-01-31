using DigiMarketWebApp.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DigiMarketWebApp.Models
{
    public class UserAccess
    {
        [Key]
        public int UserAccessID { get; set; }

        [Required]
        [StringLength(250)]
        public string UserEmail { get; set; }


        // Navigation Properties
        public int PhotoID { get; set; }
        public Photo Photo { get; set; }

        public string Id { get; set; }
        public WebAppUser WebAppUser { get; set; }
    }
}
