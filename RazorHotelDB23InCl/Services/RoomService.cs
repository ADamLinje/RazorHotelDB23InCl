using Microsoft.Data.SqlClient;
using RazorHotelDB23InCl.Interfaces;
using RazorHotelDB23InCl.Models;

namespace RazorHotelDB23InCl.Services
{
    public class RoomService : Connection, IRoomService
    {
        private string queryString = "select * from Room Where Hotel_No = @HotelID";
        private string queryStringFromID = "select * from Room Where Room_No = @ID and Hotel_No = @HotelID";
        private string insertSql = "insert into Room Values(@ID, @HotelId, @Type, @Pris)";
        private string deleteSql = "delete from Room Where Room_No = @ID and Hotel_No = HotelID";
        private string updateSql = "update Room Set Room_No = @ID, Name = @Navn,Address= @Adresse where Room_No = @ID";

        public RoomService(IConfiguration configuration) : base (configuration)
        {

        }
        public async Task<bool> CreateRoomAsync(int hotelNr, Room room)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(insertSql, connection))
                {
                    command.Parameters.AddWithValue("@ID", room.RoomNr);
                    command.Parameters.AddWithValue("@Type", room.Types);
                    command.Parameters.AddWithValue("@Pris", room.Pris);
                    command.Parameters.AddWithValue("@HotelID", room.HotelNr);
                    try
                    {                                              
                        command.Connection.Open();
                        int noOfRows = await command.ExecuteNonQueryAsync();
                        if (noOfRows == 1)
                        {
                            return true;
                        }
                        return false;
                    }
                    catch (SqlException sqlEx)
                    {
                        Console.WriteLine("Database error " + sqlEx.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Generel fejl " + ex.Message);
                    }
                }
            }
            return false;
        }       

        public async Task<List<Room>> GetAllRoomAsync(int hotelNr)
        {
            List<Room> værelser = new List<Room>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@HotelID", hotelNr);
                        await command.Connection.OpenAsync();
                                                
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        
                        while (await reader.ReadAsync())
                        {
                            int roomNr = reader.GetInt32(0);
                            char type = reader.GetString(2)[0];
                            double pris = reader.GetDouble(3);
                            int hotelnr = reader.GetInt32(1);
                            Room room = new Room(roomNr, type, pris, hotelnr);
                            værelser.Add(room);
                        }
                    }
                    catch (SqlException sqlEx)
                    {
                        Console.WriteLine("Database error " + sqlEx.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Generel fejl " + ex.Message);
                    }
                    finally
                    {
                        //her kommer man altid
                    }
                }
            }
            return værelser;
        }
        public async Task<Room> GetRoomFromIdAsync(int roomNr, int hotelNr)
        {
            throw new NotImplementedException();
        }

        public Task<Room> DeleteRoomAsync(int roomNr, int hotelNr)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateRoomAsync(Room room, int roomNr, int hotelNr)
        {
            throw new NotImplementedException();
        }
    }
}
