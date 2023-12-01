using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, RentACarDBContext>, IRentalDal
    {
        public Rental GetLastRentalByCarId(int carId)
        {
            using (RentACarDBContext context = new RentACarDBContext())
            {
                return context.Rentals.Where(r => r.CarId == carId).OrderByDescending(r => r.RentDate).FirstOrDefault();
            }
        }
    }
}
