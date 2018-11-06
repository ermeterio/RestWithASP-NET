using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RestWithASPNET.Model;
using RestWithASPNET.Model.Context;
using RestWithASPNET.Services.Repository;

namespace RestWithASPNET.Services.Business
{
    public class PersonBusinessImpl : IPersonBusiness
    {
        private IPersonRepository repository;
        private volatile int count;

        public PersonBusinessImpl(IPersonRepository repository)
        {
            this.repository = repository;
        }
        
        public Person Create(Person person)
        {
            return repository.Create(person);
        }

        public void Delete(long id)
        {
            repository.Delete(id);            
        }

        public List<Person> FindAll()
        {
            return repository.FindAll();
        }       

        public Person FindById(long id)
        {
            return repository.FindById(id);            
        }

        public Person Update(Person person)
        {
            return repository.Update(person);           
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
