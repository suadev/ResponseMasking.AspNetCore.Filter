using System.Collections.Generic;
using ResponseMasking.AspNetCore.Filter.Models;

namespace ResponseMasking.AspNetCore.Filter.Faker
{
    public class UserModelFaker
    {
        public static List<UserModel> GetUsers()
        {
            return new List<UserModel>
            {
                new UserModel (1,"suadev", "Suat Köse", 29, "12345678901"),
                new UserModel (2,"zeynep", "Zeynep Köse", 3, "23456789012"),
                new UserModel (3,"omer", "Ömer Akif Köse", 1, "34567890123")
            };
        }
    }
}