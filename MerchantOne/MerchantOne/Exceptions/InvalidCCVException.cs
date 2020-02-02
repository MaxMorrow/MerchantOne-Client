using System;
using System.Collections.Generic;
using System.Text;

namespace MerchantOne.Exceptions
{
   public class InvalidCCVException : Exception
   {
      public InvalidCCVException(string message) : base(message)
      {
      }
   }
}
