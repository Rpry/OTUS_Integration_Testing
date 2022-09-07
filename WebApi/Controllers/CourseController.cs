using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.Abstractions;
using BusinessLogic.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CourseController: ControllerBase
    {
        private ICourseService _service;
        private IMapper _mapper;
        private readonly ILogger<CourseController> _logger;

        public CourseController(ICourseService service, ILogger<CourseController> logger, IMapper mapper)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(_mapper.Map<CourseModel>(await _service.GetById(id)));
        }

        [HttpPost]
        public async Task<IActionResult> Add(CourseModel lessonDto)
        {
            return Ok(await _service.Create(_mapper.Map<CourseDto>(lessonDto)));
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, CourseModel lessonDto)
        {
            await _service.Update(id, _mapper.Map<CourseDto>(lessonDto));
            return Ok();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok();
        }
        
        [HttpGet("list/{page}/{itemsPerPage}")]
        public async Task<IActionResult> GetList(int page, int itemsPerPage)
        {
            return Ok(_mapper.Map<List<CourseModel>>(await _service.GetPaged(page, itemsPerPage)));
        }
    }
}