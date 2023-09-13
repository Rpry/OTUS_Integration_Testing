using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using TechTalk.SpecFlow;
using WebApi.Models;

namespace WebApi.Integration.SpecFlow.Steps
{
    [Binding]
    public sealed class CourseStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly FeatureContext _featureContext;
        private readonly TestWebApplicationFactory<Startup> _factory;
        private readonly HttpClient _httpClient;

        public CourseStepDefinitions(
            ScenarioContext scenarioContext,
            FeatureContext featureContext,
            TestWebApplicationFactory<Startup> factory)
        {
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
            _factory = factory;
            _httpClient = _factory.CreateClient();
        }

        [Given(@"new course is created")]
        public async Task GivenNewCourseIsCreated()
        {
            var courseModel = new CourseModel
            {
                Name = "course_name",
                Price = (new Random()).Next(int.MaxValue)
            };
            var addCourseResponse = await _httpClient.PostAsJsonAsync("/course", courseModel);
            var courseId = JsonConvert.DeserializeObject<int>(await addCourseResponse.Content.ReadAsStringAsync());
            _scenarioContext["courseId"] = courseId;
            _scenarioContext["courseModel"] = courseModel;
        }

        [When(@"the course is requested")]
        public async Task WhenTheCourseIsBeingSearched()
        {
            var getCourseResponse = await _httpClient.GetAsync($"/course/{_scenarioContext["courseId"]}");
            _scenarioContext["getCourseResponse"] = getCourseResponse;
        }

        [Then(@"the course should be found")]
        public async Task ThenTheCourseShouldBeFound()
        {
            var getCourseResponse = _scenarioContext["getCourseResponse"] as HttpResponseMessage;
            var initialCourseModel = _scenarioContext["courseModel"] as CourseModel;
            getCourseResponse.IsSuccessStatusCode.Should().BeTrue();
            getCourseResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var courseModel = JsonConvert.DeserializeObject<CourseModel>(await getCourseResponse.Content.ReadAsStringAsync());
            courseModel.Should().NotBeNull();
            courseModel.Name.Should().Be(initialCourseModel.Name);
            courseModel.Price.Should().Be(initialCourseModel.Price);
        }
    }
}