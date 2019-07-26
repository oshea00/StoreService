using System.ComponentModel.DataAnnotations;

namespace StoreService.Web.DTO
{
    public class TopicDto
    {
        public int TopicId { get; set; }
        [MaxLength(100)]
        public string TopicName { get; set; }
    }
}