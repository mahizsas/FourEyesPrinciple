using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModelingFourEyes.Test
{    
    [TestClass]
    public abstract class Scenario
    {
        [TestInitialize]
        public void TestSetup()
        {
            Given();
            When();
        }

        public abstract void Given();

        public abstract void When();
    }
}
