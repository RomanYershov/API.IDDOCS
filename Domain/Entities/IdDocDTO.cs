using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class IdDocDTO : IBaseEntity
    {
        public Guid ID { get; set; }
        public DocType Type { get; set; }
        public String Name { get; set; }
        public Guid ReceiverUserId { get; set; }
        public String Content { get; set; }
    }
}
