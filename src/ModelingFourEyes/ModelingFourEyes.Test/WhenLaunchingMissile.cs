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
        private Mock<IRequestHandler<LaunchMissileRequestContent>> _launchMissileRequestHandler;
        private IRequestRepository _repository;

        public override void Given()
        {
            DependenciesAreRegistered();

            _requestUnderTest = new Request(
                new Requested(new User("MiniMe")),
                new LaunchMissileRequestContent()
                {
                    MissileName = "V-2",
                    Destination = "The Ozone Layer"
                });

            _repository.Add(_requestUnderTest);
        }

        public override void When()
        {                    
            var request = _repository.GetById(_requestUnderTest.Id); 

            request.Accept(new Supervised(new User("DrEvil")));              
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

        private void DependenciesAreRegistered()
        {
            _launchMissileRequestHandler = new Mock<IRequestHandler<LaunchMissileRequestContent>>();
            _repository = new InMemoryRepository();
            
            DomainEvents.Kernel.Bind<IRequestHandler<LaunchMissileRequestContent>>().ToConstant(_launchMissileRequestHandler.Object);
            DomainEvents.Kernel.Bind<IEventHandler<RequestApprovedEvent>>().ToConstant(new RequestApprovedEventHandler(DomainEvents.Kernel, _repository));
            DomainEvents.Kernel.Bind<IRequestRepository>().ToConstant(_repository);         
        }
    }
}
