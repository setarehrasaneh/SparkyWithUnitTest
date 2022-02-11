using Bongo.Core.Services;
using Bongo.DataAccess.Repository;
using Bongo.DataAccess.Repository.IRepository;
using Bongo.Models.Model;
using Bongo.Models.Model.VM;
using Moq;
using NUnit.Framework;

namespace Bongo.Core.Test
{
    [TestFixture]
    public class StudyRoomBookingServiceTest
    {
        private Mock<IStudyRoomBookingRepository> _studyRoomBookingRepositoryMock;
        private Mock<IStudyRoomRepository> _studyRoomRepositoryMock;
        private StudyRoomBookingService _studyRoomBookingService;
        private StudyRoomBooking _request;
        private List<StudyRoom> _availableStudyRoom;

        [SetUp]
        public void SetUp()
        {
            _request = new StudyRoomBooking
            {
                FirstName = "Ben",
                LastName = "Spark",
                Email = "ben@gmil.com",
                Date = new DateTime(2022, 01, 01),
            };
            _availableStudyRoom = new List<StudyRoom>()
            {
                new StudyRoom
                {
                    Id = 10,RoomName ="Michigan",RoomNumber="A202"
                }
            };

            _studyRoomBookingRepositoryMock = new Mock<IStudyRoomBookingRepository>();
            _studyRoomRepositoryMock = new Mock<IStudyRoomRepository>();
            _studyRoomRepositoryMock.Setup(x => x.GetAll()).Returns(_availableStudyRoom);
            _studyRoomBookingService = new StudyRoomBookingService(
                _studyRoomBookingRepositoryMock.Object,
                _studyRoomRepositoryMock.Object
                );
        }

        [TestCase]
        public void GetAllBooking_InvokeMethod_CheckIfRepoIsCalled()
        {
            _studyRoomBookingService.GetAllBooking();
            _studyRoomBookingRepositoryMock.Verify(u => u.GetAll(null), Times.Once);

        }

        [TestCase]

        public void BookingException_NullRequest_ThrowsException()
        {
            var exception = Assert.Throws<ArgumentNullException>(
                () => _studyRoomBookingService.BookStudyRoom(null));

            Assert.AreEqual("Value cannot be null. (Parameter 'request')", exception.Message);
            Assert.AreEqual("request", exception.ParamName);
        }

        [Test]
        public void StudyRoomBooking_SaveBookingWhithAvailableRoom_ReturnResultWhitAvailableValues()
        {
            //Arrange
            StudyRoomBooking saveStudyRoomBooking = null;
            _studyRoomBookingRepositoryMock.Setup(x => x.Book(It.IsAny<StudyRoomBooking>())).
            Callback<StudyRoomBooking>(booking =>
            {
                saveStudyRoomBooking = booking;
            });

            //Act
            _studyRoomBookingService.BookStudyRoom(_request);

            //Assert
            _studyRoomBookingRepositoryMock.Verify(x => x.Book(It.IsAny<StudyRoomBooking>()), Times.Once);
            Assert.IsNotNull(saveStudyRoomBooking);
            Assert.AreEqual(_request.FirstName, saveStudyRoomBooking.FirstName);
            Assert.AreEqual(_request.LastName, saveStudyRoomBooking.LastName);
            Assert.AreEqual(_request.Email, saveStudyRoomBooking.Email);
            Assert.AreEqual(_request.Date, saveStudyRoomBooking.Date);
            Assert.AreEqual(_availableStudyRoom.First().Id, saveStudyRoomBooking.StudyRoomId);
        }

        [Test]
        public void StudyRoomBookinResultCheck_InputRequest_ValuesMatchInRequest()
        {
            StudyRoomBookingResult result = _studyRoomBookingService.BookStudyRoom(_request);
            Assert.NotNull(result);
            Assert.AreEqual(_request.FirstName, result.FirstName);
            Assert.AreEqual(_request.LastName, result.LastName);
            Assert.AreEqual(_request.Email, result.Email);
            Assert.AreEqual(_request.Date, result.Date);
        }

        [TestCase(true, ExpectedResult = StudyRoomBookingCode.Success)]
        [TestCase(false, ExpectedResult = StudyRoomBookingCode.NoRoomAvailable)]
        public StudyRoomBookingCode ResultCodeSuccess_RoomAvailability_ReturnSuccessResultCode(bool roomAvailability)
        {
            if (!roomAvailability)
            {
                _availableStudyRoom.Clear();
            }
            return _studyRoomBookingService.BookStudyRoom(_request).Code;
        }

        [TestCase(0, false)]
        [TestCase(55, true)]
        public void StudyRoomBooking_BookRoomWithAvailability_ReturnsBookingId
            (int expectedBookingId, bool roomAvailability)
        {
            if (!roomAvailability)
            {
                _availableStudyRoom.Clear();
            }

            _studyRoomBookingRepositoryMock.Setup(x => x.Book(It.IsAny<StudyRoomBooking>())).
            Callback<StudyRoomBooking>(booking =>
            {
                booking.BookingId = 55;
            });

            var result = _studyRoomBookingService.BookStudyRoom(_request);

            Assert.AreEqual(expectedBookingId, result.BookingId);

        }

        [Test]
        public void StudyNotInvoke_BookRoomWithAvailability_ReturnsBookingId()
        {
            _availableStudyRoom.Clear();
            var result = _studyRoomBookingService.BookStudyRoom(_request);
           _studyRoomBookingRepositoryMock.Verify(x => x.Book(It.IsAny<StudyRoomBooking>()), Times.Never);
        }
    }
}
