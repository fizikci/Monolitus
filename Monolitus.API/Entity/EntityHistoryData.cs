namespace Monolitus.API.Entity.Common
{
    public class EntityHistoryData : BaseEntity
    {
        public string EntityHistoryId { get; set; }
        public string Changes { get; set; }

        public EntityHistory GetEntityHistory() { return Provider.ReadEntityWithRequestCache<EntityHistory>(EntityHistoryId); }

    }
}