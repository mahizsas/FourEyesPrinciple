using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security;

namespace ModelingFourEyes.Test
{
    [TestClass]
    public class WhenDecliningRequestAfterItHasBeenAccepted : Scenario
    {
        private Request _sut;
        private bool _invalidOperationThrown;

        public override void Given()
        {
            _sut = new Request(
                new Requested(new User("JefC")),
                new LaunchMissileRequestContent() { Destination = "Sahara", MissileName = "ABC123" });

            _sut.Accept(new Supervised(new User("KristienB")));
        }
        
        public override void When()
        {
            try
            {
                _sut.Decline(new Supervised(new User("JanJ")));
            }
            catch (InvalidOperationException)
            {
                _invalidOperationThrown = true;
            }
        }

        [TestMethod]
        public void AnInvalidOperationExceptionIsThrown()
        {
            Assert.IsTrue(_invalidOperationThrown);
        }
    }
}
