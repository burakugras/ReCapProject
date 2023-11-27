using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;

internal class Program
{
    private static void Main(string[] args)
    {


        //CarDtoTest();
        //EfCarTest();
        //InMemoryTest();

    }

    private static void CarDtoTest()
    {
        CarManager carManager = new CarManager(new EfCarDal());

        foreach (var car in carManager.GetCarDetails())
        {
            Console.WriteLine($"{car.CarName} / {car.BrandName} / {car.ColorName} / {car.DailyPrice}");

        }
    }

    private static void EfCarTest()
    {
        Car mercedes = new Car() { BrandId = 3, Name = "Mercedes", ColorId = 3, DailyPrice = 25000, ModelYear = 2018, Description = "Diesel motor Mercedes" };
        Car ferrari = new Car() { BrandId = 4, Name = "Ferrari", ColorId = 3, DailyPrice = 55000, ModelYear = 2023, Description = "Ultra luxury Ferrari" };
        Car volvo = new Car() { BrandId = 5, Name = "Volvo", ColorId = 3, DailyPrice = 5000, ModelYear = 2023, Description = "cheap model for Volvo" };

        CarManager carManager = new CarManager(new EfCarDal());

        carManager.Add(mercedes);
        carManager.Add(ferrari);
        carManager.Add(volvo);

        carManager.Delete(mercedes);
        carManager.Delete(ferrari);
        carManager.Delete(volvo);
    }

    private static void InMemoryTest()
    {
        ICarService carService = new CarManager(new InMemoryCarDal());

        Car mercedes = new Car() { Id = 3, BrandId = 3, Name = "Mercedes", ColorId = 3, DailyPrice = 25000, ModelYear = 2018, Description = "Diesel motor Mercedes" };

        Car ferrari = new Car() { Id = 4, BrandId = 4, Name = "Ferrai", ColorId = 3, DailyPrice = 55000, ModelYear = 2023, Description = "Ultra luxury Ferrari" };

        Car volvo = new Car() { Id = 5, BrandId = 5, Name = "Volvo", ColorId = 3, DailyPrice = 5000, ModelYear = 2023, Description = "cheap model for Volvo" };

        carService.Add(mercedes);
        carService.Add(ferrari);
        carService.Add(volvo);

        carService.Delete(volvo);


        //Console.WriteLine(carService.GetCarById(4).BrandName);

        ferrari.Name = "Ferrari";
        carService.Update(ferrari);

        foreach (var cars in carService.GetAll())
        {
            Console.WriteLine(cars.Name);
        }
    }
}