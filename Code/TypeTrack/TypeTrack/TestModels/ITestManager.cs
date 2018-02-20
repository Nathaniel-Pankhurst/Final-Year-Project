using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeTrack.TestModels
{
    interface ITestManager
    {
        string GetCurrentWord();
        string AdvanceWord();
        string IsLastWord();
        string GetSampleText();
        void RepeatTest();
        void StartNewTest();
    }
}
