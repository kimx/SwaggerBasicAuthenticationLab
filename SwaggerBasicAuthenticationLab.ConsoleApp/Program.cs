// See https://aka.ms/new-console-template for more information

using SwaggerBasicAuthenticationLab.ConsoleApp.Clients;

HttpClient httpClient = new HttpClient();
httpClient.BaseAddress = new Uri("https://localhost:7172/");
GakClient client = new GakClient(new ClientConfiguration(), httpClient);
client.Busy = new BusyControl();
var list = await client.GetListAsync();
foreach (var item in list)
{
    Console.WriteLine(item.Date);
}
Console.WriteLine("Hello, World!");
//DuotifyServiceClient duotifyServiceClient = new DuotifyServiceClient(new HttpClient());
