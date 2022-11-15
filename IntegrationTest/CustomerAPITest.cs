using API;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTest
{
    public class CustomerAPITest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> webApplicationFactory;

        public CustomerAPITest(WebApplicationFactory<Startup> webApplicationFactory)
        {
            this.webApplicationFactory = webApplicationFactory;
        }

        #region HelperMethods
        static float NextFloat(float min, float max)
        {
            System.Random random = new System.Random();
            double val = (random.NextDouble() * (max - min) + min);
            return (float)val;
        }
        #endregion

        [Fact]
        public async Task RegisterNewUser_AcceptInvitation_Login()
        {
            Fixture AutoData = new Fixture();
            string S = AutoData.Create<string>();
            var webClient = webApplicationFactory.CreateClient();
            var customer = new CustomerAPI("http://localhost:5000/", webClient);

            try
            {
                var result = await customer.RegisternewcustomerAsync(new RegisterViewModel()
                {
                    FullName = AutoData.Create<string>(),
                    FirebaseToken = AutoData.Create<string>(),
                    BirthDate = DateTime.Now.AddYears(-18),
                    Email = AutoData.Create<string>() + "@directik.com",
                    Gender = "male",
                    MobileNumber = "+905528316032"
                });

                var result2 = await customer.RegisternewcustomerAsync(new RegisterViewModel()
                {
                    FullName = AutoData.Create<string>(),
                    FirebaseToken = AutoData.Create<string>(),
                    BirthDate = DateTime.Now.AddYears(-18),
                    Email = AutoData.Create<string>() + "@directik.com",
                    Gender = "male",
                    MobileNumber = "+905528316030"
                });
                var customerdata = await customer.CustomerGETAsync(result.Id);
                string code = customerdata.LastCode.VerifyCode;

                var result1 = await customer.CheckregisteringcustomerverificationcodeAsync(new
                    VerifyCodeViewModel()
                {
                    Code = code,
                    MobileNumber = "+905528316032",
                    RefferalInformation = new RefferalInformation()
                    {
                        RefferalMobileNumber = "+905528316030"
                    }
                });


                var refferal = await customer.LoginAsync(new LoginViewModel()
                {
                    MobileNumber = "+905528316030"
                });

                var getrefferalcodedata = await customer.CustomerGETAsync(refferal.Id);

                var gettokenforlogin = await customer.CheckregisteredcustomerverificationcodeAsync(
                    new VerifyCodeViewModel()
                    {
                        Code = getrefferalcodedata.LastCode.VerifyCode
                    ,
                        MobileNumber = "+905528316030"
                    });

                webClient = webApplicationFactory.CreateClient();
                webClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + gettokenforlogin.LastMobileToken);
                customer = new CustomerAPI("http://localhost:5000/", webClient);

                var acceptthisuser = await customer.ConfirmnewcustomerasrefferalAsync(new ConfirmAsRefferalViewModel()
                {
                    Id = result.Id
                });

                var LoginNewUserCheck = await customer.LoginAsync(new LoginViewModel()
                {
                    MobileNumber = "+905528316032"
                });

                Assert.True(true);

            }
            catch (Exception Ex)
            {
                Assert.False(true, Ex.Message);
            }
        }

        [Fact]
        public async Task UpdateCustomer()
        {
            Fixture AutoData = new Fixture();
            var webClient = webApplicationFactory.CreateClient();

            var customer = new CustomerAPI("http://localhost:5000/", webClient);
            try
            {
                var result = await customer.RegisternewcustomerAsync(new RegisterViewModel()
                {
                    FullName = AutoData.Create<string>(),
                    FirebaseToken = AutoData.Create<string>(),
                    BirthDate = DateTime.Now.AddYears(-18),
                    Email = AutoData.Create<string>() + "@directik.com",
                    Gender = "male",
                    MobileNumber = "+905528316032"
                });

                var customerdata = await customer.CustomerGETAsync(result.Id);
                customerdata.FullName = "ChangedValue";

                var Customer = new CustomerViewModel()
                {
                    Id = customerdata.Id,
                    FullName = "ChangedValue",
                    FirebaseToken = AutoData.Create<string>(),
                    BirthDate = DateTime.Now.AddYears(-18),
                    Email = AutoData.Create<string>() + "@directik.com",
                    Gender = "male",
                    MobileNumber = "+905528316032"
                };
                await customer.CustomerPUTAsync(Customer);

                var getdata = await customer.CustomerGETAsync(customerdata.Id);
                getdata.Should().NotBeNull();
                getdata.FullName.Should().BeEquivalentTo("ChangedValue");
            }
            catch (Exception Ex)
            {
                Assert.False(true);
            }
        }

        [Fact]
        public async Task DeleteCustomer()
        {
            Fixture AutoData = new Fixture();
            var webClient = webApplicationFactory.CreateClient();

            var customer = new CustomerAPI("http://localhost:5000/", webClient);
            try
            {
                var result = await customer.RegisternewcustomerAsync(new RegisterViewModel()
                {
                    FullName = AutoData.Create<string>(),
                    FirebaseToken = AutoData.Create<string>(),
                    BirthDate = DateTime.Now.AddYears(-18),
                    Email = AutoData.Create<string>() + "@directik.com",
                    Gender = "male",
                    MobileNumber = "+905528316032"
                });

                var getdata = await customer.CustomerGETAsync(result.Id);
                getdata.Should().NotBeNull();

                await customer.CustomerDELETEAsync(getdata.Id);
                getdata = await customer.CustomerGETAsync(getdata.Id);
            }
            catch (Exception Ex)
            {
                Ex.Message.Should().Contain("Status: 204");
            }
        }

    }

}
