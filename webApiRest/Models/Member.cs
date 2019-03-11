using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webApiRest.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public Member (int id,string fullname,int age)
        {
            Id = id;
            FullName = fullname;
            Age = age;
        }
    }
}