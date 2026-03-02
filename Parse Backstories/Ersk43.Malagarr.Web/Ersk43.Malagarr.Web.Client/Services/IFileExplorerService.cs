using Microsoft.JSInterop;

namespace Ersk43.Malagarr.Web.Client.Services
{
    public interface IFileExplorerService
    {
        public Task<IJSObjectReference> RequestUserToOpenADirectory();

        public class DirectoryContentsChild
        {
            public string? Name { get; set; }
            public string? Kind { get; set; }
        }
       

        public Task<List<DirectoryContentsChild>> GetDirectoryContents(IJSObjectReference directoryHandle);
        public Task<IJSObjectReference> GetChildDirectoryHandle(IJSObjectReference parentDirectoryHandle, string childDirectoryName);
        public Task<IJSObjectReference> GetChildFileHandle(IJSObjectReference parentDirectoryHandle, string childFileName);
        public Task<string> GetFileText(IJSObjectReference fileHandle);

        public Task<IJSObjectReference> GetDescendantDirectory(
            IJSObjectReference jsDirectoryHandle,
            params string[] descendantFolderName);
    }

    public static class ListDirectoryContentsChildExtensions
    {
        public static IEnumerable<IFileExplorerService.DirectoryContentsChild> GetFiles(this List<IFileExplorerService.DirectoryContentsChild> dirContents)
        {
            return dirContents.Where(child => child.Kind == "file");
        }
        public static IEnumerable<IFileExplorerService.DirectoryContentsChild> GetDirectories(this List<IFileExplorerService.DirectoryContentsChild> dirContents)
        {
            return dirContents.Where(child => child.Kind == "directory");
        }
    }
}
