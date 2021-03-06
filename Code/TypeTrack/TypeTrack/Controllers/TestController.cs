﻿using System;
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
            _testTimer = new Stopwatch();
            _testCompleted = false;
            _userEntryText = string.Empty;
            _completedWords = 0;
            _userProgressing = false;
        }

        public async void StartNewTest(string fileName = "") // @TODO: Make this asynchronous
        {
            _testCompleted = false;
            _completedWords = 0;
            _testTimer.Restart();
            _testManager.StartNewTest();
            NewTest.Invoke(this, new WordEventArgs(_testManager.GetRemainingWords()));

            await Task.Run(async () => RunTest());
        }

        public void UserProgress()
        {
            if (!_userProgressing)
            {
                _userProgressing = true;
            }
        }

        private void ProgressWord()
        {
            if (!_testManager.IsLastWord())
            {
                _userEntryText = string.Empty;
                _testManager.AdvanceWord();
                _completedWords += 1;
                NextWord.Invoke(this, new WordEventArgs(_testManager.GetRemainingWords()));
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
                        if (_userEntryText == _testManager.GetCurrentWord())
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
            double elapsedTime = _testTimer.Elapsed.TotalSeconds / 60;
            return (int)(_completedWords / (elapsedTime != 0 ? elapsedTime : 1));
        }

        public TestTelemetry GetCurrentTelemetry()
        {
            return new TestTelemetry(GetWPM(), _testTimer.Elapsed);
        }
    }
}
