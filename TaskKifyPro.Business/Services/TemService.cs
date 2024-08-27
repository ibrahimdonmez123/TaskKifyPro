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
    public class TeamService : ITeamService
    {
        private readonly ITeamDal _TeamDal;

        public TeamService(ITeamDal TeamDal)
        {
            _TeamDal = TeamDal;
        }

        public void Add(Team entity)
        {
            entity.CreatedTime = DateTime.Now;
            entity.UpdatedTime = DateTime.Now;
            entity.Status = true;
            entity.CreatedUserId = entity.CreatedUserId;
            _TeamDal.Add(entity);
        }


        public void Delete(Team entity)
        {
            _TeamDal.Delete(entity);
        }

        public List<Team> GetAll()
        {
            var list = _TeamDal.GetAll(x => x.Status == true);
            return list;
        }

        public Team GetById(int id)
        {
            var result = _TeamDal.Get(x => x.Id == id);
            return result;
        }



        public void Update(Team entity)
        {
            entity.UpdatedTime = DateTime.Now;
            _TeamDal.Update(entity);
        }
    }
}
