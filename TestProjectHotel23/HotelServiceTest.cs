using RazorHotelDB23InCl.Models;
using RazorHotelDB23InCl.Services;

namespace TestProjectHotel23
{
    [TestClass]
    public class HotelServiceTest
    {
        private string connectionString = Secret.connectionString;
        //"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HotelDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        [TestMethod]
        public void TestAddHotel()
        {
            //Arrange
            HotelService hotelService = new HotelService(connectionString);
            List<Hotel> hotels = hotelService.GetAllHotelAsync().Result;
            
            //Act
            int numberOfHotelsBefore = hotels.Count;
            Hotel newHotel = new Hotel(68, "TestHotel.com", "TestAlle");
            bool ok = hotelService.CreateHotelAsync(newHotel).Result;
            hotels = hotelService.GetAllHotelAsync().Result;

            int numberOfHotelsAfter = hotels.Count;
            Hotel h = hotelService.DeleteHotelAsync(newHotel.HotelNr).Result;

            //Assert
            Assert.AreEqual(numberOfHotelsBefore + 1, numberOfHotelsAfter);
            Assert.IsTrue(ok);
            Assert.AreEqual(h.HotelNr, newHotel.HotelNr);
        }
        [TestMethod]
        public void TestDeleteHotel()
        {
            //Arrange
            HotelService hotelService = new HotelService(connectionString);
            List<Hotel> hotels = hotelService.GetAllHotelAsync().Result;

            //Act
            int numberOfHotelsBefore = hotels.Count;
            Hotel newHotel = new Hotel(69, "TestTestHotel.com", "TestBoulevard");            
            hotels = hotelService.GetAllHotelAsync().Result;
            bool ok = hotelService.CreateHotelAsync(newHotel).Result;

            int numberOfHotelsAfter = hotels.Count;
            Hotel h = hotelService.DeleteHotelAsync(newHotel.HotelNr).Result;
            Hotel ok2 = hotelService.GetHotelFromIdAsync(newHotel.HotelNr).Result;

            //Assert
            Assert.IsTrue(ok);
            Assert.AreEqual(numberOfHotelsAfter, numberOfHotelsBefore);
            Assert.IsNull(ok2);
            Assert.AreEqual(h.HotelNr, newHotel.HotelNr);
            

        }
    }
}