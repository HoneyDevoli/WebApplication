using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoNameApp.WEB.Helpers
{
    public static class ListHelpers
    {
        public static MvcHtmlString PluralizeRus(int number, string singular, string several, string plural)
    {
            int abs = Math.Abs(number);
            int lastDigit = abs % 10;
            int beforeLastDigit = abs % 100 - lastDigit;
            if (lastDigit == 1 && beforeLastDigit != 10)
            {
                return new MvcHtmlString($"{number} {singular}");
            }
            else if (lastDigit >= 2 && lastDigit <= 4 && beforeLastDigit != 10)
            {
                return new MvcHtmlString($"{number} {several}");
            }
            else
            {
                return new MvcHtmlString($"{number} {plural}");
            }
            }
    }
}