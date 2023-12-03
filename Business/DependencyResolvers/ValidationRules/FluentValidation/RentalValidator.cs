using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolvers.ValidationRules.FluentValidation
{
    public class RentalValidator : AbstractValidator<Rental>
    {
        IRentalDal _rentalDal;
        public RentalValidator(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;

            RuleFor(r => r.ReturnDate).Must((rental, returnDate) => CheckRentalRules(rental))
                .WithMessage(Message.RentalInvalid);
        }

        private bool CheckRentalRules(Rental rental)
        {
            if (rental.ReturnDate == null)
            {
                return false;
            }

            if (IsCarNotReturnedYet(rental.CarId))
            {
                return false;
            }

            return true;
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
    }
}
