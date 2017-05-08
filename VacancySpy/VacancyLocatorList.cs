using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using System;
using System.Collections.Generic;
using System.Linq;

namespace VacancySpy
{
    public class VacancyLocatorList
    {
        string html;
        string selector;
        IHtmlDocument document;

        public VacancyLocatorList(string html, string selector)
        {
            if (string.IsNullOrWhiteSpace(html))
                throw new ArgumentException("Supplied parameter html can't be null, or empty string");

            if (string.IsNullOrWhiteSpace(selector))
                throw new ArgumentException("Supplied parameter selector can't be null, or empty string");

            this.html = html;
            this.selector = selector;
            var parser = new HtmlParser();            
            document = parser.Parse(html);
        }

        public IEnumerable<string> GetList()
        {
            var list = document.QuerySelectorAll(selector).Select(GetCleanLocator).Where(l => !string.IsNullOrWhiteSpace(l));

            if (list == null || !list.Any())
                throw new EmptyVacancyLocatorListException("There is no vacancy locators");

            return list;
        }

        private string GetCleanLocator(IElement element)
        {
            var locator = element.GetAttribute("href");
            var queryStringStart = locator.IndexOf("?");
            if (queryStringStart >= 0)
                locator = locator.Substring(0, queryStringStart);
            return locator;
        }
    }
}
