

using Datalagring_Assignment.Models.Entities;

namespace Datalagring_Assignment.Models
{
    internal class Errand
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public DateTime ErrandDateTime { get; set; }
        public string CustomerName { get; set; } = null!;
        public string? Phone { get; set; }

        public string Email { get; set; } = null!;

        public ErrandStatus Status { get; set; }
        public ICollection<CommentEntity> Comments { get; set; } = new List<CommentEntity>();

        public Errand()
        {
            Status = ErrandStatus.NotStarted;
        }

    }
}
