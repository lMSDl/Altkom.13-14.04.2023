using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Services.Bogus.Fakers
{
    public class UserFaker : EntityFaker<User>
    {
        public UserFaker()
        {
            RuleFor(x => x.Name, x => x.Person.FullName);
            RuleFor(x => x.Login, x => x.Internet.UserName());
            RuleFor(x => x.Password, x => x.Internet.Password(10));
            RuleFor(x => x.BithDate, x => x.Person.DateOfBirth);
        }
    }
}
