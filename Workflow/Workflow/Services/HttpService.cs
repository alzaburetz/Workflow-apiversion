using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Workflow.Services
{
    public class HttpService : IHttpService
    {
        public HttpClient Client { get; set; }
        public string Token { get; set; }
        public HttpService()
        {
            TrySetToken();
            this.Client = new HttpClient()
            {
                BaseAddress = new System.Uri("https://workflow-2020.herokuapp.com/api")
            };
        }
        private void TrySetToken()
        {
            var token = Application.Current.Properties["Token"];
            if (token == null || string.IsNullOrEmpty(token.ToString()))
            {
                Token = string.Empty;
            } 
            else
            {
                Token = token.ToString();
            }
        }
        public Task<T> DeleteRequest<T>(string endpoint)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetRequest<T>(string endpoint)
        {
            throw new NotImplementedException();
        }

        public Task<T> PostRequest<T, D>(string endpoint, D data, bool token)
        {
            throw new NotImplementedException();
        }

        public Task<T> PutRequest<T, D>(string endpoint, D data)
        {
            throw new NotImplementedException();
        }
    }
}
