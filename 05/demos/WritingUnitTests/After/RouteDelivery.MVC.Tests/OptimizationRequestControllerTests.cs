using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Hangfire;
using RouteDelivery.Data;
using RouteDelivery.OptimizationEngine;
using RouteDelivery.MVC;
using RouteDelivery.Models;

namespace RouteDelivery.MVC.Tests
{
    [TestClass]
    public class OptimizationRequestControllerTests
    {
        [TestMethod]
        public void CheckOnOptimizationRequestCreate_BackgroundjobSchedules()
        {
            //Arrange
            var backgroundjob = Substitute.For<IBackgroundJobClient>();
            var uof = Substitute.For<IUnitOfWork>();
            var oe = Substitute.For<IOptimizationEngine>();

            var controller = new OptimizationRequestController(uof, oe, backgroundjob);
            var optimizationRequest = new OptimizationRequest();

            //Act
            controller.Create(optimizationRequest);

            //Assert
            backgroundjob.ReceivedWithAnyArgs().Schedule(() => oe.OptimizeDeliveries(optimizationRequest.ID),
                                        TimeSpan.FromMinutes(optimizationRequest.OptimizeAfterMinuntes));
        }
    }
}
