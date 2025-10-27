
namespace HappenCodeECommerceAPI.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int Age { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public int? CartId { get; set; }
        public Cart? Cart { get; set; }

    }

}

// Some notes for myself...

// Compiler warning silenced by using null! (null forgiving trick)
// Null forgiving trick essentially is : "I'm aware this is non-nullable and I promise to set it before using it."
// Could also use a default value of "" in declaration or in the constructor.
// EFCore interprets non-nullable reference types (i.e string) as NOT NULL in the DB
// using the ? operator would mean the property can be null, which isn't something we want
// Id is special in that EFCore treats it as the primary key, therefore it can not be null by default
// When using a property name that isn't Id or doesn't follow the <class_name>Id convention, the [Key] attribute needs to be used
// To ensure EFCore recognizes the property as the Primary Key
// Value types (such as int, decimal etc) can not be null by default.


