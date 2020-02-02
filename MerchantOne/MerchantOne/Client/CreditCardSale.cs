using MerchantOne.Exceptions;
using MerchantOne.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MerchantOne.Client
{
   public class CreditCardSale
   {
      public CreditCardSale(
         string securityKey, 
         decimal amount,
         BillingAddress billingAddress,
         string creditCardNumber, 
         int ccv,
         CreditCardExpirationDate expirationDate)
      {
         if (string.IsNullOrEmpty(securityKey))
         {
            throw new ArgumentException("Security Key is required to submit a CC request.", nameof(securityKey));
         }

         SecurityKey = securityKey;
         Amount = amount;
         BillingAddress = billingAddress ?? throw new ArgumentNullException(nameof(billingAddress));
         CreditCardNumber = creditCardNumber ?? throw new ArgumentNullException(nameof(creditCardNumber));
         CCV = ccv;
         ExpirationDate = expirationDate ?? throw new ArgumentNullException(nameof(expirationDate));
      }

      /// <summary>
      /// Security key found within Home > Gateway Options > Security Keys
      /// Use 6457Thfj624V5r7WUwc5v6a68Zsd6YEm for testing
      /// </summary>
      public string SecurityKey { get; set; }

      public decimal Amount { get; }

      public BillingAddress BillingAddress { get; }

      /// <summary>
      /// Full credit card number
      /// For testing Visa, use: 4111111111111111
      /// </summary>
      public string CreditCardNumber { get; set; }

      public CreditCardExpirationDate ExpirationDate { get; set; }

      private int _ccv { get; set; }
      public int CCV
      {
         get
         {
            return _ccv;
         }
         set
         {
            if (value < 100 || value > 999)
               throw new InvalidCCVException($"{value} is not a valid CCV. It needs to be a 3 digit integer");

            _ccv = value;
         }
      }

      public override string ToString()
      {
         return $"security_key={SecurityKey}" 
                   + $"&firstname={BillingAddress.FirstName}&lastname={BillingAddress.LastName}"
                   + $"&address1={BillingAddress.Address1}&city={BillingAddress.City}"
                   + $"&state={BillingAddress.State}&zip={BillingAddress.ZIP}"
                   + $"&payment=creditcard&type=sale"
                   + $"&amount={Amount}&ccnumber={CreditCardNumber}&ccexp={ExpirationDate}&cvv={CCV}";
      }
   }
}
