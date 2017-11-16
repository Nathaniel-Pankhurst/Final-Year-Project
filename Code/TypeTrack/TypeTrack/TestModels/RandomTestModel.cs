﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeTrack.TestModels
{
    public class RandomTestModel : TestModel
    {
        private RandomTestModel() : base(GenerateRandomText())
        {
            _testType = TestType.Random;
        }

        private static List<string> GenerateRandomText()
        {
            return new List<string>();
        }
    }
}