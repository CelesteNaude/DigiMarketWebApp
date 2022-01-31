using DigiMarketWebApp.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DigiMarketWebApp.Models
{
    public class SharedAlbum
    {
        [Key]
        public int SharedAlbumID { get; set; }

        [Required]
        [StringLength(250)]
        public string UserEmail { get; set; }

        // Navigation Properties
        public int AlbumNameID { get; set; }
        public AlbumName AlbumName { get; set; }

        public string Id { get; set; }
        public WebAppUser WebAppUser { get; set; }
    }
}
