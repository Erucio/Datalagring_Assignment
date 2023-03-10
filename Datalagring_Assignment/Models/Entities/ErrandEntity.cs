using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datalagring_Assignment.Models.Entities
{
    internal class ErrandEntity
    {
        //[Key] gör att följande rad kod blir primär nyckel i kodblocket
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string Description { get; set; } = null!;

        public DateTime ErrandDateTime { get; set; }

        public Guid CustomerId { get; set; }
        public CustomerEntity Customer { get; set; } = null!;

        public int Status { get; set; }

        public ICollection<CommentEntity> Comments { get; set; } = new List<CommentEntity>();
    }
}
