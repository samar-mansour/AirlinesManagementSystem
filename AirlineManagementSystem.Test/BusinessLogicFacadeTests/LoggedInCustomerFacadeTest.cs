using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirlineManagementSystem.Test.BusinessLogicFacadeTests
{
    [TestFixture]
    public class LoggedInCustomerFacadeTest
    {
        [SetUp]
        public void Setup()
        {
            _repostory = new Mock<ILoggedInCustomerFacade>();
        }
        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void CancelTicket_InvalidTokenNullValue_ThrowArgumentNullException()
        {
            _repositry.Setup(r => )
        }

        [Test]
        public void CancelTicket_IsTokenUserEqualToCustomerID_RemoveTicket()
        {

        }

        [Test]
        public void CancelTicket_IsTokenUserNotEqualToCustomerID_ThrowCustomerNotFoundException()
        {

        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void GetAllMyFlights_InvalidTokenNullValue_ReturnNull()
        {

        }

        [Test]
        public void GetAllMyFlights_IsTokenUserEqualToCustomerIDAndFlightIdEqualToTicketFilghtId_ReturnFlightsList()
        {

        }

        [Test]
        public void GetAllMyFlights_IsTokenUserNotEqualToCustomerIDAndFlightIdNotEqualToTicketFilghtId_ThrowExceptionThereIsNoFlights()
        {

        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void PurchaseTicket_InvalidTokenNullValue_ReturnNull()
        {

        }

        [Test]
        public void PurchaseTicket_IsTokenUserNotEqualToCustomerIDAndFlightIdNotEqualToTicketFilghtIdAndRemainingTicketsGreaterThanZero_ReturnTicket()
        {

        }

        [Test]
        public void PurchaseTicket_IsTokenUserEqualToCustomerIDAndFlightIdEqualToTicketFilghtIdAndRemainingTicketsLessThanZero_ReturnStringError()
        {

        }
    }
}
