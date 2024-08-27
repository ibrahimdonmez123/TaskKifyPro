using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskKifyPro.Entity.Concrete
{
    public class User
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

        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string Adres { get; set; }
        public string Phone { get; set; }
        public bool Type { get; set; }
        public string PasswordHash { get; set; } 
    }


}
