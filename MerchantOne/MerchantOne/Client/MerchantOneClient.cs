using System.IO;
using System.Net;

namespace MerchantOne.Client
{
   public class MerchantOneClient
   {

      public const string MerchantOnePostUrl = "https://secure.networkmerchants.com/api/transact.php";

      public MerchantOneClient()
      {

      }

      public MerchantOnePaymentResult ProcessCreditCardSale(CreditCardSale creditCardSale)
      {
         var postContent = creditCardSale.ToString();
         var postResult = string.Empty;

         HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(MerchantOnePostUrl);
         httpWebRequest.Method = "POST";
         httpWebRequest.ContentLength = postContent.Length;
         httpWebRequest.ContentType = "application/x-www-form-urlencoded";

         var httpRequestWriter = new StreamWriter(httpWebRequest.GetRequestStream());
         httpRequestWriter.Write(postContent);
         httpRequestWriter.Close();

         HttpWebResponse merchantOneResponse = (HttpWebResponse)httpWebRequest.GetResponse();
         using (StreamReader sr =
            new StreamReader(merchantOneResponse.GetResponseStream()))
         {
            postResult = sr.ReadToEnd();
            sr.Close();
         }

         var merchantOneResult = new MerchantOnePaymentResult(postResult);

         return merchantOneResult;
      }
   }
}
