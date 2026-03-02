using Ersk43.Malagarr.Web.Client.Controllers.Editor.Library;
using Ersk43.Malagarr.Web.Client.Events.Args;
using Ersk43.Malagarr.Web.Client.Services;
using Microsoft.JSInterop;
using ParseXmlDefinitions;
using ParseXmlDefinitions.Model;
using static Ersk43.Malagarr.Web.Client.Services.IFileExplorerService;
//using static Ersk43.Malagarr.Web.Client.Controllers.Editor.ModLoading;
using static Ersk43.Malagarr.Web.Client.Services.ListDirectoryContentsChildExtensions;

namespace Ersk43.Malagarr.Web.Client.Controllers.Editor.Mods
{

    public abstract class ModLoading
    {
        public string Id { get; }
        public ModKindEnum Kind { get; }

        public event EventHandler<FileCountChangeEventArgs>? FileCountChangeEvent;

        public event EventHandler<StateChangeEventArgs> StateChangeEvent;
        protected virtual void InvokeStateChange(object? sender, StateChangeEventArgs eventArgs)
        {
            StateChangeEvent?.Invoke(sender, eventArgs);
        }

        public event EventHandler<ModLoadingErrorEventArgs> ErrorEvent;
        protected virtual void InvokeError(object? sender, ModLoadingErrorEventArgs eventArgs)
        {
            ErrorEvent?.Invoke(sender, eventArgs);
        }

        private ModLoadingStateEnum state = ModLoadingStateEnum.CountFiles; // ModLoading.StepsOrder[0];
        public ModLoadingStateEnum State
        {
            get
            {
                return state;
            }
            protected set
            {
                ModLoadingStateEnum previousState = state;
                state = value;
                StateChangeEvent?.Invoke(this, new(previousState, state));
            }
        }

        protected IJSObjectReference modDir { get; }
        protected IJSObjectReference? defsDir { get; set; }

        protected readonly IFileExplorerService fileExplorerService;

        private int fileCount = 0;
        //public int FileCount => fileCount;
        public int FileCount
        {
            get
            {
                return fileCount;
            }
            set
            {
                fileCount = value;
                FileCountChangeEvent?.Invoke(this, new FileCountChangeEventArgs(fileCount));
            }
        }

        public Task? LoadingTask { get; protected set; }

        public ModLoading(
            string id,
            IFileExplorerService fileExplorerService,
            IJSObjectReference modDir,
            ModKindEnum kind)
        {
            Id = id;
            this.modDir = modDir;
            this.fileExplorerService = fileExplorerService;
            Kind = kind;
        }

    }

    public class NativeModLoading : ModLoading
    {
        public NativeModLoading(
            string id, 
            IFileExplorerService fileExplorerService, 
            IJSObjectReference modDir)
            : base(id, fileExplorerService, modDir, ModKindEnum.Native)
        {

        }

        public async Task LoadAsync()
        {
            LoadingTask = Task.Run(async () =>
            {
                // Get Defs directory
                defsDir = await fileExplorerService.GetChildDirectoryHandle(modDir, "Defs");

                TopLevelExploreDirectory topLevelDir = new("Defs", fileExplorerService, defsDir);
                await topLevelDir.LoadAsync();



                //await CountFilesInHierarchy(defsDir);

                //State = ModLoadingStateEnum.ParseDefTypes;

                //await ParseDefTypesInHierarchy(defsDir);


            });

            await LoadingTask;
        }


        private async Task CountFilesInHierarchy(IJSObjectReference dir)
        {
            // Get contents
            List<DirectoryContentsChild> contents = await fileExplorerService.GetDirectoryContents(dir);

            FileCount += contents.GetFiles().Count();

            IEnumerable<DirectoryContentsChild> directories = contents.GetDirectories();

            List<Task> countChildDirectoryTasks = new();
            foreach (var directoryContentsChild in directories)
            {
                IJSObjectReference childDir = await fileExplorerService.GetChildDirectoryHandle(dir, directoryContentsChild.Name!);
                Task countFilesInChildDir = CountFilesInHierarchy(childDir);
                countChildDirectoryTasks.Add(countFilesInChildDir);
            }

            if (countChildDirectoryTasks.Any())
            {
                await Task.WhenAll(countChildDirectoryTasks);
            }
        }

        private async Task ParseDefTypesInHierarchy(IJSObjectReference dir)
        {
            // Get contents
            List<DirectoryContentsChild> contents = await fileExplorerService.GetDirectoryContents(dir);

            List<Task<bool>> parseDefTypesInFileTasks = new();
            foreach (DirectoryContentsChild childContentFile in contents.GetFiles())
            {
                IJSObjectReference fileHandle = await fileExplorerService.GetChildDirectoryHandle(dir, childContentFile.Name!);
                Task<bool> parseDefTypesInFileTask = ParseDefTypesInFile(fileHandle);
                parseDefTypesInFileTasks.Add(parseDefTypesInFileTask);
            }

            IEnumerable<DirectoryContentsChild> directories = contents.GetDirectories();

            List<Task> parseDefTypesInHierarchyTasks = new();
            foreach (var directoryContentsChild in directories)
            {
                IJSObjectReference childDir = await fileExplorerService.GetChildDirectoryHandle(dir, directoryContentsChild.Name!);
                Task parseDefTypesInHierarchyTask = ParseDefTypesInHierarchy(childDir);
                parseDefTypesInHierarchyTasks.Add(parseDefTypesInHierarchyTask);
            }

            if (parseDefTypesInFileTasks.Any())
            {
                await Task.WhenAll(parseDefTypesInFileTasks);
            }
            if (parseDefTypesInHierarchyTasks.Any())
            {
                await Task.WhenAll(parseDefTypesInHierarchyTasks);
            }
         
        }

        private async Task<bool> ParseDefTypesInFile(IJSObjectReference fileHandle)
        {
            try
            {
                string fileText = await fileExplorerService.GetFileText(fileHandle);

                XmlDefFileReader reader = new(fileText, XmlDefFileReader.XmlDefFileReaderInputTypeEnum.FileText);
                List<DefElement> defs = reader.Parse();

                DefTypeLibrary library = new();
                library.ParseDefs(defs);

                var lib = library.library;
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }


}
