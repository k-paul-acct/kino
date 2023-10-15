using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Kino.ApiClient;

public partial class ApiClient
{
    private readonly HttpClient _client;

    public ApiClient(string baseAddress)
    {
        _client = new HttpClient
        {
            BaseAddress = new Uri(baseAddress),
        };

        JsonConvert.DefaultSettings = () => new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
        };
    }
}