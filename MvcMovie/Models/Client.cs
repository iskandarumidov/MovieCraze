using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcMovie.Models
{
    public class Client
    {
        public Client()
        {

        }
        public Client(string firstName, string lastName, string address, string city, string state, string zip, string phone, string doB, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            City = city;
            State = state;
            Zip = zip;
            Phone = phone;
            DoB = doB;
            Email = email;
        }

        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string DoB { get; set; }
        public string Email { get; set; }

        /*
        public class ClientContext : DbContext
        {
            public ClientContext() : base("ClientContext")
            {

            }
            public DbSet<Client> Clients { get; set; }
        }*/
    }
}