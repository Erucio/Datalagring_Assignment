using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datalagring_Assignment.Models.Entities
{
    public enum ErrandStatus
    {
        NotStarted = 1,
        InProgress = 2,
        Completed = 3
    }
    internal class StatusEntity
    {
        [Key]
        public int Id { get; set; }
        public ErrandStatus Status { get; set; }
        public ICollection<ErrandEntity> Errands = new HashSet<ErrandEntity>();


    }
}
