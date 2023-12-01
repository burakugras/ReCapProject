using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, RentACarDBContext>, IRentalDal
    {
        public List<RentalDetailDto> GetRentalDetails()
        {
            using (RentACarDBContext context = new RentACarDBContext())
            {
                var result = from c in context.Customers
                             join u in context.Users
                             on c.UserId equals u.Id
                             join r in context.Rentals
                             on c.Id equals r.CustomerId
                             join ca in context.Cars
                             on r.CarId equals ca.Id

                             select new RentalDetailDto
                             {
                                 FirstName = u.FirstName,
                                 LastName = u.LastName,
                                 RentDate = r.RentDate,
                                 ReturnDate = r.RentDate,
                                 CarName = ca.Name
                             };
                return result.ToList();
            }
        }
        //carName FirstName  LastName RentDate   ReturnDate

        public Rental GetLastRentalByCarId(int carId)
        {
            using (RentACarDBContext context = new RentACarDBContext())
            {
                return context.Rentals.Where(r => r.CarId == carId).OrderByDescending(r => r.RentDate).FirstOrDefault();
            }
        }
    }
}
