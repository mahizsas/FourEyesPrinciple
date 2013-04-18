using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModelingFourEyes.Test
{
    [TestClass]
    public class WhenGuarding
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ForEmptyString()
        {
            Guard.ForEmpty(string.Empty, "string");      
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ForEmptyObject()
        {
            Guard.ForEmpty((object)null, "object");
        }
    }
}
