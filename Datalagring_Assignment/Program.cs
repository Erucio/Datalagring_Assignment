using Datalagring_Assignment.Services;
var menu = new MenuService();

while (true)
{
    Console.Clear();
    Console.WriteLine("1. Create Errand");
    Console.WriteLine("2. Display Errands");
    Console.WriteLine("3. View Specific Errand");
    Console.WriteLine("4. Update Errand");
    Console.WriteLine("5. Delete Errand");
    Console.WriteLine("Input Command 1-5: ");

    switch (Console.ReadLine())
    {
        case "1":
            await menu.CreateNewErrandAsync();
            break;
        case "2":
            await menu.ListAllErrandsAsync();
            break;
        case "3":
            await menu.ListSpecificErrandAsync();
            break;
        case "4":
            await menu.UpdateSpecificErrandAsync();
            break;
        case "5":
            await menu.DeleteSpecificErrandAsync();
            break;

        default:
            Console.WriteLine("Please Input Command 1-5");
            break;
    }
    Console.WriteLine("Press a key to continue...");
    Console.ReadKey();
}