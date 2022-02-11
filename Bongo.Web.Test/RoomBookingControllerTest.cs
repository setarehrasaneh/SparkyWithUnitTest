using Bongo.Core.Services.IServices;
using Bongo.Models.Model;
using Bongo.Models.Model.VM;
using Bongo.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bongo.Web.Test
{
    [TestFixture]
    public class RoomBookingControllerTest
    {
        private Mock<IStudyRoomBookingService> _studyRoomBookingService;
        private RoomBookingController _roomBookingController;

        [SetUp]
        public void SetUp()
        {
            _studyRoomBookingService = new Mock<IStudyRoomBookingService>();
            _roomBookingController = new RoomBookingController(_studyRoomBookingService.Object);
        }

        [Test]
        public void IndexPage_CallRequest_VerifyGetAllInvoked()
        {
            _roomBookingController.Index();
            _studyRoomBookingService.Verify(x=>x.GetAllBooking(), Times.Once());
        }

        [Test]
        public void BookRoomCheck_ModelStateInvalid_ReturnView()
        {
            _roomBookingController.ModelState.AddModelError("test", "test");
            var result = _roomBookingController.Book(new StudyRoomBooking());

            ViewResult viewResult = (ViewResult)result;
            Assert.AreEqual("Book",viewResult.ViewName); 

        }

       [Test]
       public void BookRoomCheck_NotSuccessFul_NoRoomCode()
        {
            _studyRoomBookingService.Setup(x => x.BookStudyRoom(It.IsAny<StudyRoomBooking>()))
                .Returns(new StudyRoomBookingResult()
                {
                    Code = StudyRoomBookingCode.NoRoomAvailable
                });
            var result = _roomBookingController.Book(new StudyRoomBooking());
            Assert.IsInstanceOf<ViewResult>(result);    
            ViewResult viewResult = (ViewResult) result;
            Assert.AreEqual("No Study Room available for selected date", viewResult.ViewData["Error"]);
        }

        [Test]
        public void BookRoomCheck_NotSuccessFul_SuccessCodeAndRedirect()
        {
            //Arrange
            _studyRoomBookingService.Setup(x => x.BookStudyRoom(It.IsAny<StudyRoomBooking>()))
                .Returns((StudyRoomBooking booking) => new StudyRoomBookingResult()
                {
                    Code = StudyRoomBookingCode.Success,
                    FirstName = booking.FirstName,
                    LastName = booking.LastName,
                    Email = booking.Email,
                    Date = booking.Date,
                });

            //Act
            var result = _roomBookingController.Book(new StudyRoomBooking()
            {
                Date = DateTime.Now,
                Email="hello@dotnetmastery.com",
                FirstName= "Hello",
                LastName= "DotNetMastery",
                StudyRoomId = 1

            });
            //Assert

            Assert.IsInstanceOf<RedirectToActionResult>(result);
            RedirectToActionResult actionResult = (RedirectToActionResult)result;
            Assert.AreEqual("Hello", actionResult.RouteValues["FirstName"]);
            Assert.AreEqual(StudyRoomBookingCode.Success, actionResult.RouteValues["Code"]);
        }
    }
}
