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
        private DirectoryInfo _dInfo = new DirectoryInfo(@"\\SampleTexts");

        public TestManager() // @TODO: Need to edit this so that it allows the user to pre-load a test. 
        {
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
                throw new NullReferenceException();
            }
        }

        public void StartNewTest(string fileName = "")
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                //_testModel =
            }
        }

        public void RefreshTestList()
        { 
            FileInfo[] files = _dInfo.GetFiles("*.json");

            _testLocations = files.Select(p => p.Name).ToList();
        }

        private bool TryGetTestModel(string fileLocation, out TestModel testModel)
        {
            bool found = false;

            StreamReader file = File.OpenText(fileLocation);
            JsonTextReader jsonFile = new JsonTextReader(file);
            JObject testObject = (JObject)JToken.ReadFrom(jsonFile);
            testModel = new SampleTestModel(testObject);

            return found;
        }
    }
}
