using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ninject;

namespace ModelingFourEyes.Test
{
    [TestClass]
    public class WhenLaunchingMissile : Scenario
    {
        private Request _requestUnderTest;
        private RequestCoordinator _coordinator;
        private Mock<IRequestHandler<LaunchMissileRequestContent>> _launchMissileRequestHandler;

        public override void Given()
        {
            _launchMissileRequestHandler = new  Mock<IRequestHandler<LaunchMissileRequestContent>>();

            var kernel = new StandardKernel();
            kernel
                .Bind<IRequestHandler<LaunchMissileRequestContent>>()
                .ToConstant(_launchMissileRequestHandler.Object);
            
            _coordinator = new RequestCoordinator(
                new InMemoryRepository(), kernel);            
        }

        public override void When()
        {
            _requestUnderTest = new Request(
                new Requested(new User("MiniMe")),
                new LaunchMissileRequestContent()
                {
                    MissileName = "V-2",
                    Destination = "The Ozone Layer"
                });

            _coordinator.Create(_requestUnderTest);
            _coordinator.Accept(_requestUnderTest.Id, new User("DrEvil"));            
        }

        [TestMethod]
        public void TheMissileIsLaunched()
        {
            _launchMissileRequestHandler.Verify(
                x => x.OnAccepted(
                    It.Is<LaunchMissileRequestContent>(
                        req => req.MissileName == "V-2" && req.Destination == "The Ozone Layer")));
        }

        [TestMethod]
        public void TheRequestIsRequestedByMiniMe()
        {
            Assert.AreEqual(new User("MiniMe"), _requestUnderTest.Requested.By);
        }

        [TestMethod]
        public void TheRequestIsAccepted()
        {
            Assert.AreEqual(Status.Accepted, _requestUnderTest.Status);
        }

        [TestMethod]
        public void TheRequestIsAcceptedByDrEvil()
        {
            Assert.AreEqual(new User("DrEvil"), _requestUnderTest.Supervised.By);
        }
    }
}
