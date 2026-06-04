using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Car
{
    public string Driver { get; set; }
    public string Model { get; set; }
    public int PositionOnTrack { get; set; }
    public List<double> LapTimes { get; set; } = new List<double>();

    public Car(string driver, string model)
    {
        Driver = driver;
        Model = model;
        PositionOnTrack = 0;
    }

    public double TotalTime()
    {
        return LapTimes.Sum();
    }
}

class Track
{
    public string Name { get; set; }
    public int Length { get; set; }

    public Track(string name, int length)
    {
        Name = name;
        Length = length;
    }
}

class LapTimer
{
    private Random random = new Random();

    public double GenerateLapTime()
    {
        return Math.Round(random.NextDouble() * 20 + 60, 2);
    }
}

class Race
{
    public Track Track { get; set; }
    public List<Car> Cars { get; set; }
    public int Laps { get; set; }
    private LapTimer lapTimer = new LapTimer();

    public Race(Track track, List<Car> cars, int laps)
    {
        Track = track;
        Cars = cars;
        Laps = laps;
    }

    public void StartRace()
    {
        Console.WriteLine($"Wyścig na torze: {Track.Name}");
        Console.WriteLine($"Długość toru: {Track.Length} m");
        Console.WriteLine($"Liczba okrążeń: {Laps}");
        Console.WriteLine();

        for (int lap = 1; lap <= Laps; lap++)
        {
            Console.WriteLine($"--- Okrążenie {lap} ---");

            foreach (Car car in Cars)
            {
                double lapTime = lapTimer.GenerateLapTime();
                car.LapTimes.Add(lapTime);
                car.PositionOnTrack += Track.Length;

                Console.WriteLine($"{car.Driver} ({car.Model}) - czas okrążenia: {lapTime} s");
            }

            Console.WriteLine();
            Thread.Sleep(500);
        }

        ShowPodium();
    }

    public void ShowPodium()
    {
        Console.WriteLine("=== PODIUM ===");

        var results = Cars
            .OrderBy(car => car.TotalTime())
            .ToList();

        for (int i = 0; i < results.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {results[i].Driver} - {results[i].Model} | Łączny czas: {results[i].TotalTime()} s");
        }
    }
}

class RacingGame
{
    static void Main()
    {
        Track track = new Track("Silverstone", 5800);

        List<Car> cars = new List<Car>
        {
            new Car("Kierowca 1", "Ferrari"),
            new Car("Kierowca 2", "Lamborghini"),
            new Car("Kierowca 3", "Porsche"),
            new Car("Kierowca 4", "BMW")
        };

        Race race = new Race(track, cars, 3);

        race.StartRace();

        Console.ReadKey();
    }
}