using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelingFourEyes
{
    public class Request
    {
        public Request(Requested requested, IRequestContent content)
        {
            Guard.ForEmpty(requested, "headers");
            Guard.ForEmpty(content, "content");

            Id = Guid.NewGuid();

            Requested = requested;
            Status = Status.Waiting;
            Content = content;
        }

        public Guid Id { get; private set; }

        public Requested Requested { get; private set; }

        public Supervised Supervised { get; private set; }

        public IRequestContent Content { get; private set; }

        public Status Status { get; private set; }

        public void Accept(Supervised supervised)
        {
            Guard.ForEmpty(supervised, "supervised");
            Guard.SupervisingUserBeingTheRequestingUser(Requested.By, supervised.By);

            ChangeStatus(Status.Accepted);
            Supervised = supervised;
        }

        public void Decline(Supervised supervised)
        {
            Guard.ForEmpty(supervised, "supervised");
            Guard.SupervisingUserBeingTheRequestingUser(Requested.By, supervised.By);

            ChangeStatus(Status.Declined);
            Supervised = supervised;
        }

        private void ChangeStatus(Status status)
        {
            if (Status != Status.Waiting)
                throw new InvalidOperationException("This request is already accepted or declined.");
            if (status == Status.Waiting)
                throw new InvalidOperationException("The status shouldn't be changed to waiting after creation.");            

            Status = status;
        }
    }                      
}
