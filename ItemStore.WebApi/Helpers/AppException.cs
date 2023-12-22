using System.Globalization;

namespace ItemStore.WebApi.csproj.Helpers
{
    public class AppException : Exception
    {
        public AppException(string message) : base(message) { }

        public AppException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args)) { }
    }
}
