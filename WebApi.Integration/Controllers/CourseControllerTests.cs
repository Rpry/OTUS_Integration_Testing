using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using WebApi.Models;
using Xunit;

namespace WebApi.Integration.Controllers
{
    public class CourseControllerTests
    {
        private HttpClient _httpClient;
        private string baseUri = "http://localhost:5000";
        public CourseControllerTests()
        {
            _httpClient = new HttpClient();
        }
        
        [Fact]
        public async Task CreateCourse_ShouldCreateCourse()
        {
            //Arrange 
            var request = new CourseModel
            {
                Name = "course_name",
                Price = (new Random()).Next(int.MaxValue)
            };
            var addCourseResponse = await _httpClient.PostAsJsonAsync($"{baseUri}/course", request);
            var actual = JsonConvert.DeserializeObject<int>(await addCourseResponse.Content.ReadAsStringAsync());
            
            //Act
            var getCourseResponse = await _httpClient.GetAsync($"{baseUri}/course/{actual}");
            
            //Assert
            addCourseResponse.IsSuccessStatusCode.Should().BeTrue();
            addCourseResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var courseModel = JsonConvert.DeserializeObject<CourseModel>(await getCourseResponse.Content.ReadAsStringAsync());
            courseModel.Should().NotBeNull();
            courseModel.Name.Should().Be(request.Name);
            courseModel.Price.Should().Be(request.Price);
        }
    }
}