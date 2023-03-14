using Datalagring_Assignment.Models;
using Datalagring_Assignment.Models.Entities;

namespace Datalagring_Assignment.Services
{
    internal class MenuService
    {

        #region Create Errand

        public async Task CreateNewErrandAsync()
        {
            var errand = new Errand();
            Console.Clear();
            Console.WriteLine("Insert Full Customer Name:");
            errand.CustomerName = Console.ReadLine() ?? "";

            Console.WriteLine("Insert Customer Phone Number:");
            errand.Phone = Console.ReadLine() ?? "";


            Console.WriteLine("Insert Customer E-mail Address:");
            errand.Email = Console.ReadLine() ?? "";


            Console.WriteLine("Errand Description:");
            errand.Description = Console.ReadLine() ?? "";

            errand.ErrandDateTime = DateTime.Now;
            Console.WriteLine("Errand Created:");


            //Save errand to database
            await ErrandService.SaveAsync(errand);

        }
        #endregion

        #region Show All Errands
        public async Task ListAllErrandsAsync()
        {
            var errands = await ErrandService.GetAllAsync();

            if (errands.Any())
            {
                foreach (Errand errand in errands)
                {
                    Console.Clear();
                    Console.WriteLine($"Errand ID: {errand.Id}");
                    Console.WriteLine($"Customer Name: {errand.CustomerName}");
                    Console.WriteLine($"Customer Phone: {errand.Phone}, Customer Email: {errand.Email}");
                    Console.WriteLine($"Error Description: {errand.Description}");
                    Console.WriteLine($"Errand Created: {errand.ErrandDateTime}");
                    Console.WriteLine($"Errand Status: {errand.Status}");

                    if (errand.Comments.Any())
                    {
                        Console.WriteLine("Comments:");

                        foreach (CommentEntity comment in errand.Comments)
                        {
                            Console.WriteLine($"\t{comment.Comment} \n  \t{comment.CommentDateTime}\n");
                        }
                    }

                    Console.WriteLine("");
                }
            }
            else
            {
                Console.WriteLine("No Errands To Show...");
                Console.WriteLine("");
            }
        }
        #endregion

        #region Show Specific Errand
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
                    Console.Clear();
                    Console.WriteLine($"Errand ID: {errand.Id}");
                    Console.WriteLine($"Customer Name: {errand.CustomerName}");
                    Console.WriteLine($"Customer Phone: {errand.Phone}, Customer Email: {errand.Email}");
                    Console.WriteLine($"Error Description: {errand.Description}");
                    Console.WriteLine($"Errand Created: {errand.ErrandDateTime}");
                    Console.WriteLine($"Errand Status: {errand.Status}");
                    //Display Comments if there are any
                    if (errand.Comments.Any())
                    {
                        Console.WriteLine("Comments:");

                        foreach (CommentEntity comment in errand.Comments)
                        {
                            Console.WriteLine($"\t{comment.Comment} \n \t{comment.CommentDateTime} \n");
                        }
                    }

                    Console.WriteLine("");
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
        #endregion

        #region Update Errand
        public async Task UpdateSpecificErrandAsync()
        {
            Console.WriteLine("Input The Errand ID You Would Like To Update: ");
            int id = 0;
            int.TryParse(Console.ReadLine(), out id);

            var errand = await ErrandService.GetAsync(id);

            if (errand != null)
            {
                //Change Status
                Console.Clear();
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

                //Add comment?
                Console.Clear();
                Console.WriteLine("Would You Like To Add A Comment To This Errand? (Y/N)");

                if (Console.ReadKey(true).Key == ConsoleKey.Y)
                {
                    Console.Clear();
                    Console.WriteLine("Enter Your Comment:");
                    string comment = Console.ReadLine() ?? "";

                    await ErrandService.AddCommentAsync(errand.Id, comment);

                    Console.WriteLine("Comment Added...");
                    Console.Clear();
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

        #endregion

        #region Delete
        public async Task DeleteSpecificErrandAsync()
        {
            Console.WriteLine("Input The ID of the Errand you would like to Delete:");
            int id = 0;
            int.TryParse(Console.ReadLine(), out id);

            var errand = await ErrandService.GetAsync(id);

            if (errand != null)
            {
                //Only deletes Completed Errands

                if ((int)errand.Status == 3)
                {
                    Console.Clear();
                    await ErrandService.DeleteAsync(id);
                    Console.WriteLine("Errand Deleted");
                }
                else
                {
                    Console.WriteLine("Errand Can Only Be Deleted Once It Has Been Completed");
                }
            }
            else
            {
                Console.WriteLine($"No Errand Found with ID {id}...");
            }
        }
        #endregion
    }
}

