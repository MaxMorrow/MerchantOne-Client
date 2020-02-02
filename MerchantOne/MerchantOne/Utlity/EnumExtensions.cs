using System;
using System.ComponentModel;
using System.Reflection;

namespace MerchantOne.Utlity
{
   public static class EnumExtensions
   {
      public static string GetDescription(this Enum enumVal)
      {
         MemberInfo[] memInfo = enumVal.GetType().GetMember(enumVal.ToString());
         DescriptionAttribute attribute = CustomAttributeExtensions.GetCustomAttribute<DescriptionAttribute>(memInfo[0]);
         return attribute.Description;
      }
   }
}
