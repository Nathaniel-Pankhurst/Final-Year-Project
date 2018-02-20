using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TypeTrack.TestModels
{
    interface ITestManager
    {
        void AdvanceWord();
        string GetCurrentWord();
        string GetSampleText();
        bool IsLastWord();
        void RepeatTest();
        void StartNewTest(string fileName = "");
        void RefreshTestList();
    }
}
