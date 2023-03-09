using Datalagring_Assignment.Services;
using Datalagring_Assignment.Models;
using Microsoft.EntityFrameworkCore;
using Datalagring_Assignment.Contexts;
using Datalagring_Assignment.Models.Entities;

namespace Datalagring_Assignment.Services
{
    internal class MenuService
    {


        public async Task CreateNewErrandAsync()
        {
            var errand = new Errand();

            Console.WriteLine("Insert Full Customer Name");
            errand.CustomerName = Console.ReadLine() ?? "";

            Console.WriteLine("Insert Customer Phone Number");
            errand.Phone = Console.ReadLine() ?? "";


            Console.WriteLine("Insert Customer E-mail Address");
            errand.Email = Console.ReadLine() ?? "";


            Console.WriteLine("Describe Your Error");
            errand.Description = Console.ReadLine() ?? "";

            errand.ErrandDateTime = DateTime.Now;
            Console.WriteLine("Errand Created");


            //Save errand to database
            await ErrandService.SaveAsync(errand);

        }
        public async Task ListAllErrandsAsync()
        {
            var errands = await ErrandService.GetAllAsync();

            if (errands.Any())
            {
                foreach (Errand errand in errands)
                {
                    Console.WriteLine($"Errand ID: {errand.Id}");
                    Console.WriteLine($"Customer Name: {errand.CustomerName}");
                    Console.WriteLine($"Customer Phone: {errand.Phone}, Customer Email: {errand.Email}");
                    Console.WriteLine($"Error Description: {errand.Description}");
                    Console.WriteLine($"Errand Created: {errand.ErrandDateTime}");
                    Console.WriteLine($"Errand Status: {errand.Status}");


                    Console.WriteLine("");

                }

            }
            else
            {
                Console.WriteLine("No Errands To Show...");
                Console.WriteLine("");
            }
        }
        public async Task ListSpecificErrandAsync()
        {
            Console.WriteLine("Input Errand ID: ");
            int id = 0;
            int.TryParse(Console.ReadLine(), out id);


            if (id > 0)
            {
                var errand = await ErrandService.GetAsync(id);

                if (errand != null)
                {
                    Console.WriteLine($"Errand ID: {errand.Id}");
                    Console.WriteLine($"Customer Name: {errand.CustomerName}");
                    Console.WriteLine($"Customer Phone: {errand.Phone}, Customer Email: {errand.Email}");
                    Console.WriteLine($"Error Description: {errand.Description}");
                    Console.WriteLine($"Errand Created: {errand.ErrandDateTime}");
                    Console.WriteLine($"Errand Status: {errand.Status}");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"No Errand Found with ID {id}...");
                    Console.WriteLine("");
                }
            }
            else
            {
                Console.WriteLine($"No Input...");
                Console.WriteLine("");
            }
        }

        public async Task UpdateSpecificErrandAsync()
        {
            Console.WriteLine("Input The Errand ID You Would Like To Update: ");
            int id = 0;
            int.TryParse(Console.ReadLine(), out id);

            var errand = await ErrandService.GetAsync(id);

            if (errand != null)
            {
                Console.WriteLine("Select Errand Status:\n1. Not Started \n2. In Progress\n3. Completed");

                Console.WriteLine("Status: ");

                ErrandStatus status;
                if (Enum.TryParse(Console.ReadLine(), out status) && Enum.IsDefined(typeof(ErrandStatus), status))
                {
                    errand.Status = status;
                }
                else
                {
                    Console.WriteLine("Invalid Input...");
                }

                await ErrandService.UpdateAsync(errand);
                Console.WriteLine("Errand Updated");
            }
            else
            {
                Console.WriteLine($"No Errand Found with ID {id}...");
                Console.WriteLine("");
            }
        }
        public async Task DeleteSpecificErrandAsync()
        {
            Console.WriteLine("Input The ID of the Errand you would like to Delete:");
            int id = 0;
            int.TryParse(Console.ReadLine(), out id);

            var errand = await ErrandService.GetAsync(id);

            if (errand != null)
            {
                await ErrandService.DeleteAsync(id);
                Console.WriteLine("Errand Deleted");
            }
            else
            {
                Console.WriteLine($"No Errand Found with ID {id}...");
            }
        }
    }
}

