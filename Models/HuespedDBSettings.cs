using MongoDB.Driver.Core.Configuration;

namespace MiApiWeb.Models
{
    public class HuespedDBSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string CollectionName { get; set; } = null!;
    }
}
