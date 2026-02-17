using Microsoft.JSInterop;

namespace Ersk43.Malagarr.Web.Client.Services
{
    public interface IFileExplorerService
    {
        public Task<IJSObjectReference> RequestUserToOpenADirectory();
        public Task<List<string>> GetDirectoryContents(IJSObjectReference directoryHandle);
        public Task<IJSObjectReference> GetChildDirectoryHandle(IJSObjectReference parentDirectoryHandle, string childDirectoryName);
        public Task<IJSObjectReference> GetChildFileHandle(IJSObjectReference parentDirectoryHandle, string childFileName);
        public Task<string> GetFileText(IJSObjectReference fileHandle);
    }
}
