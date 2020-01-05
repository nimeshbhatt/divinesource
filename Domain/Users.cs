using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Users : ModelBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyWebSite { get; set; }
        public string Password { get; set; }

        // 0 = Admin 
        // 1 = Clients
        public string Role { get; set; }
        public string IsDelete { get; set; }
    }
}
