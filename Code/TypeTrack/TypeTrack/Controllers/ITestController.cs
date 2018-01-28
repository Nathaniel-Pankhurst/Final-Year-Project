﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeTrack.Controllers
{
    public class WordEventArgs : EventArgs
    {
        public string RemainingWords { get; set; }

        public WordEventArgs(string remainingWords)
        {
            RemainingWords = remainingWords;
        }
    }

    public class TestEndEventArgs : EventArgs
    {
        public int CompletedWords { get; set; }
        public TimeSpan CompletedTime { get; set; }
        public double WPM { get; set; }

        public TestEndEventArgs(int completedWords, TimeSpan completedTime, double wpm)
        {
            CompletedWords = completedWords;
            CompletedTime = completedTime;
            WPM = wpm;
        }
    }

    public class MistakeEventArgs : EventArgs
    {
        public string CorrectSubstring { get; set; }
        public string IncorrectSubstring { get; set; }

        public MistakeEventArgs(string correctSubstring, string incorrectSubstring)
        {
            CorrectSubstring = correctSubstring;
            IncorrectSubstring = incorrectSubstring;
        }
    }

    public delegate void NextWordHandler(object sender, WordEventArgs e);
    public delegate void NewTestHandler(object sender, WordEventArgs e);
    public delegate void TestEndHandler(object sender, TestEndEventArgs e);
    public delegate void MistakeHandler(object sender, MistakeEventArgs e);
 
    interface ITestController
    {
        void StartNewTest();
        void UserProgress();
    }
}
