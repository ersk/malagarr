using Ersk43.Malagarr.Web.Client.Controllers.Editor.Mods;
using Ersk43.Malagarr.Web.Client.Services;
using Microsoft.JSInterop;
using static Ersk43.Malagarr.Web.Client.Services.IFileExplorerService;

namespace Ersk43.Malagarr.Web.Client.Controllers.Editor.Controller
{
    public class EditorController
    {
        public event EventHandler<LocateModEventArgs>? LocateModEvent;

        private IFileExplorerService fileExplorerService;

        
        private Dictionary<string, NativeModLoading> nativeMods = new();
        public IDictionary<string, NativeModLoading> NativeMods => nativeMods;
        public NativeModLoading? coreMod => nativeMods.ContainsKey("Core") ? nativeMods["Core"] : null;
  
        public EditorController(IFileExplorerService fileExplorerService)
        {
            this.fileExplorerService = fileExplorerService;
        }

        /* Steps
         * ---------------
         * 1. Locate RimWorld directory
         * 
         * 
         * 
         */

        // C:\Program Files (x86)\Steam\steamapps\common\RimWorld\
        //
        // Mods
        // Data/Core/Defs


        private IJSObjectReference? rimworldJSDirectoryHandle = null;

        public async Task LoadNativeRimworldModsAsync(IJSObjectReference jsDirectoryHandle)
        {
            // Get Data directory
            IJSObjectReference dataDir = await fileExplorerService.GetDescendantDirectory(jsDirectoryHandle, "Data");

            // Read contents of Data directory
            // to see which expansions exist
            List<DirectoryContentsChild> dataDirContents = await fileExplorerService.GetDirectoryContents(dataDir);

            await LoadCoreAsync(dataDir, dataDirContents);

            await LoadExpansionsAsync(dataDir, dataDirContents);
        }

        private async Task LoadCoreAsync(IJSObjectReference dataDir, List<DirectoryContentsChild> dataDirContents)
        {
            // Check core exists
            DirectoryContentsChild? coreDirChild = dataDirContents.FirstOrDefault(child => child.Name == "Core" && child.Kind == "directory");
            if (coreDirChild == null)
            {
                throw new Exception("Failed to find 'Core' directory in the RimWorld directory provided.");
            }

            await LoadModAsync(dataDir, "Core");
        }

        private async Task LoadExpansionsAsync(IJSObjectReference dataDir, List<DirectoryContentsChild> dataDirContents)
        {
            List<Task> tasks = new List<Task>();    

            // Check core exists
            foreach(DirectoryContentsChild dirChild in dataDirContents)
            {
                if (dirChild.Name == "Core") continue;

                if (dirChild.Kind == "file") continue;

                Task task = LoadModAsync(dataDir, dirChild.Name!);
                tasks.Add(task);
            }

            await Task.WhenAll(tasks);
        }

        private async Task LoadModAsync(IJSObjectReference dataDir, string directoryModName)
        {     
            // Get Core directory
            IJSObjectReference modDir = await fileExplorerService.GetDescendantDirectory(dataDir, directoryModName);

            NativeModLoading nativeMod = new(directoryModName, fileExplorerService, modDir);

            nativeMods.Add(directoryModName, nativeMod);

            LocateModEventArgs eventArgs = new(nativeMod);
            LocateModEvent?.Invoke(this, eventArgs);

            await nativeMod.LoadAsync();
        }


        
    }
}
