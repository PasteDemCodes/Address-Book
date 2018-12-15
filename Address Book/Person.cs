using System;

namespace Address_Book
{
    class Person    {

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; } = "-";
        public string Phonenumber { get; set; }
        public string Address { get; set; } = "-";

        // Example of Correct Birthday input "dd-MM-yyyy"
        public DateTime? Birthday { get; set; } = null;

        // Name, Surname and Phonenumber are mandatory in order to create a contact
        public Person(string name, string surname, string phonenumber)
        {
            Name = name;
            Surname = surname;
            Phonenumber = phonenumber;
        }

    }
}