using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using WebApi.Models;
using Xunit;

namespace WebApi.Integration.Tests
{
    public class CourseControllerTests : IClassFixture<TestFixture>
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUri;
        
        public CourseControllerTests(TestFixture testFixture)
        {
            _httpClient = new HttpClient();
            var configuration = testFixture.Configuration;
            _baseUri = configuration["BaseUri"];
        }
        
        [Fact]
        public async Task CourseShouldBeCreatedSuccessfully()
        {
            //Arrange 
            var initialCourseModel = new CourseModel
            {
                Name = "course_name",
                Price = (new Random()).Next(int.MaxValue)
            };
            var addCourseResponse = await _httpClient.PostAsJsonAsync($"{_baseUri}/course", initialCourseModel);
            var courseId = JsonConvert.DeserializeObject<int>(await addCourseResponse.Content.ReadAsStringAsync());
            
            //Act
            var getCourseResponse = await _httpClient.GetAsync($"{_baseUri}/course/{courseId}");
            
            //Assert
            addCourseResponse.IsSuccessStatusCode.Should().BeTrue();
            addCourseResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var courseModel = JsonConvert.DeserializeObject<CourseModel>(await getCourseResponse.Content.ReadAsStringAsync());
            courseModel.Should().NotBeNull();
            courseModel.Name.Should().Be(initialCourseModel.Name);
            courseModel.Price.Should().Be(initialCourseModel.Price);
        }
    }
}