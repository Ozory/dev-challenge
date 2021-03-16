using AutoMapper;
using Desafio.Umbler.Application;
using Desafio.Umbler.Controllers;
using Desafio.Umbler.Infrastructure;
using Desafio.Umbler.Models;
using Desafio.Umbler.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using Desafio.Umbler.Application.Mappers;
using Desafio.Umbler.ViewModels;
using DnsClient;
using Desafio.Umbler.Infrastructure.Interfaces;

namespace Desafio.Umbler.Test
{
    [TestClass]
    public class ControllersTest
    {

        private readonly MapperConfiguration config = new MapperConfiguration(cfg => cfg.AddProfile<DomainProfile>());

        private static DbContextOptions<DatabaseContext> GetOptions()
        {
            return new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "challange")
                .Options;
        }

        [TestMethod]
        public void Home_Index_returns_View()
        {
            //arrange 
            var controller = new HomeController();

            //act
            var response = controller.Index();
            var result = response as ViewResult;

            //assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Home_Error_returns_View_With_Model()
        {
            //arrange 
            var controller = new HomeController();
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();

            //act
            var response = controller.Error();
            var result = response as ViewResult;
            var model = result.Model as ErrorViewModel;

            //assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(model);
        }

        [TestMethod]
        public async Task Domain_In_Database()
        {
            var options = GetOptions();

            var domain = new Domain { Id = 1, Ip = "192.168.0.1", Name = "test.com", UpdatedAt = DateTime.Now, HostedAt = "umbler.corp", Ttl = 60, WhoIs = "Ns.umbler.com" };

            // Insert seed data into the database using one instance of the context
            using (var db = new DatabaseContext(options))
            {
                db.Domains.Add(domain);
                db.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var db = new DatabaseContext(options))
            {
                var mockService = new SearchDomainService(db, new UmblerWhoisClient(), new LookupClient());
                var mockApplication = new SearchDomainApplication(mockService, config.CreateMapper());
                var controller = new DomainController(mockApplication);

                //act
                var response = await controller.Get("test.com");
                var result = response as OkObjectResult;
                var obj = result.Value as DomainViewModel;
                Assert.AreEqual(obj.Id, domain.Id);
                Assert.AreEqual(obj.Ip, domain.Ip);
                Assert.AreEqual(obj.Name, domain.Name);
            }
        }

        [TestMethod]
        public async Task Domain_Not_In_Database()
        {
            // Use a clean instance of the context to run the test
            using (var db = new DatabaseContext(GetOptions()))
            {
                var mockService = new SearchDomainService(db, new UmblerWhoisClient(), new LookupClient());
                var mockApplication = new SearchDomainApplication(mockService, config.CreateMapper());
                var controller = new DomainController(mockApplication);

                //act
                var response = await controller.Get("test.com");
                var result = response as OkObjectResult;
                var obj = result.Value as DomainViewModel;
                Assert.IsNotNull(obj);
            }
        }

        [TestMethod]
        public async Task Domain_Moking_LookupClient()
        {
            //arrange 
            var lookupClient = new Mock<IUmblerLookupClient>(MockBehavior.Strict);
            var domainName = "test.com";

            var dnsResponse = new Mock<IDnsQueryResponse>(MockBehavior.Strict);
            lookupClient.Setup(f => f.QueryAsync(domainName)).Returns(Task.FromResult(dnsResponse.Object));

            // Use a clean instance of the context to run the test
            using (var db = new DatabaseContext(GetOptions()))
            {
                //inject lookupClient in controller constructor

                //------------------------------------------------------------
                // lookupClient.Object methods (queryAsync) is allways
                // returning null. So I keep the literal intance of class
                //------------------------------------------------------------
                var mockService = new SearchDomainService(db, new UmblerWhoisClient(), lookupClient.Object);
                var mockApplication = new SearchDomainApplication(mockService, config.CreateMapper());
                var controller = new DomainController(mockApplication);

                //act
                var response = await controller.Get("test.com");
                var result = response as OkObjectResult;
                var obj = result.Value as DomainViewModel;

                Assert.IsNotNull(obj);
                Assert.IsNotNull(dnsResponse.Object);
            }
        }

        [TestMethod]
        public async Task Domain_Moking_WhoisClient()
        {
            // arrange
            // whois is a static class, we need to create a class to "wrapper" in a mockable version of WhoisClient
            var whoisClient = new Mock<IUmblerWhoisClient>(MockBehavior.Strict);
            var domainName = "test.com";

            // Use a clean instance of the context to run the test
            using (var db = new DatabaseContext(GetOptions()))
            {
                //inject lookupClient in controller constructor

                //------------------------------------------------------------
                // whoisClient.Object methods (queryAsync) is allways
                // returning null. So I keep the literal intance of class
                //------------------------------------------------------------
                var mockService = new SearchDomainService(db, whoisClient.Object, new LookupClient());
                var mockApplication = new SearchDomainApplication(mockService, config.CreateMapper());
                var controller = new DomainController(mockApplication);

                //act
                var response = await controller.Get(domainName);
                var result = response as OkObjectResult;
                var obj = result.Value as DomainViewModel;

                Assert.IsNotNull(obj);
            }
        }
    }
}