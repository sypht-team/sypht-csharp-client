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
        private string SYPHT_URL = "https://api.sypht.com";
        private OAuthHelper oauthHelper = null;
        private HttpClient httpClient = null;
        public Client()
        {
            this.oauthHelper = new OAuthHelper();
            this.httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(SYPHT_URL);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> upload(String fileName, params string[] fieldSets)
        {
            string formattedFieldSets = "[\"" + string.Join("\", \"", fieldSets) + "\"]";
            if (fieldSets.Length == 0)
            {
                formattedFieldSets = "[\"sypht.document\"]";
            }

            byte[] file_bytes = File.ReadAllBytes(fileName);
            
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await oauthHelper.login());
            
            MultipartFormDataContent form = new MultipartFormDataContent();
            form.Add(new StringContent(formattedFieldSets, Encoding.UTF8), "fieldSets");
            form.Add(new ByteArrayContent(file_bytes, 0, file_bytes.Length), "fileToUpload", fileName);
            HttpResponseMessage response = await httpClient.PostAsync("/fileupload", form);
            response.EnsureSuccessStatusCode();

            var syphtResponse = await response.Content.ReadAsStringAsync();
            return JObject.Parse(syphtResponse)["fileId"].ToObject<string>();
        }

        public async Task<string> result(String fileId)
        {
            HttpResponseMessage response = await httpClient.GetAsync("/result/final/" + fileId);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

    }
}
