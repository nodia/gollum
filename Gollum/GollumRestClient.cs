using System;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;

namespace Aidon.Tools.Gollum
{
    /// <summary>
    /// A base class for rest clients that use cookies
    /// </summary>
    public abstract class GollumRestClient
    {

        /// <summary>
        /// The default timeout for IO using the REST client.
        /// </summary>
        protected static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(60);

        /// <summary>
        /// The REST client.
        /// </summary>
        protected readonly RestClient Client;

        /// <summary>
        /// Used to determine if we need to get the cookie or if we already have it saved.
        /// </summary>
        private bool HasCookie { get; set; }

        /// <summary>
        /// Directory name in the isolated storage.
        /// </summary>
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
        protected bool ReadCookies()
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
        private CookieCollection ReadCookiesFromFile()
        {
            try
            {
                var cookies = new CookieCollection();
                var formatter = new BinaryFormatter();
                using (var isolatedStorageFile = IsolatedStorageFile.GetUserStoreForAssembly())
                {
                    if (!isolatedStorageFile.DirectoryExists(_directory))
                    {
                        return null;
                    }

                    var path = _directory + "\\*";
                    var files = isolatedStorageFile.GetFileNames(path);

                    foreach (var file in files.Where(file => file != null))
                    {
                        var fullName = Path.Combine(_directory, file);
                        using (var isolatedStorageFileStream = isolatedStorageFile.OpenFile(fullName, FileMode.Open, FileAccess.Read))
                        {
                            cookies.Add((Cookie)formatter.Deserialize(isolatedStorageFileStream));
                        }
                    }

                    return cookies;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                ClearCookieFile();
                return null;
            }
        }

        /// <summary>
        /// Clears the cookie files from the specified directory.
        /// </summary>
        protected void ClearCookieFile()
        {
            try
            {
                using (var isolatedStorageFile = IsolatedStorageFile.GetUserStoreForAssembly())
                {
                    if (!isolatedStorageFile.DirectoryExists(_directory))
                    {
                        return;
                    }

                    var files = isolatedStorageFile.GetFileNames(_directory + "\\*");
                    foreach (var file in files.Where(file => file != null))
                    {
                        var fullName = Path.Combine(_directory, file);
                        if (isolatedStorageFile.FileExists(fullName))
                        {
                            isolatedStorageFile.DeleteFile(fullName);
                        }
                    }
                    isolatedStorageFile.DeleteDirectory(_directory);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// Gets the cookie from the reponse, sets it to the cookie container and saves it to storage file.
        /// </summary>
        /// <param name="response">The response.</param>
        protected void ProcessResponseCookies(IRestResponse response)
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
        private void SaveCookiesToFile(CookieCollection cookies)
        {
            try
            {
                var formatter = new BinaryFormatter();
                using (var isolatedStorageFile = IsolatedStorageFile.GetUserStoreForAssembly())
                {


                    if (!isolatedStorageFile.DirectoryExists(_directory))
                    {
                        isolatedStorageFile.CreateDirectory(_directory);
                    }

                    for (int i = 0; i < cookies.Count; i++)
                    {
                        var cookie = cookies[i];

                        using (var isolatedStorageFileStream = isolatedStorageFile.OpenFile(Path.Combine(_directory, cookie.Name), FileMode.Create, FileAccess.Write))
                        {
                            formatter.Serialize(isolatedStorageFileStream, cookie);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// Executes the request and callback asynchronously.
        /// </summary>
        /// <remarks>
        /// See pull request in GitHub:
        /// https://github.com/restsharp/RestSharp/pull/367
        /// </remarks>
        /// <param name="request">Request to be executed</param>
        /// <param name="token">The cancellation token</param>
        /// <returns>
        /// A task that represents the asynchronous request. 
        /// The Result property of the task contains the <see cref="IRestResponse"/> when the request completes. 
        /// </returns>
        /// <exception cref="OperationCanceledException">
        /// Thrown if cancellation is requested before entering this method.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// Thrown if <paramref name="token"/> is being disposed of.
        /// </exception>
        protected Task<IRestResponse> ExecuteAsync(IRestRequest request, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            var taskCompletionSource = new TaskCompletionSource<IRestResponse>();

            try
            {
                var async = Client.ExecuteAsync(request, (response, handle) =>
                {
                    if (token.IsCancellationRequested)
                    {
                        // do nothing since token has a registered callback to handle cancellation
                        return; 
                    }

                    if (response.ErrorException == null)
                    {
                        if (response.ResponseStatus == ResponseStatus.Completed)
                        {
                            taskCompletionSource.TrySetResult(response);
                        }
                        else
                        {
                            taskCompletionSource.TrySetException(
                                new InvalidOperationException(String.Format("An error occurred: received status code '{0}', response status '{1}' response from the server.", 
                                                                            response.StatusCode, response.ResponseStatus)));
                        }
                    }
                    else
                    {
                        taskCompletionSource.TrySetException(response.ErrorException);
                    }
                });

                token.Register(() =>
                {
                    taskCompletionSource.TrySetCanceled();
                    async.Abort();
                });
            }
            catch (Exception ex)
            {
                taskCompletionSource.TrySetException(ex);
            }

            return taskCompletionSource.Task;
        }
    }
}
