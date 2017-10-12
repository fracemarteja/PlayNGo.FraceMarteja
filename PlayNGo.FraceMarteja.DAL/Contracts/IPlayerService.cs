using PlayNGo.FraceMarteja.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayNGo.FraceMarteja.DAL.Contracts
{
    public interface IPlayerService : IBaseService<Player>
    {
        Task<List<Player>> GetPlayersOrderedByName();
        Task<bool> IsAlreadyExists(string name);
    }
}
