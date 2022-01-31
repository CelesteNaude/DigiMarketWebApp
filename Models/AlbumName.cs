using DigiMarketWebApp.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DigiMarketWebApp.Models
{
    public class AlbumName
    {
        [Key]
        public int AlbumNameID { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }

        //Navigation Properties
        public string Id { get; set; }
        public WebAppUser WebAppUser { get; set; }

        public List<Album> Albums { get; set; }
        public List<SharedAlbum> SharedAlbums { get; set; }
    }
}
