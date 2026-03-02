namespace Ersk43.Malagarr.Web.Client.Events.Args.ListenableList
{
    public class RemoveEventArgs<T> : EventArgs
    {
        public T Item { get; }

        public RemoveEventArgs(T item)
        {
            Item = item;
        }
    }
}
