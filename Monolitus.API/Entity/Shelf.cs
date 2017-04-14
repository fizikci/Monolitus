using Cinar.Database;
using Monolitus.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Monolitus.API.Entity
{
    public class Shelf : NamedEntity
    {
        [ColumnDetail(Length =12)]
        public string FolderId { get; set; }
    }

}
