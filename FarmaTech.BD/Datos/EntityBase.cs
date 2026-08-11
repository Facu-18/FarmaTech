using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmaTech.BD.Datos
{
    public class EntityBase: IEntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }

    public interface IEntityBase
    {
        public int Id { get; set; }
    }
}
