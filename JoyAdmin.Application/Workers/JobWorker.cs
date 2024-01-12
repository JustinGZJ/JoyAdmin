
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Furion.TaskScheduler;

namespace JoyAdmin.Application.Workers;

public class JobWorker : ISpareTimeWorker
{
    [SpareTime(5000, "UpdateWorkOrder", StartNow = true)]
    public async Task UpdateWorkOrder(SpareTimer timer, long count)
    {
        using var client = new HttpClient();
        // Set the base address if you're going to make several requests to the same domain
        client.BaseAddress = new Uri("http://127.0.0.1:9001");

        // Add the Authorization header with the bearer token
        // client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

        // Prepare the content of the request
        // var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

        // Make the POST request
        var response = await client.PostAsync("/api/WorkOrder/UpdateActiveWorkOrder", null);

        // Ensure we got a successful response
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine("Error: " + response.StatusCode);
        }
        else
        {
            // Assuming the response is in JSON format
            string responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Response received successfully!");
            Console.WriteLine(responseBody);
        }
    }
}