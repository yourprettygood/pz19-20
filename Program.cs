using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Ввод технических параметров самолёта
        Console.WriteLine("Введите модель самолёта:");
        string model = Console.ReadLine();
        Console.WriteLine("Введите максимальную скорость (км/ч):");
        int speed = int.Parse(Console.ReadLine());
        Console.WriteLine("Введите максимальную дальность (км):");
        int range = int.Parse(Console.ReadLine());
        Console.WriteLine("Введите тип топлива:");
        string fuelType = Console.ReadLine();

        // Ввод параметров бронирования мест
        Console.WriteLine("Введите общее количество бизнес мест:");
        int businessSeats = int.Parse(Console.ReadLine());
        Console.WriteLine("Введите общее количество эконом мест:");
        int economySeats = int.Parse(Console.ReadLine());

        Console.WriteLine("Введите количество уже забронированных бизнес мест:");
        int businessBooked = int.Parse(Console.ReadLine());
        Console.WriteLine("Введите количество уже забронированных эконом мест:");
        int economyBooked = int.Parse(Console.ReadLine());

        // Проверка корректности введенных данных
        if (businessBooked > businessSeats || economyBooked > economySeats)
        {
            Console.WriteLine("Ошибка: количество забронированных мест не может превышать общее количество мест.");
            return;
        }

        Airplane airplane = new Airplane(model, speed, range, fuelType, businessSeats, economySeats, businessBooked, economyBooked);

        // Ввод данных о рейсе
        Console.WriteLine("Введите номер рейса:");
        string flightNumber = Console.ReadLine();
        Console.WriteLine("Введите город отправления:");
        string departure = Console.ReadLine();
        Console.WriteLine("Введите город назначения:");
        string destination = Console.ReadLine();
        airplane.Flight.SetFlightDetails(flightNumber, departure, destination);

        // Ввод данных экипажа
        Console.WriteLine("Сколько членов экипажа добавить?");
        int crewCount = int.Parse(Console.ReadLine());
        for (int i = 0; i < crewCount; i++)
        {
            Console.WriteLine($"Введите должность для члена экипажа {i + 1}:");
            string position = Console.ReadLine();
            Console.WriteLine($"Введите имя для {position}:");
            string name = Console.ReadLine();
            airplane.Crew.AddMember(position, name);
        }

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\n--- Меню ---");
            Console.WriteLine("1. Показать информацию о самолёте");
            Console.WriteLine("2. Изменить технические данные");
            Console.WriteLine("3. Изменить данные экипажа");
            Console.WriteLine("4. Изменить данные рейса");
            Console.WriteLine("5. Забронировать место");
            Console.WriteLine("6. Выход");
            Console.Write("Выберите опцию: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    airplane.PrintTechnicalInfo();
                    airplane.PrintCrewInfo();
                    airplane.PrintFlightInfo();
                    airplane.PrintSeatInfo();
                    break;
                case "2":
                    UpdateTechnicalData(airplane);
                    break;
                case "3":
                    UpdateCrewData(airplane);
                    break;
                case "4":
                    UpdateFlightData(airplane);
                    break;
                case "5":
                    BookSeat(airplane);
                    break;
                case "6":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Опция не распознана.");
                    break;
            }
        }
    }

    static void UpdateTechnicalData(Airplane airplane)
    {
        Console.WriteLine("Обновление технических данных:");
        Console.Write("Введите новую модель самолёта: ");
        string newModel = Console.ReadLine();
        Console.Write("Введите новую максимальную скорость (км/ч): ");
        int newSpeed = int.Parse(Console.ReadLine());
        Console.Write("Введите новую максимальную дальность (км): ");
        int newRange = int.Parse(Console.ReadLine());
        Console.Write("Введите новый тип топлива: ");
        string newFuelType = Console.ReadLine();

        airplane.Technical.Update(newModel, newSpeed, newRange, newFuelType);
        Console.WriteLine("Технические данные обновлены.");
    }

    static void UpdateCrewData(Airplane airplane)
    {
        Console.WriteLine("Обновление данных экипажа:");
        Console.WriteLine("1. Добавить члена экипажа");
        Console.WriteLine("2. Обновить данные существующего");
        Console.Write("Выберите опцию: ");
        string option = Console.ReadLine();

        if (option == "1")
        {
            Console.Write("Введите должность: ");
            string position = Console.ReadLine();
            Console.Write("Введите имя: ");
            string name = Console.ReadLine();
            airplane.Crew.AddMember(position, name);
            Console.WriteLine("Член экипажа добавлен.");
        }
        else if (option == "2")
        {
            Console.Write("Введите должность для обновления: ");
            string position = Console.ReadLine();
            Console.Write("Введите новое имя: ");
            string newName = Console.ReadLine();
            if (airplane.Crew.UpdateMember(position, newName))
                Console.WriteLine("Данные обновлены.");
            else
                Console.WriteLine("Член экипажа с указанной должностью не найден.");
        }
        else
        {
            Console.WriteLine("Опция не распознана.");
        }
    }

    static void UpdateFlightData(Airplane airplane)
    {
        Console.WriteLine("Обновление данных рейса:");
        Console.Write("Введите новый номер рейса: ");
        string flightNumber = Console.ReadLine();
        Console.Write("Введите новый город отправления: ");
        string departure = Console.ReadLine();
        Console.Write("Введите новый город назначения: ");
        string destination = Console.ReadLine();
        airplane.Flight.SetFlightDetails(flightNumber, departure, destination);
        Console.WriteLine("Данные рейса обновлены.");
    }

    static void BookSeat(Airplane airplane)
    {
        Console.WriteLine("Бронирование места:");
        Console.Write("Введите тип класса (Business/Economy): ");
        string seatType = Console.ReadLine();
        if (airplane.Seats.BookSeat(seatType))
            Console.WriteLine("Место успешно забронировано.");
        else
            Console.WriteLine("Нет свободных мест или указан неверный тип.");
    }
}

class Airplane
{
    public TechnicalDetails Technical { get; }
    public Crew Crew { get; }
    public Flight Flight { get; }
    public Seats Seats { get; }

    public Airplane(string model, int speed, int range, string fuelType, int businessSeats, int economySeats, int businessBooked, int economyBooked)
    {
        Technical = new TechnicalDetails(model, speed, range, fuelType);
        Crew = new Crew();
        Flight = new Flight();
        Seats = new Seats(businessSeats, economySeats, businessBooked, economyBooked);
    }

    public void PrintTechnicalInfo() => Technical.PrintInfo();
    public void PrintCrewInfo() => Crew.PrintInfo();
    public void PrintFlightInfo() => Flight.PrintInfo();
    public void PrintSeatInfo() => Seats.PrintInfo();
}

class TechnicalDetails
{
    private string Model;
    private int MaxSpeed;
    private int MaxRange;
    private string FuelType;

    public TechnicalDetails(string model, int speed, int range, string fuelType)
    {
        Model = model;
        MaxSpeed = speed;
        MaxRange = range;
        FuelType = fuelType;
    }

    public void PrintInfo()
    {
        Console.WriteLine($"\nТехнические данные:\nМодель: {Model}, Макс. скорость: {MaxSpeed} км/ч, Макс. дальность: {MaxRange} км, Топливо: {FuelType}");
    }

    public void Update(string model, int speed, int range, string fuelType)
    {
        Model = model;
        MaxSpeed = speed;
        MaxRange = range;
        FuelType = fuelType;
    }
}

class Crew
{
    private Dictionary<string, string> Members = new Dictionary<string, string>();

    public void AddMember(string position, string name)
    {
        Members[position] = name;
    }

    public bool UpdateMember(string position, string newName)
    {
        if (Members.ContainsKey(position))
        {
            Members[position] = newName;
            return true;
        }
        return false;
    }

    public void PrintInfo()
    {
        Console.WriteLine("\nЭкипаж:");
        foreach (var member in Members)
        {
            Console.WriteLine($"{member.Key}: {member.Value}");
        }
    }
}

class Flight
{
    private string FlightNumber;
    private string Departure;
    private string Destination;

    public void SetFlightDetails(string flightNumber, string departure, string destination)
    {
        FlightNumber = flightNumber;
        Departure = departure;
        Destination = destination;
    }

    public void PrintInfo()
    {
        Console.WriteLine($"\nРейс:\nНомер рейса: {FlightNumber}, Из: {Departure}, В: {Destination}");
    }
}

class Seats
{
    private int BusinessSeats;
    private int EconomySeats;
    private int BusinessOccupied;
    private int EconomyOccupied;

    public Seats(int business, int economy, int businessBooked, int economyBooked)
    {
        BusinessSeats = business;
        EconomySeats = economy;
        BusinessOccupied = businessBooked;
        EconomyOccupied = economyBooked;
    }

    // Возвращает true, если бронирование прошло успешно
    public bool BookSeat(string type)
    {
        if (type.Equals("Business", StringComparison.OrdinalIgnoreCase))
        {
            if (BusinessOccupied < BusinessSeats)
            {
                BusinessOccupied++;
                return true;
            }
        }
        else if (type.Equals("Economy", StringComparison.OrdinalIgnoreCase))
        {
            if (EconomyOccupied < EconomySeats)
            {
                EconomyOccupied++;
                return true;
            }
        }
        return false;
    }

    public void PrintInfo()
    {
        Console.WriteLine($"\nМеста:");
        Console.WriteLine($"Бизнес класс: {BusinessOccupied}/{BusinessSeats} занято");
        Console.WriteLine($"Эконом класс: {EconomyOccupied}/{EconomySeats} занято");
    }
}
