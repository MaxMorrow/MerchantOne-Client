using MerchantOne.Client;
using MerchantOne.Models;
using MerchantOne.Utlity;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace MerchantOne.Tests
{
   [ExcludeFromCodeCoverage]
   public class MerchantOneIntegrationTest
   {
      [Fact]
      public void MerchantOneTestShouldReturnSuccess()
      {
         string security_key = "6457Thfj624V5r7WUwc5v6a68Zsd6YEm";
         string firstname = "John";
         string lastname = "Smith";
         string address1 = "1234 Main St.";
         string city = "Chicago";
         string state = "IL";
         string zip = "60193";

         var creditCardSale =
            new CreditCardSale(
               security_key,
               1.00m,
               new BillingAddress(firstname, lastname, address1, city, state, zip),
               "4111111111111111",
               123,
               new CreditCardExpirationDate(ExpirationMonth.October, 2023));

         var merchantOneClient = new MerchantOneClient();
         var result = merchantOneClient.ProcessCreditCardSale(creditCardSale);

         Assert.Contains("SUCCESS", result.ResponseText);
      }
   }
}
