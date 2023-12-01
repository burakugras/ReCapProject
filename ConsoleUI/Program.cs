using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;

internal class Program
{
    private static void Main(string[] args)
    {
        RentalDetailsDtoTest();



        //UpdateTest();
        //CarDtoTest();
        //EfCarTest();
        //InMemoryTest();
    }

    private static void RentalDetailsDtoTest()
    {
        Car car = new Car() { BrandId = 1, ColorId = 1, Name = "Toyota", ModelYear = 2019, DailyPrice = 22000, Description = "High segment Toyota Corolla" };

        CarManager carManager = new CarManager(new EfCarDal());
        //carManager.Add(car);

        User user = new User() { FirstName = "Mehmet", LastName = "Beyaz", Email = "mbeyaz@mail.com", Password = "123456" };
        UserManager userManager = new UserManager(new EfUserDal());
        //userManager.Add(user);

        Customer customer = new Customer() { UserId = 3, CompanyName = "Avis" };
        CustomerManager customerManager = new CustomerManager(new EFCustomerDal());
        //customerManager.Add(customer);

        Rental rental = new Rental() { CarId = 1, CustomerId = 3, RentDate = new DateTime(2023, 10, DateTime.DaysInMonth(2023, 10)), ReturnDate = null };
        RentalManager rentalManager = new RentalManager(new EfRentalDal());
        //rentalManager.Add(rental);

        foreach (var rentals in rentalManager.GetRentalDetails().Data)
        {
            Console.WriteLine($"{rentals.FirstName} / {rentals.LastName} / {rentals.CarName} / {rentals.RentDate} / {rentals.ReturnDate}");
        }
    }

    private static void UpdateTest()
    {
        Color blue = new Color() { Name = "Blue" };
        Brand maserati = new Brand() { Name = "Maserati" };
        Brand volkswagen = new Brand() { Name = "Volkswagen" };

        ColorManager colorManager = new ColorManager(new EfColorDal());
        blue.Id = 1003;
        blue.Name = "Dark Blue";
        colorManager.Update(blue);

        BrandManager brandManager = new BrandManager(new EfBrandDal());
        maserati.Id = 1002;
        maserati.Name = "Masserati";
        brandManager.Update(maserati);
        brandManager.Add(volkswagen);

        CarManager carManager = new CarManager(new EfCarDal());


        using (RentACarDBContext context = new RentACarDBContext())
        {
            int carIdToUpdate = 4; //volvo ----> Volkwagen
            var carToUpdate = context.Cars.Find(carIdToUpdate);

            if (carToUpdate != null)
            {
                carToUpdate.Id = 4;
                carToUpdate.Name = "Volkswagen";
                carToUpdate.BrandId = 1003;
                carToUpdate.ColorId = 3;
                carToUpdate.ModelYear = 2022;
                carToUpdate.DailyPrice = 7500;
                carToUpdate.Description = "A wagen for folk";
                carManager.Update(carToUpdate);
            }
            else
            {
                Console.WriteLine("No Match!");
            }
        }
    }

    private static void CarDtoTest()
    {
        CarManager carManager = new CarManager(new EfCarDal());

        foreach (var car in carManager.GetCarDetails().Data)
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

        foreach (var cars in carService.GetAll().Data)
        {
            Console.WriteLine(cars.Name);
        }
    }
}