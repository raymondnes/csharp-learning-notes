using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace csharpLearning_byGemma4
{
    public class Car
    {
        public Car() 
        {
            // Default constructor, can be used to create a Car object without setting any properties
        }
        // The Constructor
        public Car(string name, string color, string model, int year) 
        {
            // Sets the imput value below to the properties of the class
            this.Name = name;
            this.Color = color;
            this.Model = model;
            this.Year = year;
        }

        public string Name { get; set; }
        public string Color { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public virtual void Start()
        {
            Console.WriteLine($"The {this.Color} {this.Name}'s engine has started.");
        }
        public void honk()
        {
            Console.WriteLine("Beep beep!");
        }
    }

    // The Derived class, with inheritance from the Car class
    public class SportsCar : Car
    {
        // New specialized attributes for the sportscar
        public int TurboPower { get; set; }

        // The constructor must first call the parent constructor first! Then add its own attributes afterwards, use : base() to call the parent constructor
        public SportsCar(string name, string color, string model, int year, int turboPower) : base(name, color, model, year)
        { 
            this.TurboPower = turboPower;
        }

        // Overriding the parent's method (making it more specific to the sportscar)
        public override void Start()
        {
            Console.WriteLine($"BRRRRBRRRRR! The {Color} {Name} roars to life with a Turbo Power of {TurboPower} ");
        }

        // New unique method for the sportscar
        public void ActivateTurbo()
        {
            Console.WriteLine($"The {Name} is now in Turbo mode! Hold on tight!");
        }
    }

    public class ElectricCar : Car
    {
        // New specialized attributes for the electric car
        public int BatteryCapacity { get; set; }

        // The constructor must first call the parent constructor first! Then add its own attributes afterwards, use : base() to call the parent constructor
        public ElectricCar(string name, string color, string model, int year, int batteryCapacity) : base(name, color, model, year)
        { 
            this.BatteryCapacity = batteryCapacity;
        }

        // Overriding the parent start method to make it more specific to the electric car
        public override void Start()
        {
            Console.WriteLine($"Silent whirr... The {Color} {Model} powers up. Range remaining: {BatteryCapacity} kWh.");
        }
    }

    public class Cinema
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public int NumOfTheaters { get; set; }

        public Cinema() 
        {
            // base constructor, can be used to create a Cinema object without setting any properties
        }

        public Cinema(string name, string location, int numOfTheaters)
        {
            this.Name = name;
            this.Location = location;
            this.NumOfTheaters = numOfTheaters;
        }

        public virtual void ShowInfo()
        {
            Console.WriteLine($"Cinema: {Name}, Location: {Location}, Number of Theatres: {NumOfTheaters}");
        }
    }

    // Establish Theatre Classes with inheritance from Cinema class, and add more specific attributes and methods to the Theatre class
    public class Theater : Cinema
    {
        public int SeatingCapacity { get; set; }
        public bool Has3D { get; set; }
        public int TheaterNumber { get; set; }
        public string MovieTitle { get; set; } // <-- Changed from int to string

        public Theater(string name, string location, int numOfTheaters, int seatingCapacity, bool has3D, int theaterNumber, string movieTitle) : base(name, location, numOfTheaters)
        {
            this.SeatingCapacity = seatingCapacity;
            this.Has3D = has3D;
            this.TheaterNumber = theaterNumber;
            this.MovieTitle = movieTitle;
        }

        public void NowShowing(string movieTitle)
        {
            Console.WriteLine($"Now showing: {MovieTitle} at Theater {TheaterNumber} in {Name} Cinema. Seating Capacity: {SeatingCapacity}, 3D Available: {Has3D}");
        }

        public override void ShowInfo()
        {
            base.ShowInfo();
            Console.WriteLine($"Theater {TheaterNumber} is Now showing {MovieTitle}");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            string favoriteMovie = "Inception";
            int movieYear = 2010;
            Console.WriteLine($"My favorite movie is {favoriteMovie}, released in {movieYear}.");

            int score = 50;
            if (score > 70)
            {
                Console.WriteLine("Congratulations! You Passed.");
            }
            else 
            {
                Console.WriteLine("Keep studying! You need to try again.");
            }

            int month = 5;
            switch (month) {

                case 1:
                case 2:
                case 12:
                    Console.WriteLine("Winter");
                    break;
                case 3:
                case 4:
                case 5:
                    Console.WriteLine("Spring");
                    break;  
                case 6:
                case 7:
                case 8:
                    Console.WriteLine("Summer");
                    break;
                case 9:
                case 10:
                case 11:
                    Console.WriteLine("Autumn");
                    break;             
            }

            // Create a Car object and set its properties
            Car Tesla = new Car();
            Tesla.Color = "black";
            Tesla.Model = "Model S";
            Tesla.Year = 2022;
            // Call the methods of the Car object
            Tesla.Start();
            Console.WriteLine($"Car: {Tesla.Model}, {Tesla.Color}, {Tesla.Year}");
            
            SportsCar Ferrari = new SportsCar("Ferrari", "red", "F8 Tributo", 2020, 710);
            Ferrari.Start();
            Ferrari.ActivateTurbo();

            ElectricCar NissanLeaf = new ElectricCar("Nissan Leaf", "white", "Leaf", 2021, 40);
            NissanLeaf.Start();
            NissanLeaf.honk();

            Cinema Novare = new Cinema("Novare", "City Center", 10);
            Novare.ShowInfo();

            Theater Theater1 = new Theater("Novare", "Helsinki", 10, 150, true, 1, "Inception");
            Theater1.NowShowing("Inception");
            Theater1.ShowInfo();
            Theater Theater2 = new Theater("Finnikko", "Itakeskus", 5, 40, false, 2, "Mario Galaxy");
            Theater2.ShowInfo();
            Theater2.NowShowing("Mario Galaxy");

            Console.ReadLine();
        }
    }
}
