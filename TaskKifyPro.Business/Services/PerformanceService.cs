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
    public class PerformanceService : IPerformanceService
    {
        private readonly IPerformanceDal _PerformanceDal;

        public PerformanceService(IPerformanceDal PerformanceDal)
        {
            _PerformanceDal = PerformanceDal;
        }

        public void Add(Performance entity)
        {
            entity.CreatedTime = DateTime.Now;
            entity.UpdatedTime = DateTime.Now;
            entity.Status = true;
            entity.CreatedUserId = entity.CreatedUserId;
            _PerformanceDal.Add(entity);
        }


        public void Delete(Performance entity)
        {
            _PerformanceDal.Delete(entity);
        }

        public List<Performance> GetAll()
        {
            var list = _PerformanceDal.GetAll(x => x.Status == true);
            return list;
        }

        public Performance GetById(int id)
        {
            var result = _PerformanceDal.Get(x => x.Id == id);
            return result;
        }



        public void Update(Performance entity)
        {
            entity.UpdatedTime = DateTime.Now;
            _PerformanceDal.Update(entity);
        }
    }
}
