using ConsoleGrpcApp;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;

namespace GrpcClient;

class Program
{
    static async Task Main()
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:5058");
        var client = new Order.OrderClient(channel);

        while (true)
        {
            Console.WriteLine("\n======");
            Console.WriteLine("1. Показать все заказы");
            Console.WriteLine("2. Получить заказ по ID");
            Console.WriteLine("3. Добавить новый заказ");
            Console.WriteLine("4. Изменить заказ");
            Console.WriteLine("5. Удалить заказ");
            Console.WriteLine("6. Фильтровать по дате и стоимости");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите пункт меню: ");

            string choice = Console.ReadLine();
            try
            {
                switch (choice)
                {
                    case "1":
                        await GetList(client);
                        break;
                    case "2":
                        await Get(client);
                        break;
                    case "3":
                        await Add(client);
                        break;
                    case "4":
                        await Update(client);
                        break;
                    case "5":
                        await Delete(client);
                        break;
                    case "6":
                        await GetFilteredList(client);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный пункт. Попробуйте снова.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при выполнении операции: {ex.Message}");
            }
        }
    }

    private static async Task GetList(Order.OrderClient client)
    {
        var reply = await client.GetListAsync(new VoidRequest());
        PrintOrders(reply);
    }

    private static async Task Get(Order.OrderClient client)
    {
        Console.Write("Введите ID заказа: ");
        int id = int.Parse(Console.ReadLine());

        var reply = await client.GetAsync(new GetRequest { Id = id });
        PrintOrder(reply);
    }

    private static async Task Add(Order.OrderClient client)
    {
        AddRequest request = new()
        {
            OrderDate = Timestamp.FromDateTime(DateTime.UtcNow)
        };

        Console.Write("Введите название товара: ");
        string title = Console.ReadLine();
        Console.Write("Введите цену товара: ");
        double price = double.Parse(Console.ReadLine());

        request.Products.Add(new Product { Title = title, Price = price });

        var reply = await client.AddAsync(request);
        Console.WriteLine($"Заказ успешно добавлен");
    }

    private static async Task Update(Order.OrderClient client)
    {
        Console.Write("Введите ID заказа для обновления: ");
        int id = int.Parse(Console.ReadLine());

        UpdateRequest request = new()
        {
            Id = id,
            OrderDate = Timestamp.FromDateTime(DateTime.UtcNow)
        };

        Console.Write("Введите новое название товара: ");
        string title = Console.ReadLine();
        Console.Write("Введите новую цену товара: ");
        double price = double.Parse(Console.ReadLine());

        request.Products.Add(new Product { Title = title, Price = price });

        var reply = await client.UpdateAsync(request);
        Console.WriteLine($"Заказ {reply.Id} обновлен");
    }

    private static async Task Delete(Order.OrderClient client)
    {
        Console.Write("Введите ID заказа для удаления: ");
        int id = int.Parse(Console.ReadLine());

        var reply = await client.DeleteAsync(new() { Id = id });
        Console.WriteLine(reply.Result ? "Заказ успешно удален" : "Не удалось удалить заказ");
    }

    private static async Task GetFilteredList(Order.OrderClient client)
    {
        Console.Write("Введите минимальную дату (ГГГГ-ММ-ДД): ");
        DateTime date = DateTime.Parse(Console.ReadLine()).ToUniversalTime();

        Console.Write("Введите минимальную стоимость товара: ");
        double minPrice = double.Parse(Console.ReadLine());

        FilterRequest request = new()
        {
            MinDate = Timestamp.FromDateTime(date),
            MinPrice = minPrice
        };

        var reply = await client.GetFilteredListAsync(request);
        PrintOrders(reply);
    }

    private static void PrintOrders(GetListReply reply)
    {
        Console.WriteLine($"\nНайдено заказов: {reply.Orders.Count}");
        foreach (var order in reply.Orders)
        {
            PrintOrder(order);
        }
    }

    private static void PrintOrder(OrderReply order)
    {
        Console.WriteLine($"Заказ #{order.Id} от {order.OrderDate.ToDateTime().ToLocalTime()}");
        foreach (var prod in order.Products)
        {
            Console.WriteLine($"{prod.Title}: {prod.Price} руб.");
        }
    }
}
