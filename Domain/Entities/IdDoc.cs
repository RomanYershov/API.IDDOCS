using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class IdDoc : IBaseEntity
    {
        [Key]
        public Guid Number { get; set; }    
        public DocType Type { get; set; }
        public String Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedUserId { get; set; }
        public Guid ReceiverUserId { get; set; }
        public String Content { get; set; } 
    }
}
