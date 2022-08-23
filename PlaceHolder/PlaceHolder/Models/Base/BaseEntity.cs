using System.ComponentModel.DataAnnotations.Schema;

namespace PlaceHolder.Models.Base
{
    public class BaseEntity
    {
        [JsonPropertyOrder(1)]
        [Column("id")]
        public virtual long Id { get; set; }
    }
}
