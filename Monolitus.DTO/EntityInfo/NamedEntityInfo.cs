using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolitus.DTO.EntityInfo
{
    public class NamedEntityInfo : BaseEntityInfo
    {
        public string Name { get; set; }

        public int OrderNo { get; set; }
    }
}
