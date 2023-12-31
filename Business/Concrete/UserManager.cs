﻿using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }
        public IResult Add(User user)
        {
            _userDal.Add(user);
            return new SuccessResult(Messages.Added);
        }

        public IResult Delete(User user)
        {
            _userDal.Delete(user);
            return new SuccessResult();
        }

        public IDataResult<User> GetById(int userId)
        {
            var user = _userDal.Get(u => u.UserId == userId);
            return new SuccessDataResult<User>(user);
        }

        public IResult Update(User user)
        {
            _userDal.Update(user);
            return new SuccessResult();
        }

        IDataResult<List<User>> IUserService.GetAll()
        {
            if (DateTime.Now.Hour != 23)
            {
                return new SuccessDataResult<List<User>>(_userDal.GetAll(), Messages.Listed);
            }
            else
            {
                return new ErrorDataResult<List<User>>(Messages.MaintenanceTime);
            }
        }
    }
}
