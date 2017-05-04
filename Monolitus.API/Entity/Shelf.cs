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

        public override void Delete()
        {
            Provider.Database.ExecuteNonQuery("delete from Shelf where Id = {0}", Id);
            Provider.Database.ExecuteNonQuery("delete from Bookmark where ShelfId = {0}", Id);
        }
    }

}
