using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using DigiMarketWebApp.Models;
using Microsoft.AspNetCore.Identity;

namespace DigiMarketWebApp.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the WebAppUser class
    public class WebAppUser : IdentityUser
    {
        [Key]
        public string Id { get; set; }
        [PersonalData]
        [Column("First Name", TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }

        [PersonalData]
        [Column("Last Name", TypeName = "nvarchar(100)")]
        public string LastName { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Position { get; set; }

        // Navigation Properties
        public List<Album> Albums { get; set; }
        public List<Photo> Photos { get; set; }
        public List<UserAccess> UserAccesses { get; set; }
    }
}
