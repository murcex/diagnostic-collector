namespace PlyQor.Storage.Interfaces
{
    internal interface IRetentionManager
    {
        public bool Execute(string container, int range);
    }
}
