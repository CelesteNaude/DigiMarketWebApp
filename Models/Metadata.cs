using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DigiMarketWebApp.Models
{
    public class Metadata
    {
        [Key]
        public int MetadataID { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string Location { get; set; }
        public string Tag { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        [DisplayFormat(NullDisplayText = "No owner yet")]
        public string Owner { get; set; }

        //Navigation Properties
        public int PhotoID { get; set; }
        public Photo Photo { get; set; }
    }
}
