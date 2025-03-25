using System;

namespace CSharpLearning.Examples.CommonPitfalls
{
    // Simple customer class to demonstrate reference types
    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";

        public Customer(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        // Create a copy of the customer
        public Customer Clone()
        {
            return new Customer(FirstName, LastName);
        }
    }
}