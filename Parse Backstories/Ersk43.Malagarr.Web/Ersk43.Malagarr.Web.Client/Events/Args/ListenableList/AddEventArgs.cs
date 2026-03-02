namespace Ersk43.Malagarr.Web.Client.Events.Args.ListenableList
{
    public class AddEventArgs<T> : EventArgs
    {
        public T Item { get; }

        public AddEventArgs(T item)
        {
            Item = item;
        }
    }
}
