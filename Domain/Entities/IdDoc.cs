using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class IdDoc : IBaseEntity
    {   
        public CacheFile Content { get; set; } 
    }
}
