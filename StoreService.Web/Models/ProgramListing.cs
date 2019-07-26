using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreService.Web.Models
{
    public class ProgramListing
    {
        public int ProgramListingId { get; set; }
        [MaxLength]
        public string ProgramText { get; set;  }
        public int ProgramBookId { get; set; }
    }
}
