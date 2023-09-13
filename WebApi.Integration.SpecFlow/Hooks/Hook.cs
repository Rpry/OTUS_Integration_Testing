using Allure.Commons;
using TechTalk.SpecFlow;

namespace WebApi.Integration.SpecFlow.Hooks
{
    [Binding]
    public class Hooks
    {
        static AllureLifecycle _allureLifecycle = AllureLifecycle.Instance;

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            _allureLifecycle.CleanupResultDirectory();
        }
        
    }
}