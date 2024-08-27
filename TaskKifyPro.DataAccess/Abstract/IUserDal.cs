using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskKifyPro.DataAccess.GenericRepository;
using TaskKifyPro.Entity.Concrete;

namespace TaskKifyPro.DataAccess.Abstract
{
    public interface IUserDal : IGenericDal<User>
    {
    }
}
