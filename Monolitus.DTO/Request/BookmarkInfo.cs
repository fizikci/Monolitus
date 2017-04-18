using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolitus.DTO.Request
{
    public class BookmarkInfo : NamedEntityInfo
    {
        public string UserId { get; set; }

        public string FolderId { get; set; }

        public string ShelfId { get; set; }

        public string Url { get; set; }

        public string Title { get; set; }

        public string Picture { get; set; }

        public string Description { get; set; }
    }
}
