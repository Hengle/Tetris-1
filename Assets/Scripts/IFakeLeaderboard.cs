using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public interface IFakeLeaderboard<T>
    {
        List<T> GetLeaderboards();
        void AddNewScore(T score);
    }
}
