using Cinar.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Monolitus.API.Entity
{
    public class ViewMainEntities : BaseEntity
    {
        public string EntityName {get; set;}
        public string EntityId {get; set;}
        public string Name { get; set; }
    }
}
