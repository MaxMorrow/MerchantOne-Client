using System;
using System.Collections.Generic;
using System.Text;

namespace MerchantOne.Exceptions
{
   public class InvalidExpirationYearException : Exception
   {
      public InvalidExpirationYearException(string message) : base(message)
      {
      }
   }
}
