using System;

namespace FManager.Core.Helpers
{
    public interface ISessionHelper
    {
        Guid GetSessionToken();

        object GetFromSession(string key);

        void SetToSession(string key, object value);

        void ClearSession();
    }
}
