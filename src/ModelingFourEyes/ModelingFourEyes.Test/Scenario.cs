using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace ModelingFourEyes.Test
{    
    [TestClass]
    public abstract class Scenario
    {
        [TestInitialize]
        public void TestSetup()
        {          
            DomainEvents.Kernel = new StandardKernel();

            Given();
            When();
        }

        public abstract void Given();

        public abstract void When();
    }
}
