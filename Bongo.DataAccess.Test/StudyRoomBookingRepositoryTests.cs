using Bongo.DataAccess.Repository;
using Bongo.Models.Model;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections;

namespace Bongo.DataAccess.Test
{
    [TestFixture]
    public class StudyRoomBookingRepositoryTests
    {
        private StudyRoomBooking studyRoomBooking_One;
        private StudyRoomBooking studyRoomBooking_Two;
        private DbContextOptions<ApplicationDbContext> options;
        public StudyRoomBookingRepositoryTests()
        {
            studyRoomBooking_One = new StudyRoomBooking()
            {
                FirstName = "Ben1",
                LastName = "Spark1",
                BookingId = 11,
                Date = new DateTime(2023, 1, 1),
                Email = "ben1@gmail.com",
                StudyRoomId = 1,
            };

            studyRoomBooking_Two = new StudyRoomBooking()
            {
                FirstName = "Ben2",
                LastName = "Spark2",
                BookingId = 22,
                Date = new DateTime(2023, 2, 2),
                Email = "ben2@gmail.com",
                StudyRoomId = 2,
            };
        }

        [SetUp]
        public void SetUp()
        {
            options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "temp_Bongo").Options;
        }

        [Test]
        [Order(1)]
        public void SaveBooking_BookingOne_CheckTheValuesFromDataBase()
        {
            //Arrange
            //Act

            using (var context = new ApplicationDbContext(options))
            {
                var repository = new StudyRoomBookingRepository(context);
                repository.Book(studyRoomBooking_One);
            }

            //Assert
            using (var context = new ApplicationDbContext(options))
            {
                var bookingFromDb = context.StudyRoomBookings.FirstOrDefault(u => u.BookingId == 11);
                Assert.AreEqual(studyRoomBooking_One.BookingId, bookingFromDb.BookingId);
                Assert.AreEqual(studyRoomBooking_One.FirstName, bookingFromDb.FirstName);
                Assert.AreEqual(studyRoomBooking_One.LastName, bookingFromDb.LastName);
                Assert.AreEqual(studyRoomBooking_One.Email, bookingFromDb.Email);
                Assert.AreEqual(studyRoomBooking_One.Date, bookingFromDb.Date);
            }

        }

        [Test]
        [Order(2)]
        public void GetAllBooking_BookingOneAndTwo_CheckBothTheBookingFromDataBase()
        {
            //Arrange
            var expectedResult = new List<StudyRoomBooking>() { studyRoomBooking_One, studyRoomBooking_Two };

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                var repository = new StudyRoomBookingRepository(context);
                repository.Book(studyRoomBooking_One);
                repository.Book(studyRoomBooking_Two);
            }
            //Act
            List<StudyRoomBooking> actualList;
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new StudyRoomBookingRepository(context);
                actualList = repository.GetAll(null).ToList();
            }

            //Assert
            CollectionAssert.AreEqual(expectedResult, actualList,new BookingCompare());

        }
        private class BookingCompare : IComparer
        {
            public int Compare(object x, object y)
            {
                var bookin1 = (StudyRoomBooking)x;
                var bookin2 = (StudyRoomBooking)y;
                if (bookin1.BookingId != bookin2.BookingId)
                {
                    return 1;
                }
                return 0;
            }
        }

    }
}
