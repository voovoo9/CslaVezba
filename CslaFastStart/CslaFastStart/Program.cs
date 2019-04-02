using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CslaFastStart
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Creating a new person");
            var person = Csla.DataPortal.Create<PersonEdit>();
            Console.WriteLine("Enter first name: ");
            person.FirstName = Console.ReadLine();
            Console.WriteLine("Enter last name: ");
            person.LastName = Console.ReadLine();
            if(person.IsSavable)
            {
                person = person.Save();
                Console.WriteLine($"Added person with id {person.Id}. First name = '{person.FirstName}', last name = '{person.LastName}'.");
            }
            else
            {
                Console.WriteLine("Invalid entry");
                foreach (var item in person.BrokenRulesCollection)
                    Console.WriteLine(item.Description);
                Console.ReadKey();
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Updating existing person");
            person = Csla.DataPortal.Fetch<PersonEdit>(person.Id);
            Console.Write($"Update first name [{person.FirstName}]: ");
            var temp = Console.ReadLine();
            if(!string.IsNullOrWhiteSpace(temp))
            {
                person.FirstName = temp;
            }
            Console.WriteLine($"Update last name [{person.LastName}]: ");
            temp = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(temp))
            {
                person.LastName = temp;
            }
            if(person.IsSavable)
            {
                person = person.Save();
                Console.WriteLine($"Updated person with id {person.Id}. First name = '{person.FirstName}', last name = '{person.LastName}'.");
            }
            else
            {
                if(person.IsDirty)
                {
                    Console.WriteLine("Invalid entry");
                    foreach(var item in person.BrokenRulesCollection)
                        Console.WriteLine(item.Description);
                    Console.ReadKey();
                    return;
                }
                else
                {
                    Console.WriteLine("No changes, nothing to save.");
                }
            }

            Console.WriteLine();
            Console.WriteLine("Deleting existing person");
            Csla.DataPortal.Fetch<PersonEdit>(person.Id);
            try
            {
                person = Csla.DataPortal.Fetch<PersonEdit>(person.Id);
                Console.WriteLine("Person NOT deleted");
            }
            catch
            {
                Console.WriteLine("Person successfully deleted");
            }

            Console.ReadKey();
        }
    }
}
