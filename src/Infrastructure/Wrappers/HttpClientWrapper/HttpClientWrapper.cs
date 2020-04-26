using ApplicationCore.Messages;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Infrastructure.Wrappers.HttpClientWrapper
{
    public class HttpClientWrapper: IHttpClientWrapper
    {
        HttpClient client = new HttpClient();
        public async Task<HttpResponseMessage> PostMessage(string requestUri, MessageBase message)
        {
           return await client.PostAsync(requestUri, PrepareMessage(message));
        }

        private HttpContent PrepareMessage(MessageBase message)
        {
            return new StringContent(JsonSerializer.Serialize(message), Encoding.UTF8, "application/json");
        }
    }
}
