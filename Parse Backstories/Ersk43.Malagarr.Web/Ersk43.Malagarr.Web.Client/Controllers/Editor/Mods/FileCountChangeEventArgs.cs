namespace Ersk43.Malagarr.Web.Client.Controllers.Editor.Mods
{
    public class FileCountChangeEventArgs
    {
        public int FileCount { get; }
        public FileCountChangeEventArgs(int fileCount)
        {
            FileCount = fileCount;
        }
    }
}
