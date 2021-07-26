using System;
using System.Text.Json;

namespace Debugging
{
    public class Program
    {
        public static void Main()
        {
            var myTicket = new Ticket
            {
                MovieTitle = "A Quiet Place",
                Price = 10.5f,
                ShowDate = new DateTime(2021, 7, 21, 12, 30, 0),
                HallNumber = 10
            };

            string jsonMyTicket = JsonSerializer.Serialize(myTicket);

            Console.WriteLine("My order: ");

            Console.WriteLine(jsonMyTicket);

            Console.WriteLine("Bye!!!");
        }
    }
}