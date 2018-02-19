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

        public string SampleText
        {
            get
            {
                return string.Join(" ", _test);
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
            _test = test;
            _currentWord = _test.IndexOf(_test.FirstOrDefault());
        }


        public string GetCurrentWord()
        {
            return _test[_currentWord];
        }

        public string GetNextWord()
        {
            string nextWord = string.Empty;
            if (!IsLastWord())
            {
                _currentWord += 1;
                nextWord = GetCurrentWord();
            }

            return nextWord;
        }

        public string GetRemainingWords()
        {
            return string.Join(" ", _test.GetRange(_currentWord, _test.Count - _currentWord));
        }

        public bool IsLastWord()
        {
            bool endOfTest = true;
            if(_currentWord !=_test.Count - 1)
            {
                endOfTest = false;
            }

            return endOfTest;
        }
    }
}
