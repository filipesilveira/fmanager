using System;
using System.Web;
using FManager.Core.Helpers;

namespace FManager.Web.Helpers
{
    public class SessionHelper : ISessionHelper
    {
        /// <inheritdoc/>
        public Guid GetSessionToken()
        {
            if (Guid.TryParse((string)this.GetFromSession("Token"), out Guid sessionToken))
            {
                return sessionToken;
            }

            return Guid.Empty;
        }

        /// <summary>
        /// Read the session in threadsafe mode
        /// </summary>
        /// <param name="key">The session index</param>
        /// <returns>the value inside the session index</returns>
        public object GetFromSession(string key)
        {
            if (HttpContext.Current != null)
            {
                lock (HttpContext.Current)
                {
                    if (HttpContext.Current.Session != null)
                    {
                        return HttpContext.Current.Session[key];
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Insert the session in threadsafe mode
        /// </summary>
        /// <param name="key">The session index</param>
        /// <param name="value">The value to insert</param>
        public void SetToSession(string key, object value)
        {
            if (HttpContext.Current != null)
            {
                lock (HttpContext.Current)
                {
                    if (HttpContext.Current.Session != null)
                    {
                        HttpContext.Current.Session[key] = value;
                    }
                }
            }
        }

        /// <summary>
        /// Clear all the items from session.
        /// </summary>
        public void ClearSession()
        {
            if (HttpContext.Current != null)
            {
                lock (HttpContext.Current)
                {
                    if (HttpContext.Current.Session != null)
                    {
                        HttpContext.Current.Session.Clear();
                    }
                }
            }
        }
    }
}