using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PZ19_20
{
    class TechnicalData
    {
        public string Model { get; set; }
        public int MaxSpeed { get; set; }
        public int MaxRange { get; set; }
        public int Capacity { get; set; }

        public TechnicalData(string model, int maxSpeed, int maxRange, int capacity)
        {
            Model = model;
            MaxSpeed = maxSpeed;
            MaxRange = maxRange;
            Capacity = capacity;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Модель: {Model}");
            Console.WriteLine($"Максимальная скорость: {MaxSpeed} км/ч");
            Console.WriteLine($"Максимальная дальность: {MaxRange} км");
            Console.WriteLine($"Вместимость: {Capacity} пассажиров");
        }
    }

    class CrewMember
    {
        public string Name { get; set; }
        public string Role { get; set; }

        public CrewMember(string name, string role)
        {
            Name = name;
            Role = role;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"{Role}: {Name}");
        }
    }

    class Crew
    {
        private List<CrewMember> crewMembers = new List<CrewMember>();

        public void AddMember(string name, string role)
        {
            crewMembers.Add(new CrewMember(name, role));
        }

        public void DisplayInfo()
        {
            Console.WriteLine("Экипаж:");
            foreach (var member in crewMembers)
            {
                member.DisplayInfo();
            }
        }
    }

    class Flight
    {
        public string FlightNumber { get; set; }
        public string Destination { get; set; }
        public string DepartureTime { get; set; }

        public Flight(string flightNumber, string destination, string departureTime)
        {
            FlightNumber = flightNumber;
            Destination = destination;
            DepartureTime = departureTime;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Рейс: {FlightNumber}");
            Console.WriteLine($"Пункт назначения: {Destination}");
            Console.WriteLine($"Время вылета: {DepartureTime}");
        }
    }

    class Seats
    {
        public int TotalEconomy { get; private set; }
        public int OccupiedEconomy { get; private set; }
        public int TotalBusiness { get; private set; }
        public int OccupiedBusiness { get; private set; }

        public Seats(int totalEconomy, int totalBusiness)
        {
            TotalEconomy = totalEconomy;
            TotalBusiness = totalBusiness;
        }

        public void BookEconomy(int count)
        {
            if (OccupiedEconomy + count <= TotalEconomy)
            {
                OccupiedEconomy += count;
            }
            else
            {
                Console.WriteLine("Недостаточно мест в эконом-классе.");
            }
        }

        public void BookBusiness(int count)
        {
            if (OccupiedBusiness + count <= TotalBusiness)
            {
                OccupiedBusiness += count;
            }
            else
            {
                Console.WriteLine("Недостаточно мест в бизнес-классе.");
            }
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Эконом-класс: {OccupiedEconomy}/{TotalEconomy} занято");
            Console.WriteLine($"Бизнес-класс: {OccupiedBusiness}/{TotalBusiness} занято");
        }
    }

    class Airplane
    {
        public TechnicalData TechData { get; private set; }
        public Crew Crew { get; private set; }
        public Flight Flight { get; private set; }
        public Seats Seats { get; private set; }

        public Airplane(TechnicalData techData, Crew crew, Flight flight, Seats seats)
        {
            TechData = techData;
            Crew = crew;
            Flight = flight;
            Seats = seats;
        }

        public void DisplayAirplaneInfo()
        {
            Console.WriteLine("\n--- Информация о самолёте ---");
            TechData.DisplayInfo();
            Console.WriteLine();
            Crew.DisplayInfo();
            Console.WriteLine();
            Seats.DisplayInfo();
            Console.WriteLine();
            Flight.DisplayInfo();
            Console.WriteLine();
        }
    }

    class Program
    {
        static void Main()
        {
            TechnicalData techData = new TechnicalData("Boeing 737", 850, 6000, 180);

            Crew crew = new Crew();
            crew.AddMember("Иван Петров", "Капитан");
            crew.AddMember("Алексей Смирнов", "Второй пилот");
            crew.AddMember("Мария Иванова", "Бортпроводник");

            Flight flight = new Flight("SU123", "Москва - Санкт-Петербург", "14:30");

            Seats seats = new Seats(150, 30);
            seats.BookEconomy(50);
            seats.BookBusiness(10);

            Airplane airplane = new Airplane(techData, crew, flight, seats);
            airplane.DisplayAirplaneInfo();
        }
    }
}
