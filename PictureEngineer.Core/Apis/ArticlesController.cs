using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PictureEngineer.Data;
using PictureEngineer.Data.Domains;
using PictureEngineer.Data.DTO;
using PictureEngineer.Data.Entities;
using PictureEngineer.Reponsitories;

namespace PictureEngineer.Core.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/blogs")]
    public class ArticlesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork unitOfWork;

        public ArticlesController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogDomain>>> GetBlogs()
        {
            try
            {
                IEnumerable<Blog> blogs = await unitOfWork.Articles.GetBlogsAsync();
                return Ok(_mapper.Map<IEnumerable<BlogDomain>>(blogs));
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("{serviceID}")]
        public async Task<ActionResult<IEnumerable<BlogDomain>>> GetBlogByService(int serviceID)
        {
            try{
                IEnumerable<Blog> blogsByService = (await unitOfWork.Articles.GetBlogsAsync()).Where(x => x.ServiceId == serviceID).ToList();

                return Ok(_mapper.Map<IEnumerable<BlogDomain>>(blogsByService));
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("{meta}/detail")]
        public async Task<ActionResult<BlogDomain>> GetDetail(string meta)
        {
            Blog blog = await unitOfWork.Articles.GetCurentBlogByMetaAsync(meta);
            return Ok(_mapper.Map<BlogDomain>(blog));
        }
    }
}