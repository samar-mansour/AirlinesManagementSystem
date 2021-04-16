using AirlineManagementSystem.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// class that gives the airline companies ability to change setting and flight info
/// create new exceptions
/// </summary>
namespace AirlineManagementSystem.BusinessLogic_Facades
{
    public class LoggedInAirlineFacade : AnonymousUserFacade, ILoggedInAirlineFacade
    {
        public void CancelFlight(LoginToken<AirlineCompany> token, Flights flight)
        {
            if (token != null)
            {
                if ((token.User.ID).Equals(flight.AirlineCompId))
                {
                    _flightDAO.Remove(flight);
                }
                throw new FlightNotExistsException($"Flight not exists: {token.User.ID}");
            }
            throw new ArgumentNullException();
        }

        public void ChangeMyPassword(LoginToken<AirlineCompany> token, string oldPassword, string newPassword)
        {
            Users user = new Users();
            if (token != null)
            {
                if ((token.User.UserId).Equals(user.ID))
                {
                    if (user.Password == oldPassword)
                    {
                        user.Password = newPassword;
                        _userDAO.Update(user);
                    };
                }
            }
            throw new ArgumentNullException();
        }

        public void CreateFlight(LoginToken<AirlineCompany> token, Flights flight)
        {
            if (token != null)
            {
                if (!(token.User.ID).Equals(flight.AirlineCompId))
                {
                    _flightDAO.Add(flight);
                }
                throw new FlightAlreadyExistsException($"Cannot create the flight: {flight.ID}. Flight is already exists");
            }
            throw new ArgumentNullException();
        }

        public IList<Tickets> GetAllFlights(LoginToken<AirlineCompany> token)
        {
            Tickets t = new Tickets();
            if (token != null)
            {
                if ((token.User.ID).Equals(t.FlightID))
                {
                   return _ticketDAO.GetAll();
                }
            }
            return null;
        }

        
        public IList<Tickets> GetAllTickets(LoginToken<AirlineCompany> token)
        {
            List<Tickets> tickets = new List<Tickets>();
            Tickets t = new Tickets();
            if (token != null)
            {
                if ((token.User.ID).Equals(t.FlightID))
                {
                    tickets.Add(_ticketDAO.Get(t.ID));
                    return tickets;
                }
            }
            return null;
        }

        public void ModifyAirlineDetails(LoginToken<AirlineCompany> token, AirlineCompany airline)
        {
            if (token != null)
            {
                _airlineDAO.Update(airline);
            }
            throw new ArgumentNullException();
        }

        public void UpdateFlight(LoginToken<AirlineCompany> token, Flights flight)
        {
            if (token != null)
            {
                if ((token.User.ID).Equals(flight.AirlineCompId))
                {
                    _flightDAO.Update(flight);
                }
            }
            throw new ArgumentNullException();
        }
    }
}
