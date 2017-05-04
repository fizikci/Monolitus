using Cinar.Database;
using Monolitus.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Monolitus.API.Entity
{
    public class Bookmark : NamedEntity
    {
        [ColumnDetail(Length = 12)]
        public string UserId { get; set; }

        [ColumnDetail(Length = 12)]
        public string FolderId { get; set; }

        [ColumnDetail(Length = 12)]
        public string ShelfId { get; set; }

        [ColumnDetail(Length = 200)]
        public string Url { get; set; }

        [ColumnDetail(Length = 200)]
        public string Title { get; set; }

        [ColumnDetail(Length = 200)]
        public string Picture { get; set; }

        [ColumnDetail(Length = 500)]
        public string Description { get; set; }

        public override void Delete()
        {
            Provider.Database.ExecuteNonQuery("delete from Bookmark where Id = {0}", Id);
        }

    }

}
