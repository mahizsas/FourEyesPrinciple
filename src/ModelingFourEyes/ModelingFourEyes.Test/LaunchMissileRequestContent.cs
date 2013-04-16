using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelingFourEyes
{
    public class LaunchMissileRequestContent : IRequestContent
    {
        public string MissileName { get; set; }

        public string Destination { get; set; }

        public string Summary
        {
            get { return string.Format("Launch missile {0} destined for {1}.", MissileName, Destination); }
        }
    }           
}
