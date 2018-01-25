using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeTrack.TestModels
{
    public enum TestType
    {
        Random,
        Sample
    }

    public interface ITestModel
    {
        string GetCurrentWord();
        string GetNextWord();
        string GetRemainingWords();
        bool IsLastWord();
    }
}
