using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace MiApiWeb.Models
{
    public class huesped
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
     
        public DateTime fechaIngreso { get; set; }    
        public string nombre { get; set; }
        public string tipoIdentificacion { get; set; }
        public string numDocumento { get; set; }
        public string nacionalidad { get; set; }
        public int numHabitacion { get; set; }
        public int cantPersonas { get; set; }



    }
}
