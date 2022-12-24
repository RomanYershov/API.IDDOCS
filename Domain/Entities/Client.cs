using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Client : IBaseEntity
    {
        public Guid ID { get; set; }
        public String IIN { get; set; } 
        public DateTime CreatedAt { get; set; }
        public String Password { get; set; } //ToDo Hash
    }
}
