using System.Xml.Serialization;
using Cinar.Database;
//using System.IO;

namespace Monolitus.API.Entity
{
    public class Log : BaseEntity
    {
        /// <summary>
        /// Error, Notice
        /// </summary>
        public string LogType { get; set; }

        public string Category { get; set; }

        [ColumnDetail(IsNotNull = true, ColumnType = DbType.Text)]
        public string Description { get; set; }

        public string EntityName { get; set; }
        public int EntityId { get; set; }
    }
}
