using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cinar.Database;

namespace Monolitus.API.Entity
{
    public class NamedEntity : BaseEntity
    {
        public string Name { get; set; }
        public int OrderNo { get; set; }


        public override string ToString()
        {
            return Name;
        }
    }
}