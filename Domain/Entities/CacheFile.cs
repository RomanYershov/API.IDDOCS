using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class CacheFile : IBaseEntity
    {
        [Key]
        public Guid ID { get; set; }
        public String FileName { get; set; }
        public byte[] Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
