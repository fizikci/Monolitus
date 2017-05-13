using Cinar.Database;
using Monolitus.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Monolitus.API.Entity
{
    public class Folder : NamedEntity
    {
        [ColumnDetail(Length =12)]
        public string UserId { get; set; }

        public override void Delete()
        {
            Provider.Database.ExecuteNonQuery("delete from Folder where Id = {0}", Id);
        }
    }

    public class ListViewFolder : Folder
    {
        public string UserName { get; set; }
        public string Html { get; set; }

    }
}
