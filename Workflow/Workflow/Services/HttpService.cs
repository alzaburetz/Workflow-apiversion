using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Newtonsoft.Json;
using System.IO;

namespace Workflow.Services
{
    public class HttpService : IHttpService
    {
        public HttpClient Client { get; set; }
        public string Token { get; set; }
        public JsonSerializerSettings SerializerSettings { get; set; }
        public HttpService()
        {
            TrySetToken();
            this.Client = new HttpClient()
            {
                BaseAddress = new System.Uri("https://workflow-2020.herokuapp.com/api")
            };
            this.Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            SerializerSettings = new JsonSerializerSettings()
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            };
        }
        private void TrySetToken()
        {
            try
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
            catch { }
        }
        public Task<T> DeleteRequest<T>(string endpoint)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetRequest<T>(string endpoint)
        {
                if (!this.Client.DefaultRequestHeaders.Contains("Token"))
                {
                    this.Client.DefaultRequestHeaders.Add("Token", this.Token);
                }
            var response = await Client.GetAsync(string.Concat("api/", endpoint));
            var body = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(body);
        }

        public async Task<T> PostRequest<T, D>(string endpoint, D data, bool token = false)
        {
            if (token)
            {
                if (!this.Client.DefaultRequestHeaders.Contains("Token"))
                {
                    this.Client.DefaultRequestHeaders.Add("Token", this.Token);
                }
            }
            var form = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(string.Concat("api/",endpoint), form);
            var body = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(body);
        }

        public async Task<T> PutRequest<T, D>(string endpoint, D data)
        {
                if (!this.Client.DefaultRequestHeaders.Contains("Token"))
                {
                    this.Client.DefaultRequestHeaders.Add("Token", this.Token);
                }
            var form = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var response = await Client.PutAsync(string.Concat("api/", endpoint), form);
            var body = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(body);
        }

        public async Task<T> UploadFile<T>(string endpoint, byte[] file, string type = "avatar")
        {
            if (!this.Client.DefaultRequestHeaders.Contains("Token"))
            {
                this.Client.DefaultRequestHeaders.Add("Token", this.Token);
            }
            var image = new MemoryStream(file);
            var content = new StreamContent(image);
            var form = new MultipartFormDataContent();
            form.Add(content, type, "file.jpg");

            var response = await Client.PostAsync(string.Concat("api/", endpoint), form);
            var body = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(body);
        }

        public async Task<T> GetRequestWithBody<T, D>(string endpoint, D data)
        {
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(Client.BaseAddress,string.Concat("api/",endpoint)),
                Content = new StringContent(JsonConvert.SerializeObject(data))
            };
            if (!this.Client.DefaultRequestHeaders.Contains("Token"))
            {
                this.Client.DefaultRequestHeaders.Add("Token", this.Token);
            }
            var response = await Client.SendAsync(request);
            var body = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(body);
        }
    }
}
