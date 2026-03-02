using Ersk43.Malagarr.Web.Client.Services;
using Microsoft.JSInterop;

namespace Ersk43.Malagarr.Web.Client.Controllers.Editor.Library
{
    public interface IExploreDirectory
    {
        string Name { get;  }

        IEnumerable<ExploreDirectory> ChildDirectorie => throw new NotImplementedException();

        IEnumerable<ExploreFile> ChildFiles => throw new NotImplementedException();

        Task<IJSObjectReference> GetDirectoryHandleAsync();
        bool HasBeenParsed => throw new NotImplementedException();
        bool HasBeenLoaded => throw new NotImplementedException();

        Task LoadAsync();
    }

}
