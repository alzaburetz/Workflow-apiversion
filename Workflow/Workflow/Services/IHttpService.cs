using System;
using System.Collections.Generic;
using System.Text;

using System.Threading.Tasks;
using System.Net.Http;

namespace Workflow.Services
{
    public interface IHttpService
    {
        HttpClient Client { get; set; }
        string Token { get; set;}
        Task<T> GetRequest<T>(string endpoint);
        Task<T> GetRequestWithBody<T, D>(string endpoint, D data);
        Task<T> PostRequest<T, D>(string endpoint, D data, bool token);
        Task<T> PutRequest<T, D>(string endpoint, D data);
        Task<T> DeleteRequest<T>(string endpoint);
        Task<T> UploadFile<T>(string endpoint, byte[] file, string type = "avatar");

    }
}
