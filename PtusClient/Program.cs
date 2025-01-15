using PtusClient;

using var httpClient = new HttpClient();
var ptusServiceClient = new PtusServiceClient(httpClient);

Console.WriteLine("Provide your username");
var username = Console.ReadLine();

await ProgramLoop();

async Task ProgramLoop()
{
    while (true)
    {
        SendMessage();
        await GetSocialPoints();
    }
}

void SendMessage()
{
    Console.WriteLine("Enter your message to the CCP");
    var message = Console.ReadLine();
    var task = ptusServiceClient.SendMessageAsync(username, message);
    Console.Write("Awaiting response ");
    while (!task.IsCompleted)
    {
        Console.Write(".");
        Thread.Sleep(1000);
    }
    Console.WriteLine($"\nYou received {task.Result} social points!");
}

async Task GetSocialPoints()
{
    var socialPointsTable = await ptusServiceClient.GetSocialPointsAsync();
    Console.WriteLine("Current social points:");
    Console.WriteLine("{0, -20} Points", "Username");
    foreach (var record in socialPointsTable)
    {
        Console.WriteLine($"{record.Key, -20} {record.Value}");
    }
    Console.WriteLine();
}