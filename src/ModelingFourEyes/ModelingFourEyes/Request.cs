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
            Guard.ForEmpty(requested, "requested");
            Guard.ForEmpty(content, "content");

            Id = Guid.NewGuid();

            Requested = requested;
            Content = content;
            Status = Status.Waiting;
        }

        public Guid Id { get; private set; }

        public Requested Requested { get; private set; }

        public Supervised Supervised { get; private set; }

        public IRequestContent Content { get; private set; }

        public Status Status { get; private set; }

        public void Accept(Supervised supervised)
        {         
            ChangeStatus(Status.Accepted, supervised);

            DomainEvents.Raise(new RequestAcceptedEvent() { RequestId = Id });
        }

        public void Decline(Supervised supervised)
        {           
            ChangeStatus(Status.Declined, supervised);            
        }

        private void ChangeStatus(Status status, Supervised supervised)
        {
            Guard.ForEmpty(supervised, "supervised");
            Guard.SupervisingUserBeingTheRequestingUser(Requested.By, supervised.By);

            if (Status != Status.Waiting)
                throw new InvalidOperationException("This request is already accepted or declined.");
            if (status == Status.Waiting)
                throw new InvalidOperationException("The status shouldn't be changed to Waiting after creation.");            

            Status = status;
            Supervised = supervised;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = obj as Request;

            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }                      
}
