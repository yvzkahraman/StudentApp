﻿using StudentApp.Data.Helpers;
using StudentApp.Data.Interfaces;
using StudentApp.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Data.Repositories
{
    public class AppUserRepository : IAppUserRepository
    {
        public int Create(AppUser appUser)
        {
            var connection = new DbConnectionHelper().Connection;

            var command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = System.Data.CommandType.Text;

            command.CommandText = "insert into Users OUTPUT INSERTED.Id values(@name,@surname,@genderId,@phoneNumber,@username,@password)";
            command.Parameters.AddWithValue("@name", appUser.Name);
            command.Parameters.AddWithValue("@surname", appUser.Surname);
            command.Parameters.AddWithValue("@genderId", appUser.GenderId);
            command.Parameters.AddWithValue("@phoneNumber", appUser.PhoneNumber);
            command.Parameters.AddWithValue("@username", appUser.Username);
            command.Parameters.AddWithValue("@password", appUser.Password);

            connection.Open();
            var returnValue = command.ExecuteScalar();
            int rv = Convert.ToInt32(returnValue);
            command.Parameters.Clear();
            connection.Close();

            return rv;  
        }
    }
}
