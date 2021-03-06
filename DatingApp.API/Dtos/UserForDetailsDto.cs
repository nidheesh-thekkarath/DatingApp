using System;
using System.Collections.Generic;
using DatingApp.API.Models;

namespace DatingApp.API.Dtos
{
    public class UserForDetailsDto
    {
        public int Id{get;set;}
        public string Username{get;set;}
        public string Gender{get;set;}
        public int Age{get;set;}
        public string KnownAs{get;set;}
        public string Created{get;set;}
        public string LastActive{get;set;}
        public string Introduction{get;set;}
        public string LookingFor{get;set;}
        public string Intrests{get;set;}
        public string City{get;set;}
        public string Country{get;set;}
        public string PhotoUrl{get;set;}
        public ICollection<PhotoForDetailedDto> Photos{get;set;}
    }
}