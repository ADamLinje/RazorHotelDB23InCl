using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using RazorHotelDB23InCl.Interfaces;
using RazorHotelDB23InCl.Models;

namespace RazorHotelDB23InCl.Services
{
    public class HotelService : Connection, IHotelService
    {
        private String queryString = "select * from Hotel";
        private String queryStringFromID = "select * from Hotel where Hotel_No = @ID";
        private String insertSql = "insert into Hotel Values (@ID, @Navn, @Adresse)";
        private String deleteSql = "delete from Hotel Where Hotel_No = @ID";
        private String updateSql = "update Hotel " +
                                   "set Hotel_No= @HotelID, Name=@Navn, Address=@Adresse " +
                                   "where Hotel_No = @ID";
        private string sqlHotelByName = "select * from Hotel where Name like @Navn";
        public HotelService(IConfiguration configuration) : base(configuration)
        {

        }

        public HotelService(string connectionString):base(connectionString)
        {

        }

        public async Task<bool> CreateHotelAsync(Hotel hotel)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(insertSql, connection))
                {
                    command.Parameters.AddWithValue("@ID", hotel.HotelNr);
                    command.Parameters.AddWithValue("@Navn", hotel.Navn);
                    command.Parameters.AddWithValue("@Adresse", hotel.Adresse);
                    try
                    {
                        command.Connection.Open();
                        int noOfRows = await command.ExecuteNonQueryAsync(); //bruges ved update, delete, insert
                        if (noOfRows == 1)
                        {
                            return true;
                        }

                        return false;
                    }
                    catch (SqlException sqlex)
                    {
                        Console.WriteLine("Database error");
                        throw sqlex;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Generel error");
                        throw ex;
                    }
                }

            }
            return false;
        }

        public async Task<Hotel> DeleteHotelAsync(int hotelnr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    Hotel hotel = await GetHotelFromIdAsync(hotelnr);
                    SqlCommand command = new SqlCommand(deleteSql, connection);
                    command.Parameters.AddWithValue("@ID", hotelnr);
                    connection.Open();
                    int noOfRows = await command.ExecuteNonQueryAsync();
                    return hotel;
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
            return null;
        }

        public async Task<List<Hotel>> GetAllHotelAsync()
        {
            List<Hotel> hoteller = new List<Hotel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    try
                    {
                        await command.Connection.OpenAsync();//aSynkront
                        Thread.Sleep(1000);
                        SqlDataReader reader = await command.ExecuteReaderAsync();//aSynkront
                        Thread.Sleep(1000);
                        while (await reader.ReadAsync())
                        {
                            int hotelNr = reader.GetInt32(0);
                            string hotelNavn = reader.GetString(1);
                            string hotelAdr = reader.GetString(2);
                            Hotel hotel = new Hotel(hotelNr, hotelNavn, hotelAdr);
                            hoteller.Add(hotel);
                        }
                    }
                    catch (SqlException sqlEx)
                    {
                        Console.WriteLine("Database error " + sqlEx.Message);
                        return null;
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine("Generel fejl" + exp.Message);
                        return null;
                    }
                }
            }
            return hoteller;
        }

        public async Task<Hotel> GetHotelFromIdAsync(int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand commmand = new SqlCommand(queryStringFromID, connection);
                    commmand.Parameters.AddWithValue("@ID", hotelNr);
                    await commmand.Connection.OpenAsync();

                    SqlDataReader reader = await commmand.ExecuteReaderAsync();
                    if (reader.Read())
                    {
                        int hotelNo = reader.GetInt32(0);
                        string hotelNavn = reader.GetString(1);
                        string hotelAdr = reader.GetString(2);
                        Hotel hotel = new Hotel(hotelNo, hotelNavn, hotelAdr);
                        return hotel;
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
                    
                }
            }
            return null;
        }

        public async Task<List<Hotel>> GetHotelsByNameAsync(string name)
        {
            List<Hotel> hoteller = new List<Hotel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(sqlHotelByName, connection);
                    string nameWild = "%" + name + "%";
                    command.Parameters.AddWithValue("@Navn", nameWild);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        int hotelnr = reader.GetInt32(0);
                        string hotelNavn = reader.GetString(1);
                        string hotelAdresse = reader.GetString(2);
                        Hotel h = new Hotel(hotelnr, hotelNavn, hotelAdresse);
                        hoteller.Add(h);
                    }
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("Der skete en database fejl! " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Der skete en generel fejl! " + ex.Message);
                }
                return hoteller;
            }
            return null;

        }


        public async Task<bool> UpdateHotelAsync(Hotel hotel, int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(updateSql, connection);
                    command.Parameters.AddWithValue("@HotelID", hotel.HotelNr);
                    command.Parameters.AddWithValue("@Navn", hotel.Navn);
                    command.Parameters.AddWithValue("@Adresse", hotel.Adresse);
                    command.Parameters.AddWithValue("@ID", hotelNr);
                    
                    command.Connection.Open();
                    int noOfRows = await command.ExecuteNonQueryAsync();
                    return noOfRows == 1;

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
                    
                }
            }
            return false;
        }
    }
}
