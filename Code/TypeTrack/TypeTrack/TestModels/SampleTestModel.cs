using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeTrack.TestModels
{
    public class SampleTestModel : TestModel
    {
        public SampleTestModel(List<string> test) : base(test)
        {
            _testType = TestType.Random;
        }
    }
}
