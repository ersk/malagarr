using Ersk43.Malagarr.Web.Client.Services;
using Microsoft.JSInterop;
using static Ersk43.Malagarr.Web.Client.Services.IFileExplorerService;

namespace Ersk43.Malagarr.Web.Client.Controllers.Editor.Library
{
    public class TopLevelExploreDirectory : IExploreDirectory
    {
        public string Name { get; private set; }

        private readonly IJSObjectReference dirHandle;

        private List<ExploreDirectory> childDirectories = new();
        public IEnumerable<ExploreDirectory> ChildDirectories => childDirectories;

        private List<ExploreFile> childFiles = new();
        public IEnumerable<ExploreFile> ChildFiles => childFiles;

        private readonly IFileExplorerService fileExplorerService;


        public TopLevelExploreDirectory(
            string name,        
            IFileExplorerService fileExplorerService,
            IJSObjectReference dirHandle
            //IEnumerable<ExploreDirectory> childDirectories,
            //IEnumerable<ExploreFile> childFiles
            )
        {
            Name = name;
            this.fileExplorerService = fileExplorerService;
            this.dirHandle = dirHandle;
            //this.childDirectories = childDirectories.ToList();
            //this.childFiles = childFiles.ToList();
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

        public async Task<IJSObjectReference> GetDirectoryHandleAsync()
        {
            return dirHandle;
        }

        public async Task LoadAsync()
        {
            // Get contents
            List<DirectoryContentsChild> contents = await fileExplorerService.GetDirectoryContents(dirHandle);


            IEnumerable<DirectoryContentsChild> directories = contents.GetDirectories();
            List<Task> loadChildExploreDirTasks = new();
            foreach (var directoryContentsDirChild in directories)
            {
                ExploreDirectory childExploreDir = new(directoryContentsDirChild.Name!, fileExplorerService, this);
                childDirectories.Add(childExploreDir);

                Task loadChildExploreDirTask = childExploreDir.LoadAsync();
                loadChildExploreDirTasks.Add(loadChildExploreDirTask);
            }

            IEnumerable<DirectoryContentsChild> files = contents.GetFiles();
            List<Task> loadChildExploreFileTasks = new();
            foreach (var directoryContentsFileChild in files)
            {
                ExploreFile childExploreDir = new(directoryContentsFileChild.Name!, fileExplorerService, this);
                childDirectories.Add(childExploreDir);

                Task loadChildExploreDirTask = childExploreDir.LoadAsync();
                loadChildExploreDirTasks.Add(loadChildExploreDirTask);
            }

            if (loadChildExploreDirTasks.Any())
            {
                await Task.WhenAll(loadChildExploreDirTasks);
            }
        }

    }
}
