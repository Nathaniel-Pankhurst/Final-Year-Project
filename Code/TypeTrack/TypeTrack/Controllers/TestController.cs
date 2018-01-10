using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using TypeTrack.TestModels;

namespace TypeTrack.Controllers
{
    class TestController
    {
        private ITestModel _testModel;
        private Stopwatch _testTimer;
        private string _userEntryText;
        private int _completedWords;
        private TimeSpan _currentElapsedTime { get { return _testTimer.Elapsed; } }
        
        public TestController(string userEntryText)
        {
            _testModel = new SampleTestModel(new List<string> {"This", "is", "a", "test", "string"});
            _testTimer = new Stopwatch();
            _completedWords = 0;
        }

        public void NewTest() // @TODO: Make this asynchronous
        {
            _completedWords = 0;
            _testTimer.Restart();

            while (_testTimer.Elapsed < TimeSpan.FromSeconds(60))
            {
                if (_userEntryText == _testModel.GetCurrentWord())
                {
                    ProgressWord();
                }
                else
                {
                    // Show errors on screen.
                }
            }
        }

        private void ProgressWord()
        {
            if (!_testModel.IsLastWord())
            {
                _testModel.GetNextWord();
            }
            else
            {
                // End of test, lets finish up and hand over the score
            }
        }
    }
}
