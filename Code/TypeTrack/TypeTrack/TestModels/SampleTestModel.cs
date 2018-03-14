using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TypeTrack.TestModels
{
    public class SampleTestModel : TestModel
    {
        public SampleTestModel(List<string> test) : base(test) // @TODO remove this constructor, it's not needed in this class anymore
        {
            _testType = TestType.Sample;
        }

        public SampleTestModel(JObject testObject) : base(testObject)
        {
            _testName = (string)testObject["Name"];
            _test = ((JArray)testObject["Test"]).ToObject<List<string>>();
        }
    }
}
