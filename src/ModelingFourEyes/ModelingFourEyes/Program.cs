using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using Newtonsoft.Json;
using Ninject;

namespace ModelingFourEyes
{
    class Program
    {
        static void Main(string[] args)
        {
            var kernel = new StandardKernel();
            kernel.Bind<IRequestHandler<LaunchMissileRequestContent>>().To<LaunchMissleRequestHandler>();

            var request = new Request(
                new Requested(new User("MiniMe")),
                new LaunchMissileRequestContent() { MissileName = "V-2", Destination = "The Ozone Layer" });

            var repository = new InMemoryRepository();

            var requestCoordinator = new RequestCoordinator(repository, kernel);
            requestCoordinator.Create(request);

            foreach (var req in repository.GetAll())
                Console.WriteLine(req.Content.Summary);

            requestCoordinator.Accept(request.Id, new User("DrEvil"));

            Console.ReadLine();
        }                           
    }
}
