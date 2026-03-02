using Microsoft.JSInterop;

namespace Ersk43.Malagarr.Web.Client.Events.Args
{
    public class ModLoadingErrorEventArgs
    {
        public Exception Exception { get; }

        public ModLoadingErrorEventArgs(Exception exception)
        {
            Exception = exception;
        }
    }

}


