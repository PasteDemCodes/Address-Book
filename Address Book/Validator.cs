using System;
using System.Text.RegularExpressions;

namespace Address_Book
{
    class Validator
    {
        private String Name;
        private String Surname;
        private String Email;
        private String Phonenumber;
        private String Address;

        public Validator(String name, String surname, String email, String phonenumber, String address)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Phonenumber = phonenumber;
            Address = address;
        }

        public Boolean IsEmailValid()
        {
            Regex rx = new Regex(@"^[-!#$%&'*+/0-9=?A-Z^_a-z{|}~](\.?[-!#$%&'*+/0-9=?A-Z^_a-z{|}~])*@[a-zA-Z](-?[a-zA-Z0-9])*(\.[a-zA-Z](-?[a-zA-Z0-9])*)+$");
            return rx.IsMatch(Email);

        }

        public Boolean IsPhonenumberValid()
        {
            if (Phonenumber.Length == 10)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean IsNameValid()
        {
            if (String.IsNullOrEmpty(Name))
            {
                return false;
            }

            else
            {
                return true;
            }
        }

        public Boolean IsSurnameValid()
        {
            if (String.IsNullOrEmpty(Surname))
            {
                return false;
            }

            else
            {
                return true;
            }
        }

        // Checks where everything is valid.
        public Boolean IsValid()
        {
            if (IsPhonenumberValid() && IsEmailValid() && IsNameValid() && IsSurnameValid())
            {
                return true;
            }
            else
            {
                return false;
            }

        }


    }
}
