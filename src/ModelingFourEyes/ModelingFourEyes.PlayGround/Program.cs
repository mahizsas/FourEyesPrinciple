﻿using System;
using Ninject;

namespace ModelingFourEyes
{
    class Program
    {
        static void Main(string[] args)
        {
            var kernel = new StandardKernel();
            kernel.Bind<IEventHandler<RequestAcceptedEvent>>().To<RequestAcceptedEventHandler>();
            kernel.Bind<IRequestHandler<LaunchMissileRequestContent>>().To<LaunchMissileRequestHandler>();
            kernel.Bind<IRequestRepository>().ToConstant(new InMemoryRepository());
            DomainEvents.Kernel = kernel;

            var repository = kernel.Get<IRequestRepository>();

            var launchMissileRequest = new Request(
                new Requested(new User("MiniMe")),
                new LaunchMissileRequestContent("V-2", "The Ozone Layer"));

            repository.Add(launchMissileRequest);

            ListRepositoryContent(repository);

            var request = repository.GetById(launchMissileRequest.Id);

            request.Accept(new Supervised(new User("DrEvil")));

            ListRepositoryContent(repository);

            Console.ReadLine();
        }

        public static void ListRepositoryContent(IRequestRepository repository)
        {
            foreach (var req in repository.GetAll())
            {
                Console.WriteLine("-----------------------------------------");
                Console.WriteLine("Id: " + req.Id);
                Console.WriteLine("Status: " + req.Status);
                Console.WriteLine("Summary: " + req.Content.Summary);
                Console.WriteLine("-----------------------------------------");
            }
        }
    }

    public class LaunchMissileRequestHandler : IRequestHandler<LaunchMissileRequestContent>
    {
        public void OnAccepted(LaunchMissileRequestContent requestContent)
        {
            Console.WriteLine(string.Format("Event: launching {0} to {1}!!", requestContent.MissileName, requestContent.Destination));
        }
    }

    public class LaunchMissileRequestContent : IRequestContent
    {
        public LaunchMissileRequestContent(string missileName, string destination)
        {
            Guard.ForEmpty(missileName, "missile name");
            Guard.ForEmpty(destination, "destination");

            MissileName = missileName;
            Destination = destination;
        }

        public string MissileName { get; set; }

        public string Destination { get; set; }

        public string Summary
        {
            get { return string.Format("Launch {0} to {1}.", MissileName, Destination); }
        }
    }
}
