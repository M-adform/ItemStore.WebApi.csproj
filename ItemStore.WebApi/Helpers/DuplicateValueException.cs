namespace ItemStore.WebApi.csproj.Helpers
{
    public class DuplicateValueException : Exception
    {
        public DuplicateValueException(string message) : base(message) { }
    }
}
