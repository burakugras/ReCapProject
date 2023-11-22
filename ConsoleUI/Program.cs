using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.InMemory;

internal class Program
{
    private static void Main(string[] args)
    {
        ICarService carService=new CarManager(new InMemoryCarDal());

        foreach (var cars in carService.GetAll())
        {
            Console.WriteLine(cars.BrandName);
        }
    }
}