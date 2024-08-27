using TaskKifyPro.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskKifyPro.DataAccess.Abstract;
using TaskKifyPro.Entity.Concrete;
using TaskKifyPro.DataAccess.EntityFramework;

namespace TaskKifyPro.Business.Services
{
    public class DutyService : IDutyService
    {
        private readonly IDutyDal _DutyDal;

        public DutyService(IDutyDal DutyDal)
        {
            _DutyDal = DutyDal;
        }

        public void Add(Duty entity)
        {
            entity.CreatedTime = DateTime.Now;
            entity.UpdatedTime = DateTime.Now;
            entity.Status = true;
            entity.CreatedUserId = entity.CreatedUserId;
            _DutyDal.Add(entity);
        }


        public void Delete(Duty entity)
        {
            _DutyDal.Delete(entity);
        }

        public List<Duty> GetAll()
        {
            var list = _DutyDal.GetAll(x => x.Status == true);
            return list;
        }


      


        public Duty GetById(int id)
        {
            var result = _DutyDal.Get(x => x.Id == id);
            return result;
        }



        public void Update(Duty entity)
        {
            entity.UpdatedTime = DateTime.Now;
            _DutyDal.Update(entity);
        }
    }
}
