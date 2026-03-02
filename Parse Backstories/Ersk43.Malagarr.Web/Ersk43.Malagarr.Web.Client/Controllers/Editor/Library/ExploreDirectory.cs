using Ersk43.Malagarr.Web.Client.Services;
using Microsoft.JSInterop;

namespace Ersk43.Malagarr.Web.Client.Controllers.Editor.Library
{

    public class ExploreDirectory : IExploreDirectory
    {
        public string Name { get; private set; }

        public IExploreDirectory ParentDirectory { get; private set; }

        private List<ExploreDirectory> childDirectories = new();
        public IEnumerable<ExploreDirectory> ChildDirectories => childDirectories;

        private List<ExploreFile> childFiles = new();
        public IEnumerable<ExploreFile> ChildFiles => childFiles;

        private readonly IFileExplorerService fileExplorerService;


        public ExploreDirectory(
            string name,
            IFileExplorerService fileExplorerService,
            IExploreDirectory parentDirectory
            //IEnumerable<ExploreDirectory> childDirectories,
            //IEnumerable<ExploreFile> childFiles
            )
        {
            Name = name;
            this.fileExplorerService = fileExplorerService;
            ParentDirectory = parentDirectory;
            //this.childDirectories = childDirectories.ToList();
            //this.childFiles = childFiles.ToList();
        }

        public async Task<IJSObjectReference> GetDirectoryHandleAsync()
        {
            IJSObjectReference parentDirHandle = await ParentDirectory.GetDirectoryHandleAsync();

            IJSObjectReference dirHandle = await fileExplorerService.GetChildDirectoryHandle(parentDirHandle, Name);

            return dirHandle;
        }

        public bool HasBeenParsed
        {
            get
            {
                foreach (var childDir in ChildDirectories)
                {
                    if (childDir.HasBeenParsed == false)
                    {
                        return false;
                    }
                }

                foreach (var childFile in ChildFiles)
                {
                    if (childFile.HasBeenParsed == false)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public Task LoadAsync()
        {
            throw new NotImplementedException();
        }

    
    }

}
