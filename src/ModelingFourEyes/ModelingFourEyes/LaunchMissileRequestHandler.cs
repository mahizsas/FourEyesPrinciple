using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelingFourEyes
{
    public class LaunchMissleRequestHandler : IRequestHandler<LaunchMissileRequestContent>
    {
        public void OnAccepted(LaunchMissileRequestContent requestContent)
        {
            Console.WriteLine(string.Format("Launched {0} to {1}.", requestContent.MissileName, requestContent.Destination));
        }
    }
}
