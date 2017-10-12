using PlayNGo.FraceMarteja.DAL.Contracts;
using PlayNGo.FraceMarteja.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayNGo.FraceMarteja.DAL.Services
{
    public class GroupService: BaseService<Group>, IGroupService
    {
        public GroupService(PlayNGoDBEntities db) : base(db) {
        }
    }
}
