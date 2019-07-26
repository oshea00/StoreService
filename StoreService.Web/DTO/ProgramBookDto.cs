using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StoreService.Web.DTO
{
    public class ProgramBookDto
    {
        public int ProgramBookId { get; set; }
        public int AuthorId { get; set; }
        [MaxLength(100)]
        public string BookTitle { get; set; }

        // Relationships
        public TopicDto Topic { get; set; }
        public virtual ICollection<ProgramListingDto> ProgramListings { get; set; }

    }
}