using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datalagring_Assignment.Models.Entities
{
    internal class CommentEntity
    {
        //[Key] gör att följande rad kod blir primär nyckel i kodblocket
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string Comment { get; set; } = null!;

        [Required]
        public DateTime CommentDateTime { get; set; }

        [Required]
        public int ErrandId { get; set; }
        public ErrandEntity Errand { get; set; } = null!;
    }
}
