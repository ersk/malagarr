using Microsoft.JSInterop;

namespace Ersk43.Malagarr.Web.Client.Services
{
    public class FileExplorerService : IFileExplorerService, IAsyncDisposable
    {
        //private static FileExplorerService? instance = null;
        //public static FileExplorerService Instance
        //{
        //    get
        //    {
        //        if (instance == null)
        //        {
        //            throw new Exception("Method 'LoadJSModule' must be called before accessing the instance.");
        //        }
        //        return instance;
        //    }
        //}

        private readonly IJSRuntime jsRuntime;
        private IJSObjectReference? module = null;

        public FileExplorerService(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        public async Task LoadJSModuleIfNeeded()
        {
            if (module != null) return;

            //if (instance != null)
            //{
            //    throw new InvalidOperationException("JS Module has already been loaded.");
            //}

            module = await jsRuntime.InvokeAsync<IJSObjectReference>(
                "import",
                "./Pages/Counter.razor.js");

            //instance = new FileExplorerService(module);
        }



        public async Task<IJSObjectReference> RequestUserToOpenADirectory()
        {
            await LoadJSModuleIfNeeded();

            return await module!.InvokeAsync<IJSObjectReference>("openDirectoryAsync");
        }

        public async Task<IJSObjectReference> GetChildDirectoryHandle(IJSObjectReference parentDirectoryHandle, string childDirectoryName)
        {
            await LoadJSModuleIfNeeded();

            return await module!.InvokeAsync<IJSObjectReference>("getDirectoryHandle", parentDirectoryHandle, childDirectoryName);
        }

        public async Task<IJSObjectReference> GetChildFileHandle(IJSObjectReference parentDirectoryHandle, string childFileName)
        {
            await LoadJSModuleIfNeeded();

            return await module!.InvokeAsync<IJSObjectReference>("getFileHandle", parentDirectoryHandle, childFileName);
        }

        public async Task<List<string>> GetDirectoryContents(IJSObjectReference directoryHandle)
        {
            await LoadJSModuleIfNeeded();

            return await module!.InvokeAsync<List<string>>("getContents", directoryHandle);
        }

        public async Task<string> GetFileText(IJSObjectReference fileHandle)
        {
            await LoadJSModuleIfNeeded();

            return await module!.InvokeAsync<string>("getFileText", fileHandle);
        }

        public async ValueTask DisposeAsync()
        {
            if (module is not null)
            {
                await module.DisposeAsync();
            }
        }
    }
}
