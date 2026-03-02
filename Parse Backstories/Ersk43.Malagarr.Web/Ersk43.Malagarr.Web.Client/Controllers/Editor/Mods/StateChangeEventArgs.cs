namespace Ersk43.Malagarr.Web.Client.Controllers.Editor.Mods
{
    public class StateChangeEventArgs
    {
        public ModLoadingStateEnum PreviousState { get; }
        public ModLoadingStateEnum NewState { get; }
        public StateChangeEventArgs(ModLoadingStateEnum previousState, ModLoadingStateEnum newState)
        {
            PreviousState = previousState;
            NewState = newState;
        }
    }
}
