using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using TypeTrack.TestModels;

namespace TypeTrack.Controllers
{
    public class NextWordEventArgs : EventArgs
    {
        List<string> RemainingWords { get; set; }

        public NextWordEventArgs(List<string> remainingWords)
        {
            RemainingWords = remainingWords;
        }
    }

    public delegate void NextWordHandler(object sender, NextWordEventArgs e);


    class TestController
    {
        private ITestModel _testModel;
        private Stopwatch _testTimer;
        private bool _testCompleted;
        private string _userEntryText;
        private int _completedWords;
        private TimeSpan _currentElapsedTime { get { return _testTimer.Elapsed; } }
        public event NextWordHandler NextWord;
        
        public TestController(string userEntryText)
        {
            _testModel = new SampleTestModel(new List<string> {"This", "is", "a", "test", "string"});
            _testTimer = new Stopwatch();
            _testCompleted = false;
            _userEntryText = userEntryText;
            _completedWords = 0;
        }

        public void NewTest() // @TODO: Make this asynchronous
        {
            _testCompleted = false;
            _completedWords = 0;
            _testTimer.Restart();

            while (!_testCompleted)
            {
                if (_testTimer.Elapsed < TimeSpan.FromSeconds(60))
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
                else
                {
                    // The user has run out of time, and the test has ended.
                    TestEnded();
                }
            }

        }

        private void ProgressWord()
        {
            if (!_testModel.IsLastWord())
            {
                _testModel.GetNextWord();
                _completedWords += 1;
                NextWord.Invoke(this, new NextWordEventArgs(_testModel.GetRemainingWords()));
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
    }
}
