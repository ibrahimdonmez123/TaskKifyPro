using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskKifyPro.DataAccess.Abstract;
using TaskKifyPro.DataAccess.Context;
using TaskKifyPro.DataAccess.GenericRepository;
using TaskKifyPro.Entity.Concrete;

namespace TaskKifyPro.DataAccess.EntityFramework
{
    public class UserDutyDal : GenericDal<UserDuty, TaskifyProDbContext>, IUserDutyDal
    {
        private readonly TaskifyProDbContext _context;
        public UserDutyDal(TaskifyProDbContext context) : base(context)
        {
            _context = context;
        }


    }
}
