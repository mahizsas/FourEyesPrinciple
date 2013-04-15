using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelingFourEyes
{
    public interface IRequestHandler<TRequestContent>
    {
        void OnAccepted(TRequestContent requestContent);
    }
}
