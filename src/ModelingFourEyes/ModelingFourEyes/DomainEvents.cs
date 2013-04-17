using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;

namespace ModelingFourEyes
{
    public class DomainEvents
    {
        public static IKernel Kernel { get; set; }

        public static void Raise<TEvent>(TEvent @event) 
        {
            var handlers = Kernel.GetAll<IEventHandler<TEvent>>();

            foreach (var handler in handlers)            
                handler.Handle(@event);            
        }
    }
}
