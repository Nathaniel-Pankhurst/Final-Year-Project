using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using TypeTrack.TestModels;

namespace TypeTrack.Controllers
{
    class TestController : ITestController
    {
        private ITestModel _testModel;
        private Stopwatch _testTimer;
        private bool _testCompleted;
        private bool _userProgressing;
        private string _userEntryText;
        private int _completedWords;
        private TimeSpan _currentElapsedTime { get { return _testTimer.Elapsed; } }
        public event NextWordHandler NextWord;
        public event NewTestHandler NewTest;
        
        public TestController(string userEntryText)
        {
            _testModel = new SampleTestModel(new List<string> {"This", "is", "a", "test", "string"});
            _testTimer = new Stopwatch();
            _testCompleted = false;
            _userEntryText = userEntryText;
            _completedWords = 0;
            _userProgressing = false;
        }

        public async void StartNewTest() // @TODO: Make this asynchronous
        {
            _testCompleted = false;
            _completedWords = 0;
            _testTimer.Restart();
            NewTest.Invoke(this, new WordEventArgs(_testModel.GetRemainingWords()));

            await Task.Run(async () => RunTest());
        }

        public void UserProgress()
        {
            _userProgressing = true;
        }

        private void ProgressWord()
        {
            if (!_testModel.IsLastWord())
            {
                _userEntryText = string.Empty;
                _testModel.GetNextWord();
                _completedWords += 1;
                NextWord.Invoke(this, new WordEventArgs(_testModel.GetRemainingWords()));
            }
            else
            {
                // The user has typed all words from the sample text, and the test has ended.
                TestEnded();
            }
        }

        private void TestEnded()
        {
            _testCompleted = true;
            _testTimer.Stop();
            int userWPM = _completedWords / ((int)_testTimer.Elapsed.TotalSeconds / 60); 
        }

        private void RunTest()
        {
            while (!_testCompleted)
            {
                if (_testTimer.Elapsed < TimeSpan.FromSeconds(60))
                {
                    if (_userProgressing)
                    {
                        _userProgressing = false;
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
                else
                {
                    // The user has run out of time, and the test has ended.
                    TestEnded();
                }
            }
        }
    }
}
