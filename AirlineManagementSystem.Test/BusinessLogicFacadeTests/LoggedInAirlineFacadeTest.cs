using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirlineManagementSystem.Test.BusinessLogicFacadeTests
{
    [TestFixture]
    public class LoggedInAirlineFacadeTest
    {
        [Test]
        [TestCase (null)]
        [TestCase ("")]
        public void CancelFlight_InvalidTokenValue_ThrowArgumentNullException()
        {

        }

        [Test]
        public void CancelFlight_TokenUserIdEqualFlightAirlineCompanyID_RemoveFlight()
        {

        }

        [Test]
        public void CancelFlight_TokenUserIdNotEqualToAirlineCompanyID_ThrowFlightNotExistsException()
        {

        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void ChangeMyPassword_TokenOfTypeAirlineIsNull_ThrowArgumentNullException()
        {

        }

        [Test]
        public void ChangeMyPassword_TokenOfTypeAirlineUserIdEqualToUserIdAndPasswordIsSame_ChangePassowrdAndUpdateUserInformation()
        {

        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void CreateFlight_TokenOfTypeAirlineIsNull_ThrowArgumentNullException()
        {

        }

        [Test]
        public void CreateFlight_TokenTypeOfAirlineUserIdNotEqualToFlightId_AddNewFlight()
        {

        }

        [Test]
        public void CreateFlight_TokenTypeOfAirlineUserIdtEqualToFlightId_ThrowFlightAlreadyExistsException()
        {

        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void GetAllFlights_TokenOfTypeAirlineIsNull_ReturnEmptyTicketsList()
        {

        }

        [Test]
        public void GetAllFlights_TokenOfTypeAirlineEqualToFlightId_ReturnTicketList()
        {

        }

        [Test]
        public void ModifyAirlineDetails_TokenOfTypeAirlineIsNull_ThrowArgumentNullException()
        {

        }

        [Test]
        public void ModifyAirlineDetails_TokenOfTypeAirlineNotNull_UpdateAirline()
        {

        }


    }
}
