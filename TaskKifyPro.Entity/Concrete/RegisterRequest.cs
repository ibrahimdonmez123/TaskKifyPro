using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskKifyPro.Entity.Concrete
{
    public class RegisterRequest
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string Adres { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public int CompanyId { get; set; }
        public bool Type { get; set; }
        public int TeamId { get; set; }

    }
}
