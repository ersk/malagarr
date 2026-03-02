namespace Ersk43.Malagarr.Web.Client.Events.Args.ListenableList
{
    public class InsertEventArgs<T> : EventArgs
    {
        public T Item { get; }
        public int Index { get; }

        public InsertEventArgs(int index, T addedItem)
        {
            Index = index;
            Item = addedItem;
        }
    }
}
