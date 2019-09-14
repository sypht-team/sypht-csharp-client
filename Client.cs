using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Sypht
{
    class Client
    {
        private String SYPHT_URL = "https://api.sypht.com";
        private Auth0Helper auth0Helper = null;
        private HttpClient httpClient = null;
        public Client()
        {
            this.auth0Helper = new Auth0Helper();
            this.httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(SYPHT_URL);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<String> upload(String fileName)
        {
            byte[] file_bytes = File.ReadAllBytes(fileName);
            
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await auth0Helper.login());
            
            MultipartFormDataContent form = new MultipartFormDataContent();
            form.Add(new StringContent("[\"sypht.invoice\"]", Encoding.UTF8), "fieldSets");
            form.Add(new ByteArrayContent(file_bytes, 0, file_bytes.Length), "fileToUpload", "file.pdf");
            HttpResponseMessage response = await httpClient.PostAsync("/fileupload", form);
            response.EnsureSuccessStatusCode();

            var syphtResponse = await response.Content.ReadAsStringAsync();
            return JObject.Parse(syphtResponse)["fileId"].ToObject<string>();
        }
        
        
    }
}