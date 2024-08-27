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
    public class TeamDal : GenericDal<Team, TaskifyProDbContext>, ITeamDal
    {
        private readonly TaskifyProDbContext _context;
        public TeamDal(TaskifyProDbContext context) : base(context)
        {
            _context = context;
        }


    }
}
