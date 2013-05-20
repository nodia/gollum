using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using RestSharp;

namespace Aidon.Tools.Gollum
{
    /// <summary>
    /// A base class for rest clients that use cookies
    /// </summary>
    public abstract class GollumRestClient
    {
        /// <summary>
        /// The REST client.
        /// </summary>
        public RestClient Client;

        /// <summary>
        /// Used to determine if we need to get the cookie or if we already have it saved.
        /// </summary>
        public bool HasCookie { get; private set; }

        private readonly string _directory;

        /// <summary>
        /// Initializes a new instance of the <see cref="GollumRestClient" /> class.
        /// </summary>
        /// <param name="directory">The cookie directory.</param>
        /// <param name="clientUrl">The client URL.</param>
        protected GollumRestClient(string directory, string clientUrl)
        {
            Client = new RestClient { BaseUrl = clientUrl };
            _directory = directory;
        }

        /// <summary>
        /// Checks if a valid cookies already exist on this machine and sets them to the client cookie container. 
        /// </summary>
        /// <returns>True if a cookie or cookies exist and are not expired.</returns>
        public bool ReadCookies()
        {
            var cookies = ReadCookiesFromFile();
            if (cookies != null && cookies.Count > 0)
            {
                if (cookies.Cast<Cookie>().Any(cookie => cookie.Expired))
                {
                    ClearCookieFile();
                    return false;
                }

                Client.CookieContainer = new CookieContainer();
                Client.CookieContainer.Add(cookies);
                HasCookie = true;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Reads the isolated storage file to see if there is a cookie with the given name saved in it.
        /// </summary>
        /// <returns> Cookie, null if not found. </returns>
        public CookieCollection ReadCookiesFromFile()
        {
            try
            {
                var cookies = new CookieCollection();
                var formatter = new BinaryFormatter();
                using (var isolatedStorageFile = IsolatedStorageFile.GetUserStoreForAssembly())
                {
                    var files = isolatedStorageFile.GetFileNames(_directory + "/*");

                    foreach (var file in files.Where(file => file != null))
                    {
                        using (var isolatedStorageFileStream = isolatedStorageFile.OpenFile(_directory + "/" + file, FileMode.Open, FileAccess.Read))
                        {
                            cookies.Add((Cookie)formatter.Deserialize(isolatedStorageFileStream));
                        }
                    }

                    return cookies;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Clears the cookie files from the specified directory.
        /// </summary>
        public void ClearCookieFile()
        {
            using (var isolatedStorageFile = IsolatedStorageFile.GetUserStoreForAssembly())
            {
                var files = isolatedStorageFile.GetFileNames(_directory + "/*");

                foreach (var file in files.Where(file => file != null))
                {
                    isolatedStorageFile.DeleteFile(file);
                }

                isolatedStorageFile.DeleteDirectory(_directory);
            }
        }

        /// <summary>
        /// Gets the cookie from the reponse, sets it to the cookie container and saves it to storage file.
        /// </summary>
        /// <param name="response">The response.</param>
        public void ProcessResponseCookies(RestResponse response)
        {
            if (HasCookie || response == null)
            {
                return;
            }

            if (response.Cookies.Count == 0)
            {
                return;
            }

            var cookies = new CookieCollection();
            foreach (var restResponseCookie in response.Cookies)
            {
                cookies.Add(new Cookie
                    {
                        Name = restResponseCookie.Name,
                        Value = restResponseCookie.Value,
                        Path = restResponseCookie.Path,
                        Domain = restResponseCookie.Domain,
                        Expires = restResponseCookie.Expires,
                        Expired = restResponseCookie.Expired
                    });
            }

            Client.CookieContainer = new CookieContainer();
            Client.CookieContainer.Add(cookies);
            Client.Authenticator = null;

            HasCookie = true;

            // Failing to save the cookie to a file doesn't prevent from using the application, but will make it prompt for credentials every time.
            SaveCookiesToFile(cookies);
        }

        /// <summary>
        /// Saves the cookie to a file isolated storage file. Only one cookie can be saved to the file.
        /// </summary>
        /// <param name="cookies">Cookies to save</param>
        /// <returns>True if succesful, false otherwise</returns>
        public bool SaveCookiesToFile(CookieCollection cookies)
        {
            try
            {
                var formatter = new BinaryFormatter();
                using (var isolatedStorageFile = IsolatedStorageFile.GetUserStoreForAssembly())
                {
                    if (isolatedStorageFile.GetDirectoryNames(_directory).Length == 0)
                    {
                        isolatedStorageFile.CreateDirectory(_directory);
                    }

                    for (int i = 0; i < cookies.Count; i++)
                    {
                        var cookie = cookies[i];

                        using (var isolatedStorageFileStream = isolatedStorageFile.OpenFile(_directory + "/" + cookie.Name, FileMode.Create, FileAccess.Write))
                        {
                            formatter.Serialize(isolatedStorageFileStream, cookie);
                        }
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
