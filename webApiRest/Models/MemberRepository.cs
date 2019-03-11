using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webApiRest.Models
{
    public class MemberRepository
    {
        private static Lazy<List<Member>> container = new Lazy<List<Member>>(() => Initialize(), true);

        private static List<Member> Initialize()
        {
            return new List<Member>
            {
                new Member(1,"Ali Veli",25),
                new Member(2,"Hale Jale",23),
                new Member(3,"Ahmet Mehmet",28)
            };
        }
        public static Member Add(string fullname,int age)
        {
            int lastId = container.Value.Max(m => m.Id);
            Member newMember = new Member(lastId + 1, fullname, age);
            container.Value.Add(newMember);
            return newMember;
        }

        public static List<Member> Get()
        {
            return container.Value;
        }

        public static Member Get(int id)
        {
            return container.Value.SingleOrDefault(m=>m.Id == id);
        }

        public static void Remove(int id)
        {
            container.Value.Remove(Get(id));
        }

        public static void Update(Member member)
        {
            Remove(member.Id);
            container.Value.Add(member);
        }

        public static bool IsExists(int id)
        {
           return container.Value.Any(m => m.Id == id);
        }

        public static bool IsExists(string fullname)
        {
            return container.Value.Any(m => m.FullName == fullname);
        }

    }
}