using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayNGo.FraceMarteja.DAL.Contracts
{
    public interface IEvaluatorService
    {
        bool IsValid(int count);
        Task<int> Evaluate(List<int> handIds);
        Task<DbResult> SaveWinners(int handId, Dictionary<int, List<int>> groupOfPlayers);
    }
}
