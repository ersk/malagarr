using Ersk43.Malagarr.Web.Client.Controllers.Editor.Mods;

namespace Ersk43.Malagarr.Web.Client.Controllers.Editor.Controller
{
    public class LocateModEventArgs
    {
        public ModLoading ModLoading { get; }
        public LocateModEventArgs(ModLoading modLoading)
        {
            ModLoading = modLoading;
        }
    }
}
