namespace LinqDemo;

class Program
{
    static void Main(string[] args)
    {
        Car c1 = new()
        {
            Make = "Toyota",
            Model = "Camry",
            MPG = 30,
        };
        Car c2 = new()
        {
            Make = "Toyota",
            Model = "4Runner",
            MPG = 20,
        };
        Car c3 = new()
        {
            Make = "Honda",
            Model = "Civic",
            MPG = 32,
        };
        Car c4 = new()
        {
            Make = "Mazda",
            Model = "Mazda6",
            MPG = 27,
        };
        Car c5 = new()
        {
            Make = "Ferrari",
            Model = "F1",
            MPG = 15,
        };

        // Normal way to filter
        List<Car> carList = [c1, c2, c3, c4, c5];
        List<Car> wantToBuy = [];

        foreach (Car car in carList)
        {
            if (car.MPG >= 25)
                wantToBuy.Add(car);
        }

        foreach (Car car in wantToBuy)
        {
            System.Console.WriteLine(car);
        }

        // Using Linq - like SQL but instructor usually doesn't do it this way
        var wantToBuyLinq = from car in carList where car.MPG >= 25 select car;

        foreach (Car car in wantToBuyLinq)
        {
            System.Console.WriteLine(car);
        }

        //How Kung does it and how I should do it
        var wantToBuyLinq2 = carList.Where(car => car.MPG >= 25 && c1.Model.Contains('C')).ToList();

        int total = carList.Sum(c => c.MPG);

        double avg = carList.Average(c => c.MPG);

        int numOfCars = carList.Count(c => c.Make.ToLower().Contains('a'));
    }
}

public class Car
{
    public string Make { get; set; } = "";
    public string Model { get; set; } = "";
    public int MPG { get; set; }

    public override string ToString()
    {
        return $"{Make}, {Model}, {MPG}";
    }
}
