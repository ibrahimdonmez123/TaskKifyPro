using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskKifyPro.Entity.Concrete
{
    public class Performance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        public int CreatedUserId { get; set; }
        public int UpdatedUserId { get; set; }
        public bool Status { get; set; }

        public int TeamId { get; set; }



        public int UserId { get; set; }
        public int PerformanceNote { get; set; }




    }
}
