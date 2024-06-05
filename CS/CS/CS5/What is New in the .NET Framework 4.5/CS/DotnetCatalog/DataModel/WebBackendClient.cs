using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DotnetCatalog.DataModel
{
    internal class WebBackendClient : HttpClient
    {
        //change me to match the localhost in the WebBackend properites
        private const string endpointRoot = @"http://localhost:30847/api";

        public WebBackendClient()
        {
            MaxResponseContentBufferSize = 1024 * 1024;
        }

        public async Task<T> Download<T>(string relativePath)
        {
            string response = await GetStringAsync(endpointRoot + relativePath);
            return JsonConvert.DeserializeObject<T>(response);
        }
    }
}
