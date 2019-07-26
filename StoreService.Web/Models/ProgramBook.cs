using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StoreService.Web.Models
{
    public class ProgramBook
    {
        [Key]
        public int ProgramBookId { get; set; }
        public int AuthorId { get; set; }
        [MaxLength(100)]
        public string BookTitle { get; set; }

        // Relationships
        public Topic Topic { get; set; }
        public virtual ICollection<ProgramListing> ProgramListings { get; set; }
    }
}
