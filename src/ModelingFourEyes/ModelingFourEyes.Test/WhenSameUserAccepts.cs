using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security;

namespace ModelingFourEyes.Test
{
    [TestClass]
    public class WhenSameUserAccepts : Scenario
    {
        private Request _sut;

        private User _sameUser = new User("SadamH");
        private bool _securityExceptionIsThrown;

        public override void Given() 
        {
            _sut = new Request(
                new Requested(_sameUser),
                new TestLaunchMissileRequestContent());            
        }

        public override void When()
        {
            try
            {
                _sut.Accept(new Supervised(_sameUser));
            }
            catch (SecurityException)
            {
                _securityExceptionIsThrown = true;
            }
        }

        [TestMethod]
        public void ASecurityExceptionIsThrown()
        {
            Assert.IsTrue(_securityExceptionIsThrown);
        }
    }
}
