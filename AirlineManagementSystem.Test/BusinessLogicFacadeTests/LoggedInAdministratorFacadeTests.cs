using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirlineManagementSystem.Test
{
    [TestFixture]
    public class LoggedInAdministratorFacadeTests
    {
        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void CreateAdmin_InvalidTokenValueIsNull_ThrowArrgumentNullException()
        {
            //var logInAdminFacade = new LoggerInAdministratorFacade();
            //Assert.That(() => logInAdminFacade.CreateAdmin(new LoginToken<Administrators> { User = null }), Throws.ArgumentNullException);
        }

        [Test]
        public void CreateAdmin_AdminIdEqualToTokenUserIdAndHaveAdminAuthorityLevel_AddNewAdmin()
        {
            
        }

        [Test]
        public void CreateAdmin_AdminAlreadyExist_ThrowAdminAlreadyExistsException()
        {

        }

        [Test]
        public void CreateAdmin_AdminIdNotEqualToTokenUserIdAndDontHaveAdminAuthority_ThrowAdminLevelNotQualifiedToCreateException()
        {

        }

        [Test]
        public void CreateNewAirlinen_InvalidTokenValueIsNull_ThrowArrgumentNullException()
        {

        }

        [Test]
        public void CreateNewAirlinen_AirlineUserIdEqualToTokenUserIdAndHaveAdminAuthorityLevel_AddNewAirline()
        {

        }

        [Test]
        public void CreateNewAirlinen_AirlineAlreadyExist_ThrowAirlineAlreadyExistsException()
        {

        }

        [Test]
        public void CreateNewCustomer_InvalidTokenValueIsNull_ThrowArrgumentNullException()
        {

        }

        [Test]
        public void CreateNewCustomer_CustomerIdEqualToTokenUserIdAndHaveAdminAuthority_AddNewCustomer()
        {

        }

        [Test]
        public void CreateNewCustomer_CustomerAlreadyExist_ThrowCustomerAlreadyExistsException()
        {

        }

        [Test]
        public void GetAllCustomers_InvalidTokenValueIsNull_ThrowArrgumentNullException()
        {

        }

        [Test]
        public void GetAllCustomers_TokenNotNull_ReturnAllCustomers()
        {

        }

        [Test]
        public void RemoveAdmin_InvalidTokenValueIsNull_ThrowArrgumentNullException()
        {

        }

        [Test]
        public void RemoveAdmin_AdminThatHaveAnAuthorityLevelToRemoveOtherUsers_RemovieAdmin()
        {

        }

        [Test]
        public void RemoveAdmin_AdminThatDontHaveAnAuthorityLevelToRemoveOtherUsers_ReturnString()
        {

        }

        [Test]
        public void RemoveAirline_InvalidTokenValueIsNull_ThrowArrgumentNullException()
        {

        }

        [Test]
        public void RemoveAirline_UserHaveAnAuthorityLevelEqualToOneUnappleToRemove_ReturnStringMessage()
        {

        }

        [Test]
        public void RemoveAirline_UserThatHaveAnAuthorityLevelEqualBetweenTwoAndFourToRemoveOtherUsers_RemoveAirline()
        {

        }

        [Test]
        public void RemoveCustomer_InvalidTokenValueIsNull_ThrowArrgumentNullException()
        {

        }

        [Test]
        public void RemoveCustomer_UserHaveAnAuthorityLevelEqualToOneUnappleToRemove_ReturnStringMessage()
        {

        }

        [Test]
        public void RemoveCustomer_UserThatHaveAnAuthorityLevelEqualBetweenTwoAndFourToRemoveOtherUsers_RemoveAirline()
        {

        }

        [Test]
        public void UpdateAdmin_InvalidTokenValueIsNull_ThrowArrgumentNullException()
        {

        }

        [Test]
        public void UpdateAdmin_AdminThatHaveAnAuthorityLevelEqualToThreeOrFourToUpdateInformation_UpdateAdmin()
        {

        }

        [Test]
        public void UpdateAdmin_AdminDontHaveAnAuthorityLevelToUpdateInformation_ReturnErrorMessage()
        {

        }

        [Test]
        public void UpdateAirlineDetails_InvalidTokenValueIsNull_ThrowArrgumentNullException()
        {

        }

        [Test]
        public void UpdateAirlineDetails_HavingAnAuthorityLevelEqualToThreeOrFourToUpdateAirlineInformation_UpdateAirline()
        {

        }

        [Test]
        public void UpdateAirlineDetails_DontHaveAnAuthorityLevelToUpdateInformation_ReturnErrorMessage()
        {

        }

        [Test]
        public void UpdateCustomerDetails_InvalidTokenValueIsNull_ThrowArrgumentNullException()
        {

        }

        [Test]
        public void UpdateCustomerDetails_HavingAnAuthorityLevelEqualToThreeOrFourToUpdateCustomerInformation_UpdateCustomerDetails()
        {

        }

        [Test]
        public void UpdateCustomerDetails_DontHaveAnAuthorityLevelToUpdateCustomerInformation_ReturnErrorMessage()
        {

        }
    }
}
