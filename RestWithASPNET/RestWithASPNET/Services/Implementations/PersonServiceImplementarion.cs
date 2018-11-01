using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RestWithASPNET.Model;

namespace RestWithASPNET.Services.Implementations
{
    public class PersonServiceImplementarion : IPersonServices
    {
        private volatile int count;

        public Person Create(Person person)
        {
            return person;
        }

        public void Delete(long id)
        {
            
        }

        public List<Person> FindAll()
        {
            List<Person> listPerson = new List<Person>();
            for(var i = 0; i < 10; i++)
            {
                listPerson.Add(MockPerson(i));
            }           
            return listPerson;
        }

        public Person FindById(long id)
        {
            return new Person{
                id = IncrementAndGet(),
                FirstName = "Daniel",
                LastName = "Carvalho",
                Address = "Ribeirão Preto - São Paulo",
                Gender = "Male"
            };
        }

        public Person Update(Person person)
        {
            return person;
        }

        public Person MockPerson(int id)
        {
            return new Person
            {
                id = IncrementAndGet(),
                FirstName = "First " + id,
                LastName = "Last " + id,
                Address = "Address " + id,
                Gender = "Male"
            };
        }

        private long IncrementAndGet()
        {
            return Interlocked.Increment(ref count);
        }
    }
}
