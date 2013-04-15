using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelingFourEyes
{
    public interface IRequestRepository
    {
        IEnumerable<Request> GetAll();

        void Add(Request request);

        Request GetById(Guid id);
    }
}
