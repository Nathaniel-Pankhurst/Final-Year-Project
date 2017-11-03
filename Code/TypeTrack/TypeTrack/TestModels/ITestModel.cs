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
        string TestName { get; }
        TestType TestType { get; }
        int TestLength { get; set; }
    
        Dictionary<uint, string> GetTest();
        KeyValuePair<uint, string> GetCurrentWord();
    }
}
