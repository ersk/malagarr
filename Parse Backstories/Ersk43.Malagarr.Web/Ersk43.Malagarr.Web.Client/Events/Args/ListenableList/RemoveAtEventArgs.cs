namespace Ersk43.Malagarr.Web.Client.Events.Args.ListenableList
{
    public class RemoveAtEventArgs<T> : EventArgs
    {
        public T Item { get; }
        public int Index { get; }

        public RemoveAtEventArgs(int index, T addedItem)
        {
            Index = index;
            Item = addedItem;
        }
    }
}
