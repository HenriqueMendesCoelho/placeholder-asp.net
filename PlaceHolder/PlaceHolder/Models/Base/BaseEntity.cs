using System.ComponentModel.DataAnnotations.Schema;

namespace PlaceHolder.Models.Base
{
    public class BaseEntity
    {
        [Column("id")]
        public virtual long Id { get; set; }
    }
}
