using TaskKifyPro.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskKifyPro.DataAccess.Abstract;
using TaskKifyPro.Entity.Concrete;

namespace TaskKifyPro.Business.Services
{
    public class UserDutyService : IUserDutyService
    {
        private readonly IUserDutyDal _UserDutyDal;

        public UserDutyService(IUserDutyDal UserDutyDal)
        {
            _UserDutyDal = UserDutyDal;
        }

        public void Add(UserDuty entity)
        {
            entity.CreatedTime = DateTime.Now;
            entity.UpdatedTime = DateTime.Now;
            entity.Status = true;
            entity.CreatedUserId = entity.CreatedUserId;
            _UserDutyDal.Add(entity);
        }


        public void Delete(UserDuty entity)
        {
            _UserDutyDal.Delete(entity);
        }

        public List<UserDuty> GetAll()
        {
            var list = _UserDutyDal.GetAll(x => x.Status == true);
            return list;
        }

        public UserDuty GetById(int id)
        {
            var result = _UserDutyDal.Get(x => x.Id == id);
            return result;
        }



        public void Update(UserDuty entity)
        {
            entity.UpdatedTime = DateTime.Now;
            _UserDutyDal.Update(entity);
        }
    }
}
