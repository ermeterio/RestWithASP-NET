using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RestWithASPNET.Model;
using RestWithASPNET.Model.Context;

namespace RestWithASPNET.Services.Implementations
{
    public class PersonServiceImpl : IPersonServices
    {
        private MySqlContext context;
        public PersonServiceImpl(MySqlContext context)
        {
            this.context = context;
        }

        private volatile int count;

        public Person Create(Person person)
        {
            try {
                context.Add(person);
                context.SaveChanges();
            }
            catch (Exception ex){
                throw ex;
            }
            return person;
        }

        public void Delete(long id)
        {
            var result = context.Persons.SingleOrDefault(p => p.id.Equals(id));
            try {
                if (result != null) { 
                    context.Persons.Remove(result);
                    context.SaveChanges();
                }
                else
                    new Exception();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Person> FindAll()
        {
            return context.Persons.ToList();
        }

        public Person FindById(long id)
        {
            return context.Persons.SingleOrDefault(p => p.id.Equals(id));
        }

        public Person Update(Person person)
        {
            if (!Exists(person)) return new Person();

            var result = context.Persons.SingleOrDefault(p => p.id.Equals(person.id));
            try
            {
                context.Entry(result).CurrentValues.SetValues(person);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return person;
        }

        private bool Exists(Person person)
        {
            return context.Persons.Any(p => p.id.Equals(person.id));
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
