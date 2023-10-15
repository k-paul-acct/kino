using Kino.ApiClient;

var client = new ApiClient("http://localhost:8080/api/");

var loginRes = await client.Login("loggable", "loggable");
Console.WriteLine(loginRes);

var registerRes = await client.Register("string", "string", "string");
Console.WriteLine(registerRes);