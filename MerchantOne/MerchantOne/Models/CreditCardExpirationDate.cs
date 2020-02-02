using MerchantOne.Exceptions;
using MerchantOne.Utlity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MerchantOne.Models
{
   public class CreditCardExpirationDate
   {
      public CreditCardExpirationDate(ExpirationMonth month, int year)
      {
         Month = month;
         Year = year;
      }

      public const int MaxExpirationYears = 4;

      public ExpirationMonth Month { get; set; }

      private int _expirationYear { get; set; }
      public int Year
      {
         get
         {
            return _expirationYear;
         }
         set
         {
            if (value < DateTime.Now.Year || value > DateTime.Now.Year + MaxExpirationYears)
               throw new InvalidExpirationYearException($"{value} is not a valid expiration year. It must be greater than the current year, and not greater than {MaxExpirationYears} years in the future.");

            _expirationYear = value;
         }
      }

      public override string ToString()
      {
         var yearString = Year.ToString().Substring(2, 2);
         return $"{Month.GetDescription()}{yearString}";
      }
   }
}
