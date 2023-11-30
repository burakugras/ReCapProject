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
    public class BrandManager : IBrandService
    {
        IBrandDal _brandDal;
        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }
        public IResult Add(Brand car)
        {
            _brandDal.Add(car);
            return new SuccessResult(Message.ProductAdded);
        }

        public IResult Delete(Brand car)
        {
            _brandDal.Delete(car);
            return new SuccessResult(Message.ProductDeleted);
        }

        public IDataResult<List<Brand>> GetAll()
        {
            return new SuccesDataResult<List<Brand>>(_brandDal.GetAll());
        }

        public IDataResult<Brand> GetCarById(int id)
        {
            return new SuccesDataResult<Brand>(_brandDal.Get(b=>b.Id==id));
        }

        public IResult Update(Brand car)
        {
            _brandDal.Update(car);
            return new SuccessResult(Message.ProductUpdated);
        }
    }
}
