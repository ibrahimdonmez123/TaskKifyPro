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

    public class DutyDal : GenericDal<Duty, TaskifyProDbContext>, IDutyDal
    {
        private readonly TaskifyProDbContext _context;
        public DutyDal(TaskifyProDbContext context) : base(context)
        {
            _context = context;
        }


    }
}
