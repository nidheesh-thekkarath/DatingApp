using System;

namespace DatingApp.API.Dtos
{
    public class UserForListDto
    {
        public int Id{get;set;}
        public string Username{get;set;}
        public byte[] PasswordHash{get;set;}
        public byte[] PasswordSalt{get;set;}
        public string Gender{get;set;}
        public int Age{get;set;}
        public string KnownAs{get;set;}
        public string Created{get;set;}
        public string LastActive{get;set;}
        
        public string City{get;set;}
        public string Country{get;set;}
        public string photoURL{get;set;}
    }
}