using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Kino.ApiClient;

public partial class ApiClient
{
    private readonly HttpClient _client;
    private int? _userId;
    private string _authPart = string.Empty;

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