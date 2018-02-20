using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeTrack.TestModels
{
    class TestManager : ITestManager
    {
        private TestModel _testModel;
        private List<string> _testLocations;

        public TestManager() // @TODO: Need to edit this so that it allows the user to pre=load a test. 
        {
        }

        public void AdvanceWord()
        {
            _testModel.GetNextWord();
        }

        public string GetCurrentWord()
        {
            return _testModel.GetCurrentWord();
        }

        public string GetSampleText()
        {
            return _testModel.SampleText;
        }

        public bool IsLastWord()
        {
            return _testModel.IsLastWord();
        }

        public void RepeatTest()
        {
            if (_testModel != null)
            {
                _testModel.StartNewTest();
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        public void StartNewTest(string fileName = "")
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                _testModel = 
            }
        }
    }
}
