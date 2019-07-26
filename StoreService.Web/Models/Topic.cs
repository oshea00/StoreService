using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreService.Web.Models
{
    public class Topic
    {
        [Key]
        public int TopicId { get; set; }
        [MaxLength(100)]
        public string TopicName { get; set; }
    }
}
