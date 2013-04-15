using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;

namespace ModelingFourEyes
{
    public class RequestCoordinator
    {
        private IRequestRepository _requestRepository;
        private IKernel _kernel;

        public RequestCoordinator(IRequestRepository requestRepository, IKernel kernel)
        {
            _requestRepository = requestRepository;
            _kernel = kernel;
        }

        public void Create(Request request)
        {
            _requestRepository.Add(request);
        }

        public void Accept(Guid id, User supervisingUser)
        {
            var request = _requestRepository.GetById(id);

            request.Accept(new Supervised(supervisingUser));

            var requestHandlerType = typeof(IRequestHandler<>).MakeGenericType(request.Content.GetType());
            foreach (var requestHandler in _kernel.GetAll(requestHandlerType))
                ((dynamic)requestHandler).OnAccepted((dynamic)request.Content);
        }

        public void Decline(Guid id, User supervisingUser)
        {
            var request = _requestRepository.GetById(id);

            request.Decline(new Supervised(supervisingUser));
        }
    }

}
