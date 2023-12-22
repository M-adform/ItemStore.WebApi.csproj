namespace ItemStore.WebApi.csproj.Helpers
{
    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException(string message) : base(message) { }
    }
}
