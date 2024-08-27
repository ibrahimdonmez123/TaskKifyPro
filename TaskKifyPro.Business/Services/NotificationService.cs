using TaskKifyPro.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskKifyPro.DataAccess.Abstract;
using TaskKifyPro.DataAccess.EntityFramework;
using TaskKifyPro.Entity.Concrete;

namespace TaskKifyPro.Business.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationDal _NotificationDal;

        public NotificationService(INotificationDal NotificationDal)
        {
            _NotificationDal = NotificationDal;
        }

        public void Add(Notification entity)
        {
            entity.CreatedTime = DateTime.Now;
            entity.UpdatedTime = DateTime.Now;
            entity.Status = true;
            entity.CreatedUserId = entity.CreatedUserId;
            _NotificationDal.Add(entity);
        }


        public void Delete(Notification entity)
        {
            _NotificationDal.Delete(entity);
        }

        public List<Notification> GetAll()
        {
            var list = _NotificationDal.GetAll(x => x.Status == true);
            return list;
        }

        public Notification GetById(int id)
        {
            var result = _NotificationDal.Get(x => x.Id == id);
            return result;
        }



        public void Update(Notification entity)
        {
            entity.UpdatedTime = DateTime.Now;
            _NotificationDal.Update(entity);
        }
    }
}
