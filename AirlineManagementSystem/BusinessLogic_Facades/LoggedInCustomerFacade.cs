using AirlineManagementSystem.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// class that give the user "customer" to change flights, it show them:
/// 1.availabe flights 
/// 2.purchese ticket
/// 3.cancel ticket
/// creating new exceptions
/// </summary>
namespace AirlineManagementSystem.BusinessLogic_Facades
{
    public class LoggedInCustomerFacade : AnonymousUserFacade, ILoggedInCustomerFacade
    {
        public void CancelTicket(LoginToken<Customer> token, Tickets ticket)
        {
            if (token != null)
            {
                if ((token.User.ID).Equals(ticket.CustomerID))
                {
                    _ticketDAO.Remove(ticket);
                }
                throw new CustomerNotFoundException($"Ticket not exist nor Customer: [ticket not found: {ticket.ID}");
            }
            throw new ArgumentNullException();
        }

        public IList<Flights> GetAllMyFlights(LoginToken<Customer> token)
        {
            Flights flight = new Flights();
            Tickets ticket = new Tickets();
            if (token != null)
            {
                if ((token.User.ID).Equals(ticket.CustomerID) && (ticket.FlightID).Equals(flight.ID))
                {
                    return _flightDAO.GetAll();
                }
                throw new NoFlightsException($"No Flights availabe");
            }
            return null;
        }

        public Tickets PurchaseTicket(LoginToken<Customer> token, Flights flight)
        {
            Tickets ticket = new Tickets();
            if (token != null)
            {
                if (!(token.User.ID).Equals(ticket.CustomerID) && (ticket.FlightID).Equals(flight.ID) && flight.RemainingTickets <= 0)
                {
                    _flightDAO.Add(flight);
                    flight.RemainingTickets--;
                    return _ticketDAO.Get(ticket.ID);
                }
                Console.WriteLine($"No remaining tickets or you already purchase ticket");
            }
            return null;
        }
    }
}
