namespace VacancySpy
{
    public class SimpleSourceConfigurationProvider : ISourceConfigurationProvider
    {
        SourceConfiguration configuration;

        public SimpleSourceConfigurationProvider(string url, string vacancyListItemSelector, string vacancyTitleSelector, string vacancySalarySelector)
        {
            configuration = new SourceConfiguration
            {
                Url = url,
                VacancyListItemSelector = vacancyListItemSelector,
                VacancyTitleSelector = vacancyTitleSelector,
                VacancySalarySelector = vacancySalarySelector
            };
        }

        public SourceConfiguration GetConfiguration()
        {
            return configuration;
        }
    }
}
