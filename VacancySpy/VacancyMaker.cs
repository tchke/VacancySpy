using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using System;

namespace VacancySpy
{
    public class VacancyMaker
    {
        string html;
        string salarySelector;
        string titleSelector;
        IHtmlDocument htmlDocument;
        public VacancyMaker(string vacancyHtml, string titleSelector, string salarySelector)
        {
            if (string.IsNullOrWhiteSpace(vacancyHtml))
                throw new ArgumentException("Supplied parameter vacancyHtml can't be null, or empty string");

            if (string.IsNullOrWhiteSpace(titleSelector))
                throw new ArgumentException("Supplied parameter titleSelector can't be null, or empty string");

            if (string.IsNullOrWhiteSpace(salarySelector))
                throw new ArgumentException("Supplied parameter salarySelector can't be null, or empty string");

            var parser = new HtmlParser();
            html = vacancyHtml;
            this.titleSelector = titleSelector;
            this.salarySelector = salarySelector;
            htmlDocument = parser.Parse(html);            
        }

        public Vacancy Make()
        {
            return new Vacancy()
            {
                Title = htmlDocument.QuerySelector(titleSelector).TextContent,
                Salary = htmlDocument.QuerySelector(salarySelector).TextContent
            };
        }
    }
}
