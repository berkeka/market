using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Entities
{
    class User
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public User()
        {
        }
        public User(int ID, string Username, string PasswordHash, string Name, string Surname)
        {
            // Fix me - Implement auto incremented id
            this.ID = ID;
            this.Username = Username;
            this.PasswordHash = PasswordHash;
            this.Name = Name;
            this.Surname = Surname;
        }
    }
}
