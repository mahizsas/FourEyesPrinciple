using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelingFourEyes
{
    public class InMemoryRepository : IRequestRepository
    {
        private readonly List<Request> _requests = new List<Request>();

        public IEnumerable<Request> GetAll()
        {
            return _requests;
        }

        public void Add(Request request)
        {
            _requests.Add(request);
        }

        public Request GetById(Guid id)
        {
            return _requests.Where(x => x.Id == id).SingleOrDefault();
        }
    }
}
