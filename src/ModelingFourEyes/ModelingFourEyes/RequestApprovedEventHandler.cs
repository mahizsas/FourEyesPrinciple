using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;

namespace ModelingFourEyes
{
    public class RequestApprovedEventHandler : IEventHandler<RequestApprovedEvent>
    {
        private readonly IKernel _kernel;
        private readonly IRequestRepository _requestRepository;

        public RequestApprovedEventHandler(IKernel kernel, IRequestRepository requestRepository)
        {
            _kernel = kernel;
            _requestRepository = requestRepository;
        }

        public void Handle(RequestApprovedEvent @event)
        {
            var request = _requestRepository.GetById(@event.RequestId);

            var requestHandlerType = typeof(IRequestHandler<>).MakeGenericType(request.Content.GetType());
            foreach (var requestHandler in _kernel.GetAll(requestHandlerType))
                ((dynamic)requestHandler).OnAccepted((dynamic)request.Content);
        }
    }
}
