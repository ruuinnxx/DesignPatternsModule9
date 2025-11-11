﻿var rooms = new Dictionary<Room, bool>(10)
{
    { new Room(1, "Room1"), false },
    { new Room(2, "Room1"), false },
    { new Room(3, "Room1"), false },
    { new Room(4, "Room1"), true },
    { new Room(5, "Room1"), true },
    { new Room(6, "Room1"), true },
    { new Room(7, "Room1"), true },
    { new Room(8, "Room1"), true },
    { new Room(9, "Room1"), true },
    { new Room(10, "Room1"), true },
};

var hotelFacade = new HotelFacade(rooms);
hotelFacade.Run();


#region Фасад

public class HotelFacade(Dictionary<Room, bool> rooms)
{
    private readonly RoomBookingSystem _roomSystem = new(rooms);
    private readonly RestaurantSystem _restaurantSystem = new();
    private readonly CleaningService _clenSystem = new (rooms);
    private readonly EventManagementSystem _evenManagementSystem = new ();

    public void Run()
    {
        RoomReservationManager();
        RestaurantManager();
        CleaningManager();
        EventManager();
    }
    
    private void RoomReservationManager()
    {
        while (true)
        {
            Console.WriteLine("\n>> Бронирование комнаты <<\nСписок всех комнат");

            foreach (var keyValuePair in rooms)
                Console.WriteLine($"{keyValuePair.Key.Id}) {keyValuePair.Key.Name} - {keyValuePair.Value}");

            Console.WriteLine("\nВыберите действие: " +
                              "\n1-Забронировать комнату" +
                              "\n2-Отменить бронь" +
                              "\n0-Выйти");

            var action = Console.ReadLine();
            switch (action)
            {
                case "1":
                    Console.WriteLine("Выберите комнату для бронирования: ");
                    var roomId = Console.ReadLine();
                    _roomSystem.BookRoom(int.Parse(roomId!));
                    break;
                case "2":
                    Console.WriteLine("Отменить бронь " +
                                      "\nВведите номер комнаты");
                    var roomIdCancel = Console.ReadLine();
                    _roomSystem.CancelBooking(int.Parse(roomIdCancel!));
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Нет такого выбора, повторите попытку");
                    break;
            }
        }
    }

    private void RestaurantManager()
    {
        while (true)
        {
            Console.WriteLine(">> Заказ столов и блюд <<");
            var name = ReadInput("Введите имя для бронирования: ");
            var guests = ReadInput("Введите количество гостей: ");
            if (!int.TryParse(guests, out var countOfPerson))
            {
                Console.WriteLine("Введите корректные данные");
                continue;
            }

            _restaurantSystem.ReserveTables(name, countOfPerson, DateTime.Now);
            var dishes = new[] { "dish1", "dish2", "dish3" };

            Console.WriteLine("\nЗаказ еды");
            _restaurantSystem.OrderFood(dishes);
            var exite = ReadInput("0-Выйти");
            if (exite == "0")
                return;
        }
    }

    private void CleaningManager()
    {
        int roomId;
        while (true)
        {
            var room = ReadInput("Введите номер комнаты");
            if (!int.TryParse(room, out roomId))
            {
                Console.WriteLine("Введите корректные значения");
                continue;
            }

            break;
        }

        _clenSystem.CleanRoom(roomId);
    }

    private void EventManager() =>
        _evenManagementSystem.HotelEvent();

    private string ReadInput(string message)
    {
        while (true)
        {
            Console.WriteLine(message);
            var input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
                return input;
            Console.WriteLine("Введите корректные значения");
        }
    }
}
#endregion

#region Подсистемы отеля

public class Room(int id, string name)
{
    public int Id { get; set; } = id;
    public string Name { get; set; } = name;
}

public class RoomBookingSystem(Dictionary<Room, bool> rooms)
{
    public void BookRoom(int idRoom)
    {
        var room = rooms.FirstOrDefault(r => r.Key.Id == idRoom);

        if (room.Value)
        {
            Console.WriteLine($"Успешное бронирование комнаты - {room.Key.Name}");
            rooms[room.Key] = false;
        }
        else
            Console.WriteLine("Комната уже забронирована, выберите другую");
    }

    public void CancelBooking(int idRoom)
    {
        var room = rooms.FirstOrDefault(r => r.Key.Id == idRoom);
        if (!room.Value)
        {
            Console.WriteLine("Бронирование отменено");
            rooms[room.Key] = true;
        }
        else
            Console.WriteLine("Комната уже свободна");
    }
}


public class RestaurantSystem
{
    public void ReserveTables(string name, int countOfPerson, DateTime time)
    {
        Console.WriteLine($"Имя гостья: {name}");
        Console.WriteLine($"Количество гостей: {countOfPerson}");
        Console.WriteLine($"Забронировано на {time.ToShortDateString()}");
    }

    public void OrderFood(string[] dishes)
    {
        Console.WriteLine("Блюда: ");
        foreach (var dish in dishes)
        {
            Console.WriteLine(dish);
        }
    }
}

public class EventManagementSystem
{
    public void HotelEvent() =>
        Console.WriteLine("Мероприятие Отеля");
}

public class CleaningService(Dictionary<Room, bool> rooms)
{
    public void CleanRoom(int roomId)
    {
        var room = rooms.FirstOrDefault(r => r.Key.Id == roomId);
        Console.WriteLine($"Чистка комнаты - {room.Key.Name}");
    }
}

#endregion