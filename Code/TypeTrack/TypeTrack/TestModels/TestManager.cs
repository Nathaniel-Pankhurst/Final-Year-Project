using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace TypeTrack.TestModels
{
    class TestManager : ITestManager
    {
        private TestModel _testModel;
        private List<string> _testLocations;
        private DirectoryInfo _dInfo = new DirectoryInfo(Environment.CurrentDirectory + @"\..\..\TestModels\SampleTexts");

        public TestManager() // @TODO: Need to edit this so that it allows the user to pre-load a test. 
        {
            RefreshTestList();
        }

        public void AdvanceWord()
        {
            _testModel.GetNextWord();
        }

        public string GetCurrentWord()
        {
            return _testModel.GetCurrentWord();
        }

        public string GetSampleText()
        {
            return _testModel.SampleText; 
        }

        public bool IsLastWord()
        {
            return _testModel.IsLastWord();
        }

        public void RepeatTest()
        {
            if (_testModel != null)
            {
                _testModel.StartNewTest();
            }
            else
            {
                throw new NullReferenceException(); //@TODO: create a specific exception handler here
            }
        }

        public void StartNewTest(string fileName = "")
        {
                TestModel testModel;
            if (!string.IsNullOrEmpty(fileName))
            {
                if(TryGetTestModel(fileName, out testModel))
                {
                    _testModel = testModel;
                }
                else
                {
                    // Throw exception to be caught in error handler.
                }
            }
            else
            {
                var rand = new Random();
                string randomTestLocation = _testLocations[rand.Next(_testLocations.Count)];
                
                if(TryGetTestModel(randomTestLocation, out testModel))
                {

                }
            }
        }

        public void RefreshTestList()
        { 
            FileInfo[] files = _dInfo.GetFiles("*.json");

            _testLocations = files.Select(p => p.Name).ToList();
        }

        public string GetRemainingWords()
        {
            if(_testModel != null)
            {
                return _testModel.GetRemainingWords();
            }
            else
            {
                // throw an exception here
            }

            return string.Empty;
        }

        private static bool TryGetTestModel(string fileLocation, out TestModel testModel)
        {
            bool found = false;

            StreamReader file = File.OpenText(fileLocation + ".json");
            JsonTextReader jsonFile = new JsonTextReader(file);
            JObject testObject = (JObject)JToken.ReadFrom(jsonFile);
            testModel = new SampleTestModel(testObject);

            return found;
        }
    }
}
