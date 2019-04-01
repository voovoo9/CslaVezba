using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class PersonDal
    {
        //stores data in inmemory static collection
        //todo add database and data context
        private static List<PersonDto> _list = new List<PersonDto>();

        public PersonDto Create()
        {
            return new PersonDto { Id = -1 };
        }

        public PersonDto GetPerson(int id)
        {
            var entity = _list.FirstOrDefault(_ => _.Id == id);
            if (entity == null)
                throw new Exception("Index not found");
            return entity;
        }

        public int InsertPerson(PersonDto data)
        {
            var newId = 1;
            if (_list.Count > 0)
                newId = _list.Max(_ => _.Id) + 1;
            data.Id = newId;
            _list.Add(data);
            return newId;
        }

        public void UpdatePerson(PersonDto data)
        {
            var entity = GetPerson(data.Id);
            entity.FirstName = data.FirstName;
            entity.LastName = data.LastName;
        }

        public void DeletePeson(int id)
        {
            var entity = GetPerson(id);
            _list.Remove(entity);
        }
    }
}
