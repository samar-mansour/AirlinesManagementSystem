﻿using AirlineManagementSystem.BusinessLogic_Facades;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// this is a singletone login class which only checks the username and password
/// throw the LoginToken class which is type IUser
/// also it connected to the facades "Business Logic layer"
/// /// </summary>
namespace AirlineManagementSystem.Login
{
    public class LoginService : ConnectionDataInfo, ILoginService
    {
        private static object singletone_key = new object();
        private static object key = new object();
        private static LoginService m_Instance;

        private IAirlineCompanyDAO _airlineDAO;
        private ICustomerDAO _customerDAO;
        private IAdministratorsDAO _adminDAO;
        private IUsersDAO _userDAO;

        public static LoginService GetInstance
        {
            get
            {
                if (m_Instance == null)
                {
                    lock (singletone_key)
                    {
                        if (m_Instance == null)
                        {
                            m_Instance = new LoginService();
                        }
                    }
                }
                return m_Instance;

            }
        }
        public LoginService()
        {

        }

        public bool TryLogin(string userName, string password, out LoginToken<IUser> token, out FacadeBase facadeBase)
        {
            Users user = new Users();
            token = null;
            facadeBase = null;

            if (userName is null)
                throw new ArgumentNullException($"username cannot be: {userName}");
            if (password is null)
                throw new ArgumentNullException($"password cannot be: {password}");

            //in case it's admin "level 4"
            if (userName == "admin" && password == "9999")
            {
                if (_adminDAO.GetUserByUsername(userName).Equals(_userDAO.GetUserByUsername(userName)))
                {
                    if (user.Password == password)
                    {
                        token = new LoginToken<Administrators>();
                        facadeBase = new LoggedInAdministratorFacade();
                        return true;
                    }
                }

            }
            else
            {
                if (UserAuthentication.IsUserAuthorized(userName, password))
                {
                    if (user.UserRole == 1)
                    {
                        Administrators admin = _adminDAO.Get(user.ID);
                        token = new LoginToken<Administrators>()
                        {
                            User = admin
                        };
                        facadeBase = new LoggedInAdministratorFacade();
                        return true;
                    }
                    else if (user.UserRole == 2)
                    {
                        Administrators admin = _adminDAO.Get(user.ID);
                        token = new LoginToken<Administrators>()
                        {
                            User = admin
                        };
                        facadeBase = new LoggedInAdministratorFacade();
                        return true;
                    }
                    else if (user.UserRole == 3)
                    {
                        Administrators admin = _adminDAO.Get(user.ID);
                        token = new LoginToken<Administrators>()
                        {
                            User = admin
                        };
                        facadeBase = new LoggedInAdministratorFacade();
                        return true;
                    }
                    else if (user.ID.Equals(_customerDAO.Get(user.ID)))
                    {
                        Customer customer = _customerDAO.Get(user.ID);
                        token = new LoginToken<Customer>()
                        {
                            User = customer
                        };
                        facadeBase = new LoggedInCustomerFacade();
                        return true;
                    }
                    else
                    {
                        AirlineCompany airline = _airlineDAO.Get(user.ID);
                        token = new LoginToken<AirlineCompany>()
                        {
                            User = airline
                        };
                        facadeBase = new LoggedInAirlineFacade();
                        return true;
                    }
                }
            }
            my_logger.Info($"Login Faild\n: username: {userName} password: {password},  Not excited!");
            return false;
        }
    }
}
