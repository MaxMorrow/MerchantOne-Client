using System;
using System.Collections.Generic;
using System.Text;

namespace MerchantOne.Models
{
   public class BillingAddress
   {
      public BillingAddress(
         string firstName, string lastName,
         string address1, string city, string state,
         string zip)
      {
         FirstName = firstName;
         LastName = lastName;
         Address1 = address1;
         City = city;
         State = state;
         ZIP = zip;
      }

      public string FirstName { get; set; }

      public string LastName { get; set; }

      public string Address1 { get; set; }

      public string City { get; }

      public string State { get; set; }

      public string ZIP { get; set; }
   }
}
