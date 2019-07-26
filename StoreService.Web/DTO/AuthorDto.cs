using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreService.Web.DTO
{
    public class AuthorDto
    {
        public int AuthorId { get; set; }
        [MaxLength(60)]
        [Required]
        public string AuthorName { get; set; }
        [MaxLength(60)]
        [Required]
        public string Email { get; set; }

        // Relationships
        public ICollection<ProgramBookDto> ProgramBooks { get; set; }

    }
}
