using MerchantOne.Models;
using MerchantOne.Utlity;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace MerchantOne.Tests
{
   [ExcludeFromCodeCoverage]
   public class ExpirationMonthTests
   {
      [Fact]
      public void ExpirationMonthShouldReturn01ForJanuary()
      {
         var expirationMonth = ExpirationMonth.January;
         Assert.Equal("01", expirationMonth.GetDescription());
      }

      [Fact]
      public void ExpirationMonthShouldReturn02ForFebruary()
      {
         var expirationMonth = ExpirationMonth.February;
         Assert.Equal("02", expirationMonth.GetDescription());
      }

      [Fact]
      public void ExpirationMonthShouldReturn03ForMarch()
      {
         var expirationMonth = ExpirationMonth.March;
         Assert.Equal("03", expirationMonth.GetDescription());
      }

      [Fact]
      public void ExpirationMonthShouldReturn04ForApril()
      {
         var expirationMonth = ExpirationMonth.April;
         Assert.Equal("04", expirationMonth.GetDescription());
      }

      [Fact]
      public void ExpirationMonthShouldReturn05ForMay()
      {
         var expirationMonth = ExpirationMonth.May;
         Assert.Equal("05", expirationMonth.GetDescription());
      }

      [Fact]
      public void ExpirationMonthShouldReturn06ForJune()
      {
         var expirationMonth = ExpirationMonth.June;
         Assert.Equal("06", expirationMonth.GetDescription());
      }

      [Fact]
      public void ExpirationMonthShouldReturn07ForJuly()
      {
         var expirationMonth = ExpirationMonth.July;
         Assert.Equal("07", expirationMonth.GetDescription());
      }

      [Fact]
      public void ExpirationMonthShouldReturn08ForAugust()
      {
         var expirationMonth = ExpirationMonth.August;
         Assert.Equal("08", expirationMonth.GetDescription());
      }

      [Fact]
      public void ExpirationMonthShouldReturn09ForSeptember()
      {
         var expirationMonth = ExpirationMonth.September;
         Assert.Equal("09", expirationMonth.GetDescription());
      }

      [Fact]
      public void ExpirationMonthShouldReturn10ForOctober()
      {
         var expirationMonth = ExpirationMonth.October;
         Assert.Equal("10", expirationMonth.GetDescription());
      }

      [Fact]
      public void ExpirationMonthShouldReturn11ForNovember()
      {
         var expirationMonth = ExpirationMonth.November;
         Assert.Equal("11", expirationMonth.GetDescription());
      }

      [Fact]
      public void ExpirationMonthShouldReturn12ForDecember()
      {
         var expirationMonth = ExpirationMonth.December;
         Assert.Equal("12", expirationMonth.GetDescription());
      }
   }
}
