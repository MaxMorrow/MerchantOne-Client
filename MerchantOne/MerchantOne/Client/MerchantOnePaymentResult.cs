using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MerchantOne.Client
{
   public class MerchantOnePaymentResult
   {
      private const string MerchantOneSuccessCode = "1";

      /// <summary>
      /// Processes the raw result from the merchant one web result
      /// Example: response=1&responsetext=SUCCESS&authcode=123456&transactionid=5158550654&avsresponse=N&cvvresponse=N&orderid=&type=sale&response_code=100
      /// </summary>
      /// <param name="result"></param>
      public MerchantOnePaymentResult(string result)
      {
         RawResult = $"&{result.TrimEnd(';')}";
         ResultParts = RawResult.Split("&").ToList().Where(v => v != string.Empty).ToList();
         // 1, 2, 3 are the only valid responses according to the documentation
         Response = GetValue(ResultParts.FirstOrDefault(v => v.Contains("response=1") || v.Contains("response=2") || v.Contains("response=3")));
         ResponseText = GetValue(ResultParts.FirstOrDefault(v => v.Contains("responsetext=")));
         AuthorizationCode = GetValue(ResultParts.FirstOrDefault(v => v.Contains("authcode=")));
         TransactionId = GetValue(ResultParts.FirstOrDefault(v => v.Contains("transactionid=")));
         AvsResponse = GetValue(ResultParts.FirstOrDefault(v => v.Contains("avsresponse=")));
         CvvResponse = GetValue(ResultParts.FirstOrDefault(v => v.Contains("cvvresponse=")));
         OrderId = GetValue(ResultParts.FirstOrDefault(v => v.Contains("orderid=")));
         Type = GetValue(ResultParts.FirstOrDefault(v => v.Contains("type=")));
         ResponseCode = GetValue(ResultParts.FirstOrDefault(v => v.Contains("response_code=")));
      }

      private string GetValue(string keyValuePair)
      {
         var pairParts = keyValuePair?.Split("=");
         if (!string.IsNullOrEmpty(keyValuePair) && pairParts.Length > 0)
            return pairParts[1];
         else
            return string.Empty;
      }

      public string RawResult { get; }

      public List<string> ResultParts { get; }

      public bool ProcessedSuccessfully
      {
         get
         {
            return Response == MerchantOneSuccessCode;
         }
      }

      public string Response { get; }

      public string ResponseText { get; }

      public string AuthorizationCode { get; }

      public string TransactionId { get; }

      public string AvsResponse { get; }

      public string CvvResponse { get; }

      public string OrderId { get; }

      public string Type { get; }

      public string ResponseCode { get; }
   }
}
