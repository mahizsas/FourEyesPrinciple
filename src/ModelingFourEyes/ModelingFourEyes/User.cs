using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelingFourEyes
{
    public class User
    {
        public User(string userId)
        {
            Guard.ForEmpty(userId, "user id");

            UserId = userId;
        }

        public string UserId { get; private set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = obj as User;

            return UserId == other.UserId;
        }

        public override int GetHashCode()
        {
            return UserId.GetHashCode();
        }
    }        
}
