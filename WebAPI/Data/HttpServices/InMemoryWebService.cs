﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Persistence;

namespace WebAPI.Data.HttpServices
{
    public class InMemoryWebService : IUserService
    {
        private IList<User> users;

        public InMemoryWebService()
        {
            users = new[]
            {
                new User()
                {
                    Username = "admin",
                    Id = 2,
                    Password = "admin123",
                    SecurityLevel = 4,
                    Role = "Admin",
                },
                new User()
                {
                    Username = "user123",
                    Id = 3,
                    Password = "12345",
                    SecurityLevel = 2,
                    Role = "Member",
                }
            }.ToList();
            using (WebDbContext _webDbContext = new WebDbContext())
            {
                _webDbContext.Users.AddRange(users);
            }
        }

        public async Task<User> ValidateUserAsync(string username, string password)
        {
            User first = users.FirstOrDefault(user => user.Username.Equals(username));
            if (first == null)
            {
                throw new Exception("User not found");
            }

            if (!first.Password.Equals(password))
            {
                throw new Exception("Incorrect password");
            }

            return first;
        }
    }
}