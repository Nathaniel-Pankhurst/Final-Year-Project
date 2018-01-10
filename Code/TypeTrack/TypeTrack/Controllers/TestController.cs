using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using TypeTrack.TestModels;

namespace TypeTrack.Controllers
{
    class TestController
    {
        private ITestModel _testModel;
        private Stopwatch _testTimer;
        private int _completedWords;
        private TimeSpan _currentElapsedTime { get { return _testTimer.Elapsed; } }
        
        public TestController()
        {
            _testModel = new SampleTestModel(new List<string> {"This", "is", "a", "test", "string"});
            _testTimer = new Stopwatch();
            _completedWords = 0;
        }

        public void NewTest() // @TODO: Make this asynchronous
        {
            _completedWords = 0;
            string currentWord = _testModel.GetCurrentWord();
            _testTimer.Restart();

            while (_testTimer.Elapsed < TimeSpan.FromSeconds(60))
            {

            }
        }
    }
}
