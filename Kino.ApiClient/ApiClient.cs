using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Kino.ApiClient;

public partial class ApiClient
{
    private readonly HttpClient _client;
    private string _authPart = string.Empty;
    private int? _userId;

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