using Ersk43.Malagarr.Web.Client.Services;

namespace Ersk43.Malagarr.Web.Client.Controllers.Editor.Library
{
    public class DefInstance
    {

    }

    public class ExploreFile
    {
        public string Name { get; private set; }
        private IFileExplorerService fileExplorerService;
        public IExploreDirectory Parent { get; private set; }
        public bool HasBeenParsed { get; private set; } = false;

        private List<DefInstance> defInstances = new();
        public IEnumerable<DefInstance> DefInstances => defInstances;

        public ExploreFile(string name, IFileExplorerService fileExplorerService, IExploreDirectory parent)
        {
            Name = name;
            this.fileExplorerService = fileExplorerService;
            Parent = parent;
        }
    }
}
