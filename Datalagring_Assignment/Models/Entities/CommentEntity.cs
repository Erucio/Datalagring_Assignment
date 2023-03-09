using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datalagring_Assignment.Models.Entities
{
    internal class CommentEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string Comment { get; set; } = null!;
        public DateTime CommentDateTime { get; set; }

    }
}
