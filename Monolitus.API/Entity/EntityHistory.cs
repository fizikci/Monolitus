using Cinar.Database;
using System;
using Monolitus.API.Entity;
using Monolitus.DTO.Enums;

namespace Monolitus.API.Entity
{
    public class EntityHistory : BaseEntity
    {
        [ColumnDetail(ColumnType = DbType.VarChar, Length = 10)]
        public EntityOperation Operation { get; set; }

        public string EntityName { get; set; }
        public string EntityId { get; set; }
        public string UserId { get; set; }

        public User GetUser() { return Provider.ReadEntityWithRequestCache<User>(UserId); }
    }
}
