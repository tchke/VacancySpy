using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacancySpy
{
    class Program
    {
        static void Main(string[] args)
        {
            var sourceConfigurationProvider = new SimpleSourceConfigurationProvider(
                    "https://ufa.hh.ru/search/vacancy?clusters=true&text=%D0%9F%D1%80%D0%BE%D0%B3%D1%80%D0%B0%D0%BC%D0%BC%D0%B8%D1%81%D1%82&search_field=name&enable_snippets=true"
                    , "a.search-result-item__name.search-result-item__name_premium.HH-LinkModifier"
                    , "h1.title.b-vacancy-title"
                    , "div.b-vacancy-info td.l-content-colum-1.b-v-info-content div"
                );

            var spy = new Spy(Vacancy => {
                Console.WriteLine(Vacancy.Title);
                Console.WriteLine(Vacancy.Salary);
                Console.WriteLine();
            }, 
            exception => {
                Console.WriteLine("Unfortunately, something goes wrong. Sorry");
            },
            sourceConfigurationProvider);
            spy.Run();

            Console.ReadLine();
        }
    }
}
