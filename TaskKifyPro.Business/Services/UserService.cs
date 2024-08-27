using TaskKifyPro.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskKifyPro.DataAccess.Abstract;
using TaskKifyPro.DataAccess.Context;

using TaskKifyPro.Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace TaskKifyPro.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserDal _UserDal;

        TaskifyProDbContext context = new TaskifyProDbContext();
        public UserService(IUserDal UserDal)
        {
            _UserDal = UserDal;
        }

        public void Add(User entity)
        {
            entity.CreatedTime = DateTime.Now;
            entity.UpdatedTime = DateTime.Now;
            entity.Status = true;
            entity.CreatedUserId = entity.CreatedUserId;
            _UserDal.Add(entity);
        }
        
        public void Delete(User entity)
        {
            _UserDal.Delete(entity);
        }

        public List<User> GetAll()
        {
            var list = _UserDal.GetAll(x => x.Status == true);
            return list;
        }

        public User GetById(int id)
        {
            var result = _UserDal.Get(x => x.Id == id);
            return result;
        }



        public void Update(User entity)
        {
            entity.UpdatedTime = DateTime.Now;
            _UserDal.Update(entity);
        }
    }
}
