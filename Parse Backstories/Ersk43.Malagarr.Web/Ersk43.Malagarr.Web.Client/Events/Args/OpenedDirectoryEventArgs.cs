using Microsoft.JSInterop;

namespace Ersk43.Malagarr.Web.Client.Events.Args
{
    public class OpenedDirectoryEventArgs
    {
        public IJSObjectReference JSDirectoryHandle { get; }

        public OpenedDirectoryEventArgs(IJSObjectReference jsDirectoryHandle)
        {
            JSDirectoryHandle = jsDirectoryHandle;
        }
    }

}


