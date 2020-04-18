using System;
using System.Collections.Generic;
using System.Text;
using ShopOnline.Model;
using Npgsql;

namespace ShopOnline.DataAccess
{
    public class ContactDaoDB
    {
        DataBaseConnectionService localConnection = new DataBaseConnectionService("localhost" , "adminshop" , "adminshop" , "shop_online_project");

        public void SendToDB( TheForm form )
        {
            using var connectionObj = localConnection.GetDatabaseConnectionObject();
            string sql = @"INSERT INTO client_forms(data_time, first_name, last_name, email, request_type, request_text)" +
                "VALUES(@dateAndTime, @firstName, @lastName, @email, @requestType, @requestText);";
            connectionObj.Open();

            using var cmd = new NpgsqlCommand(sql , connectionObj);
            cmd.Parameters.AddWithValue("@dateAndTime" , DateTime.Now);
            cmd.Parameters.AddWithValue("@firstName" , form.FirstName);
            cmd.Parameters.AddWithValue("@lastName" , form.LastName);
            cmd.Parameters.AddWithValue("@email" , form.Email);
            cmd.Parameters.AddWithValue("@requestType" , form.RequestType);
            cmd.Parameters.AddWithValue("@requestText" , form.RequestText);

            cmd.Prepare();
            cmd.ExecuteNonQuery();
            connectionObj.Close();
            Console.WriteLine("Thank you for your message");
        }
    }
}
