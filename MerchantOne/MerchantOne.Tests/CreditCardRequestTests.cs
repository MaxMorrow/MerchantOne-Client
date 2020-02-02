using MerchantOne.Client;
using MerchantOne.Exceptions;
using MerchantOne.Models;
using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace MerchantOne.Tests
{
   [ExcludeFromCodeCoverage]
   public class creditCardSaleTests
   {
      public CreditCardSale MockBlankcreditCardSale()
      {
         return new CreditCardSale(
            "security-key",
            1.00m,
            new BillingAddress("FirstName", "LastName", "Address1", "City", "State", "ZIP"),
            "credit-card-number",
             123,
            new CreditCardExpirationDate(ExpirationMonth.April, DateTime.Now.Year));
      }

      [Fact]
      public void creditCardSaleShouldInstantiate()
      {
         var ccRequest = MockBlankcreditCardSale();
         Assert.Equal("security-key", ccRequest.SecurityKey);
         Assert.Equal("FirstName", ccRequest.BillingAddress.FirstName);
         Assert.Equal("LastName", ccRequest.BillingAddress.LastName);
         Assert.Equal("Address1", ccRequest.BillingAddress.Address1);
         Assert.Equal("City", ccRequest.BillingAddress.City);
         Assert.Equal("State", ccRequest.BillingAddress.State);
         Assert.Equal("ZIP", ccRequest.BillingAddress.ZIP);

         Assert.Equal(1.00m, ccRequest.Amount);

         Assert.Equal("credit-card-number", ccRequest.CreditCardNumber);
         Assert.Equal(123, ccRequest.CCV);
         Assert.Equal(ExpirationMonth.April, ccRequest.ExpirationDate.Month);
         Assert.Equal(DateTime.Now.Year, ccRequest.ExpirationDate.Year);
      }

      [Fact]
      public void creditCardSaleShouldGetRequestOnToString()
      {
         var ccRequest = MockBlankcreditCardSale();
         var request = $"security_key={ccRequest.SecurityKey}"
                   + $"&firstname={ccRequest.BillingAddress.FirstName}&lastname={ccRequest.BillingAddress.LastName}"
                   + $"&address1={ccRequest.BillingAddress.Address1}&city={ccRequest.BillingAddress.City}"
                   + $"&state={ccRequest.BillingAddress.State}&zip={ccRequest.BillingAddress.ZIP}"
                   + $"&payment=creditcard&type=sale"
                   + $"&amount={ccRequest.Amount}&ccnumber={ccRequest.CreditCardNumber}"
                   + $"&ccexp={ccRequest.ExpirationDate}&cvv={ccRequest.CCV}";

         Assert.Equal(request, ccRequest.ToString());
      }

      [Fact]
      public void paymentResultShouldParseAllValues()
      {
         var mockResponse = "response=1&responsetext=SUCCESS&authcode=123456&transactionid=5158550654&avsresponse=N&cvvresponse=N&orderid=&type=sale&response_code=100;";
         var mockMerchaneOneResult = new MerchantOnePaymentResult(mockResponse);

         Assert.Equal("1", mockMerchaneOneResult.Response);
         Assert.Equal("SUCCESS", mockMerchaneOneResult.ResponseText);
         Assert.True(mockMerchaneOneResult.ProcessedSuccessfully);
         Assert.Equal("123456", mockMerchaneOneResult.AuthorizationCode);
         Assert.Equal("5158550654", mockMerchaneOneResult.TransactionId);
         Assert.Equal("N", mockMerchaneOneResult.AvsResponse);
         Assert.Equal("N", mockMerchaneOneResult.CvvResponse);
         Assert.Equal("", mockMerchaneOneResult.OrderId);
         Assert.Equal("sale", mockMerchaneOneResult.Type);
         Assert.Equal("100", mockMerchaneOneResult.ResponseCode);
      }

      [Fact]
      public void paymentResultShouldSkipMissingValues()
      {
         var mockResponse = "responsetext=SUCCESS&authcode=123456&transactionid=5158550654&avsresponse=N&cvvresponse=N&orderid=&type=sale&response_code=100;";
         var mockMerchaneOneResult = new MerchantOnePaymentResult(mockResponse);

         Assert.Equal("", mockMerchaneOneResult.Response);
      }

      [Fact]
      public void paymentResultShouldParseFailure()
      {
         var mockResponse = "responsetext=FAILURE&authcode=123456&transactionid=5158550654&avsresponse=N&cvvresponse=N&orderid=&type=sale&response_code=100;";
         var mockMerchaneOneResult = new MerchantOnePaymentResult(mockResponse);

         Assert.False(mockMerchaneOneResult.ProcessedSuccessfully);
      }


      [Fact]
      public void creditCardSaleShouldThrowExceptionIfSecurityKeyIsNull()
      {
         Assert.Throws<ArgumentException>(() =>
         {
            var ccRequest = new CreditCardSale(null, 1.00m,
               new BillingAddress("", "", "", "", "", ""), "1234567890", 123, new CreditCardExpirationDate(ExpirationMonth.April, DateTime.Now.Year));
         });
      }

      [Fact]
      public void creditCardSaleShouldThrowExceptionIfBillingAddressIsNull()
      {
         Assert.Throws<ArgumentNullException>(() =>
         {
            var ccRequest = new CreditCardSale("1234567890", 1.00m, null, "1234567890", 123, new CreditCardExpirationDate(ExpirationMonth.April, DateTime.Now.Year));
         });
      }

      [Fact]
      public void creditCardSaleShouldThrowExceptionIfCreditCardNumberIsNull()
      {
         Assert.Throws<ArgumentNullException>(() =>
         {
            var ccRequest = new CreditCardSale("1234567890", 1.00m,
               new BillingAddress("", "", "", "", "", ""), null, 123, new CreditCardExpirationDate(ExpirationMonth.April, DateTime.Now.Year));
         });
      }

      [Fact]
      public void creditCardSaleShouldThrowExceptionIfExpirationDateIsNull()
      {
         Assert.Throws<ArgumentException>(() =>
         {
            var ccRequest = new CreditCardSale(null, 1.00m,
               new BillingAddress("", "", "", "", "", ""), "1234567890", 123, null);
         });
      }

      [Fact]
      public void CreditCardCCVShouldThrowExceptionIfLessThan100()
      {
         var ccRequest = MockBlankcreditCardSale();
         Assert.Throws<InvalidCCVException>(() =>
         {
            ccRequest.CCV = 99;
         });
      }

      [Fact]
      public void CreditCardCCVShouldThrowExceptionIfGreaterThan999()
      {
         var ccRequest = MockBlankcreditCardSale();

         Assert.Throws<InvalidCCVException>(() =>
         {
            ccRequest.CCV = 1000;
         });
      }

      [Fact]
      public void CreditCardExpirationDateShouldFormatString()
      {
         var expirationDate = new CreditCardExpirationDate(ExpirationMonth.January, 2020);

         Assert.Equal("0120", expirationDate.ToString());
      }

      [Fact]
      public void CreditCardYearShouldThrowExceptionIfGreaterThan4YearsInTheFuture()
      {
         Assert.Throws<InvalidExpirationYearException>(() =>
         {
            var tenYearsFromNow = DateTime.Now.Year + 10;
            var ccExpirationDate = new CreditCardExpirationDate(ExpirationMonth.April, tenYearsFromNow);
         });
      }

      [Fact]
      public void CreditCardYearShouldThrowExceptionIfInThePast()
      {
         Assert.Throws<InvalidExpirationYearException>(() =>
         {
            var tenYearsAgo = DateTime.Now.Year - 10;
            var ccExpirationDate = new CreditCardExpirationDate(ExpirationMonth.April, tenYearsAgo);
         });
      }
   }
}
