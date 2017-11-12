using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeTrack.TestModels
{
    public class UserModel : IUserModel
    {
        private string _userName;
        private List<int> _scores;

        public List<int> Scores
        {
            get
            {
                return _scores;
            }
        }
        public UserModel(string userName)
        {
            _userName = userName;
            _scores = new List<int>();
        }

        public void AddScore(int score)
        {
            _scores.Add(score);
        }

        public int GetAverageScore()
        {
            return _scores.Count > 0 ? Convert.ToInt32(_scores.Average()) : 0;
        }

        public int GetLatestScore()
        {
            return _scores.Last();
        }
    }
}
