using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;

namespace ModelingFourEyes
{
    public class Guard
    {
        public static void SupervisingUserBeingTheRequestingUser(User requestingUser, User supervisingUser)
        {
            if (requestingUser.Equals(supervisingUser))
                throw new SecurityException("The request can't be approved or declined by the requesting user.");
        }

        public static void ForEmpty(string value, string desc)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(desc);
        }

        public static void ForEmpty(object value, string desc)
        {
            if (value == null)
                throw new ArgumentNullException(desc);
        }
    }
}
