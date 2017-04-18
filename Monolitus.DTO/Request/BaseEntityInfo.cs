using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolitus.DTO.Request
{
    public abstract class BaseEntityInfo
    {
        public string Id { get; set; }
        
        public bool IsDeleted { get; set; }
        public DateTime InsertDate { get; set; }

    }

}