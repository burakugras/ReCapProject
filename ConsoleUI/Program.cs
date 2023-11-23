﻿using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.InMemory;
using Entities.Abstract;
using Entities.Concrete;

internal class Program
{
    private static void Main(string[] args)
    {
        ICarService carService = new CarManager(new InMemoryCarDal());

        Car mercedes = new Car() { Id = 3, BrandId = 3, BrandName = "Mercedes", ColorId = 3, DailyPrice = 25000, ModelYear = 2018, Description = "Diesel motor Mercedes" };

        Car ferrari = new Car() { Id = 4, BrandId = 4, BrandName = "Ferrai", ColorId = 3, DailyPrice = 55000, ModelYear = 2023, Description = "Ultra luxury Ferrari" };

        Car volvo = new Car() { Id = 5, BrandId = 5, BrandName = "Volvo", ColorId = 3, DailyPrice = 5000, ModelYear = 2023, Description = "cheap model for Volvo" };

        carService.Add(mercedes);
        carService.Add(ferrari);
        carService.Add(volvo);

        carService.Delete(volvo);


        //Console.WriteLine(carService.GetCarById(4).BrandName);

        ferrari.BrandName = "Ferrari";
        carService.Update(ferrari);

        foreach (var cars in carService.GetAll())
        {
            Console.WriteLine(cars.BrandName);
        }


    }
}