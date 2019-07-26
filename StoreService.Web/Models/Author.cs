using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreService.Web.Models
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }
        [MaxLength(60)]
        [Required]
        public string AuthorName { get; set; }
        [MaxLength(60)]
        [Required]
        public string Email { get; set; }

        // Relationships
        public virtual ICollection<ProgramBook> ProgramBooks { get; set; }
    }
}
