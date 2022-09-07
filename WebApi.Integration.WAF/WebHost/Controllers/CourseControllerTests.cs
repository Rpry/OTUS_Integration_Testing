using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using WebApi.Models;
using Xunit;

namespace WebApi.Integration.WebHost.Controllers
{
    public class CourseControllerTests : IClassFixture<TestWebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        
        public CourseControllerTests(TestWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }
        
        [Fact]
        public async Task CourseShouldBeCreatedSuccessfully()
        {
            //Arrange 
            var client = _factory.CreateClient();
            var initialCourseModel = new CourseModel
            {
                Name = "course_name",
                Price = (new Random()).Next(int.MaxValue)
            };
            var addCourseResponse = await client.PostAsJsonAsync("/course", initialCourseModel);
            // var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            // var addCourseResponse = await client.PostAsync("/course", content);
            var courseId = JsonConvert.DeserializeObject<int>(await addCourseResponse.Content.ReadAsStringAsync());
            
            //Act
            var getCourseResponse = await client.GetAsync($"/course/{courseId}");
            
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