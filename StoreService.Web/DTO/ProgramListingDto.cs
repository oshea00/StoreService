using System.ComponentModel.DataAnnotations;

namespace StoreService.Web.DTO
{
    public class ProgramListingDto
    {
        public int ProgramListingId { get; set; }
        [MaxLength]
        public string ProgramText { get; set; }
        public int ProgramBookId { get; set; }
    }
}