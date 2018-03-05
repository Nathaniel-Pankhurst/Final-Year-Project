using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using TypeTrack.TestModels;

namespace TypeTrack.Controllers
{
    public class TestController : ITestController
    {
        private ITestModel _testModel;
        private ITestManager _testManager;
        private Stopwatch _testTimer;
        private bool _testCompleted;
        private bool _userProgressing;
        private string _userEntryText;
        private int _completedWords;
        private TimeSpan _currentElapsedTime { get { return _testTimer.Elapsed; } }
        public event NextWordHandler NextWord;
        public event NewTestHandler NewTest;
        public event TestEndHandler TestEnd;
        public event MistakeHandler MistakeMade;
        
        public TestController(ITestManager testManager)
        {
            _testManager = testManager;
            _testModel = new SampleTestModel(new List<string> {"The", "quick", "brown", "fox", "jumped", "over", "the", "lazy", "dogs"});
            _testTimer = new Stopwatch();
            _testCompleted = false;
            _userEntryText = string.Empty;
            _completedWords = 0;
            _userProgressing = false;
        }

        public async void StartNewTest() // @TODO: Make this asynchronous
        {
            _testCompleted = false;
            _completedWords = 0;
            _testTimer.Restart();
            _testModel.StartNewTest();
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
            int userWPM = GetWPM();
            TestEnd?.Invoke(this, new TestEndEventArgs(_completedWords, _testTimer.Elapsed, userWPM));
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
                            MistakeMade?.Invoke(this, new MistakeEventArgs(string.Empty, string.Empty)); // @TODO: Fix this so that it actually displays errors
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

        public void UpdateUserEntryText(string newEntryText)
        {
            _userEntryText = newEntryText;
        }

        private int GetWPM()
        {
            int elapsedTime = (int)_testTimer.Elapsed.TotalSeconds / 60;
            return _completedWords / (elapsedTime != 0 ? elapsedTime : 1);
        }

        public TestTelemetry GetCurrentTelemetry()
        {
            return new TestTelemetry(GetWPM(), _testTimer.Elapsed);
        }
    }
}
