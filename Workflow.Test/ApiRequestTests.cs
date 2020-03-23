using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using FakeItEasy.Configuration;
using FakeItEasy;

using System.Net.Http;

using Workflow.Services;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Internals;

using Workflow.ViewModels;
using Workflow.Models;
using Workflow.Services;
using System.Threading.Tasks;


namespace Workflow.Test
{
    public class ApiRequestTests
    {
        HttpService service { get; set; }
        [SetUp]
        public void SetUp()
        {
            var platform = A.Fake<IPlatformServices>();
            Device.PlatformServices = platform;
            service = new HttpService();
        }
        [Test]
        public async Task RegisterWorksTest()
        {
            var auth = new UserAuthModel()
            {
                Email = "anton.motin.dev@gmail.com",
                Password = "testtest",
                Name = "Anton",
                Phone = "79157508874"
            };
            var response = await service.PostRequest<ResponseModel<UserModel>, UserAuthModel>("user/register", auth);
            Assert.IsTrue(response.Code == 200);
        }

        [Test]
        public async Task LoginWorksTest()
        {
            var auth = new UserAuthModel();
            auth.Email = "anton.motin.dev@gmail.com";
            auth.Password = "testtest";
            var response = await service.PostRequest<ResponseModel<UserModel>, UserAuthModel>("user/login", auth);
            Assert.Greater(response.Code, 0);
        }
    }
}
