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
    public class NotificationDal : GenericDal<Notification, TaskifyProDbContext>, INotificationDal
    {
        private readonly TaskifyProDbContext _context;
        public NotificationDal(TaskifyProDbContext context) : base(context)
        {
            _context = context;
        }


    }
}
