using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeTrack.TestModels
{
    public interface IUserModel
    {
        int GetAverageScore();
        int GetLatestScore();
        void AddScore(int score);
    }
}
