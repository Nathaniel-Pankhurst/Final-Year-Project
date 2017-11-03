using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeTrack.TestModels
{
    interface ITestModel
    {
        List<String> GetTest();
        String GetCurrentWord();
    }
}
