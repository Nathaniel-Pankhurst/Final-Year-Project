using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeTrack.TestModels
{
    public abstract class TestModel : ITestModel
    {
        protected List<string> _test;
        protected int _currentWord;
        protected TestType _testType;

        public List<string> Test
        {
            get
            {
                return _test;
            }
        }

        public TestType TestType
        {
            get
            {
                return _testType;
            }
        }

        public TestModel(List<string> test)
        {
            _test = new List<string>();
            _currentWord = _test.IndexOf(_test.FirstOrDefault());
        }


        public string GetCurrentWord()
        {
            return _test[_currentWord];
        }

        public string GetNextWord()
        {
            string nextWord = string.Empty;
            if (_currentWord < _test.Count)
            {
                _currentWord += 1;
                nextWord = GetCurrentWord();
            }

            return nextWord;
        }
    }
}
