using System;
using System.Threading.Tasks.Dataflow;

namespace VacancySpy
{
    public class Spy
    {
        Action<Vacancy> outerHandler;
        TransformBlock<string, string> fetchVacancyListHtml;
        SourceConfiguration configuration;

        public Spy(Action<Vacancy> outerHandler, Action<AggregateException> errorHandler, ISourceConfigurationProvider configProvider)
        {
            this.outerHandler = outerHandler;
            this.configuration = configProvider.GetConfiguration();

            fetchVacancyListHtml = new TransformBlock<string, string>(urlParam => {
                var vacancyListFetcher = new Fetcher(urlParam);
                return vacancyListFetcher.Fetch();
            });

            var breakIntoSnippets = new TransformManyBlock<string, string>(html => {
                var vacancyLocatorList = new VacancyLocatorList(html, configuration.VacancyListItemSelector);
                var vacancyLocators = vacancyLocatorList.GetList();
                return vacancyLocators;
            });

            var fetchVacancyHtml = new TransformBlock<string, string>(l => {
                var vacancyFetcher = new Fetcher(l);
                return vacancyFetcher.Fetch();
            });

            var makeVacancy = new TransformBlock<string, Vacancy>(html => {
                var vacancyMaker = new VacancyMaker(html
                    , configuration.VacancyTitleSelector
                    , configuration.VacancySalarySelector);
                return vacancyMaker.Make();
            });

            var doOutAction = new ActionBlock<Vacancy>(outerHandler);

            fetchVacancyListHtml.LinkTo(breakIntoSnippets);
            breakIntoSnippets.LinkTo(fetchVacancyHtml);
            fetchVacancyHtml.LinkTo(makeVacancy);
            makeVacancy.LinkTo(doOutAction);

            fetchVacancyListHtml.Completion.ContinueWith(t => {
                if (t.IsFaulted) ((IDataflowBlock)breakIntoSnippets).Fault(t.Exception);
                breakIntoSnippets.Complete();
            });
            breakIntoSnippets.Completion.ContinueWith(t => {
                if (t.IsFaulted) ((IDataflowBlock)fetchVacancyHtml).Fault(t.Exception);
                fetchVacancyHtml.Complete();
            });
            fetchVacancyHtml.Completion.ContinueWith(t => {
                if (t.IsFaulted) ((IDataflowBlock)makeVacancy).Fault(t.Exception);
                makeVacancy.Complete();
            });
            makeVacancy.Completion.ContinueWith(t => {
                if (t.IsFaulted) ((IDataflowBlock)doOutAction).Fault(t.Exception);
                doOutAction.Complete();
            });
            doOutAction.Completion.ContinueWith(t => {
                if (t.IsFaulted)
                    errorHandler(t.Exception);
            });
        }

        public void Run()
        {
            fetchVacancyListHtml.Post(configuration.Url);
            fetchVacancyListHtml.Complete();
        }
    }
}
