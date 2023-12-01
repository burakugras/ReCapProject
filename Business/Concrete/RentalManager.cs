using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;
        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }
        public IResult Add(Rental rental)
        {
            IResult result = CheckRentalRules(rental);//burada bir sonuç istiyor

            if (result.Success)
            {
                _rentalDal.Add(rental);
                return new SuccessResult(Message.RentalAdded);
            }

            return result;

        }

        private IResult CheckRentalRules(Rental rental)
        {
            if (rental.ReturnDate == null)
            {
                return new ErrorResult(Message.RentalInvalid);
            }

            if (IsCarNotReturnedYet(rental.CarId))
            {
                return new ErrorResult(Message.RentalInvalid);
            }

            return new SuccessResult(Message.RentalAdded);
        }

        private bool IsCarNotReturnedYet(int carId)
        {
            var lastRental = _rentalDal.GetLastRentalByCarId(carId);
            //en son durumu kiralanmamışsa ve returnDate null ise araç dönmedi yani, dönmeme durumu true
            if (lastRental != null && lastRental.ReturnDate == null)
            {
                return true;
            }
            return false;
        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult(Message.ProductDeleted);
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccesDataResult<List<Rental>>(_rentalDal.GetAll());
        }

        public IDataResult<Rental> GetRentalById(int id)
        {
            return new SuccesDataResult<Rental>(_rentalDal.Get(r => r.Id == id));
        }

        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult(Message.ProductUpdated);
        }
    }
}
