using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.Data.Client
{
    public interface IFallibleOperation
    {
        event EventHandler<FallibleOperationEventArgs> ErrorOccured;
    }

    public class FallibleOperationEventArgs : EventArgs
    {
        public string Message { get; }

        public FallibleOperationEventArgs(string message) { Message = message; }
    }
}
