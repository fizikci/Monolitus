using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolitus.DTO.Request
{
    public class FolderInfo : NamedEntityInfo
    {
        public string UserId { get; set; }
        public List<ShelfInfo> Shelves { get; set; }

    }
}
