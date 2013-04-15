using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelingFourEyes
{
    public class Requested
    {
        public Requested(User by)
        {
            Guard.ForEmpty(by, "by");

            On = DateTime.Now;
            By = by;
        }

        public DateTime On { get; private set; }

        public User By { get; private set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = obj as Requested;

            return On == other.On && By == other.By;
        }

        public override int GetHashCode()
        {
            return On.GetHashCode() ^ By.GetHashCode();
        }
    }
}
