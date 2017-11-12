using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeTrack.TestModels
{
    public abstract class TestModel : ITestModel
    {
        public KeyValuePair<uint, string> GetCurrentWord()
        {
            throw new NotImplementedException();
        }

        public Dictionary<uint, string> GetTest()
        {
            throw new NotImplementedException();
        }
    }
}
