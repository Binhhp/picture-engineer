using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using PictureEngineer.Data.DTO;
using PictureEngineer.Data.Entities;
using PictureEngineer.Reponsitories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using PictureEngineer.Data;

namespace PictureEngineer.Core.Controllers
{
    [Route("admin/blogs")]
    [Authorize]
    public class BlogController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHostingEnvironment environment;
        private readonly IMapper mapper;
        private readonly PictureEngineerDbContext _context;
        public BlogController(IUnitOfWork unitOfWork, IHostingEnvironment environment, IMapper mapper, PictureEngineerDbContext context)
        {
            this.unitOfWork = unitOfWork;
            this.environment = environment;
            this.mapper = mapper;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewData["Service"] = _context.Services.ToList();
            return View();
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<IEnumerable<Blog>>> GetBlogs()
        {
            var list = await unitOfWork.Articles.GetBlogsAsync();
            return Ok(new { data = list });
        }

        [HttpPost]
        public async Task<IActionResult> Create(BlogDto input)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Blog blog = mapper.Map<Blog>(input);

                    if (input.FileUpload == null || input.FileUpload.Length == 0)
                    {
                        return BadRequest();
                    }

                    blog.ImageName = input.FileUpload.FileName;
                    string uploads = Path.Combine(environment.WebRootPath, $"image");
                    string filePath = Path.Combine(uploads, input.FileUpload.FileName);

                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await input.FileUpload.CopyToAsync(fileStream);
                    }

                    using (FileStream fs = new FileStream(filePath, FileMode.Open))
                    {

                        string result = await unitOfWork.Articles.Create(blog, fs);
                        if (result != "") return BadRequest(result);
                        fs.Close();
                    }

                    System.IO.File.Delete(filePath);
                }

                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{blogId}")]
        public async Task<IActionResult> Update(BlogDto input, int blogId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Blog blog = mapper.Map<Blog>(input);
                    blog.Id = blogId;

                    if (input.FileUpload == null || input.FileUpload.Length == 0)
                    {
                        await unitOfWork.Articles.Update(blog, null);
                        return NoContent();
                    }

                    blog.ImageName = input.FileUpload.FileName;
                    string uploads = Path.Combine(environment.WebRootPath, $"image");
                    string filePath = Path.Combine(uploads, input.FileUpload.FileName);

                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await input.FileUpload.CopyToAsync(fileStream);
                    }

                    using (FileStream fs = new FileStream(filePath, FileMode.Open))
                    {

                        blog = await unitOfWork.Articles.Update(blog, fs);

                        fs.Close();
                    }

                    System.IO.File.Delete(filePath);
                    return Ok(blog);

                }
                else
                {
                    return BadRequest();
                }

            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpDelete("{key}")]
        public async Task<IActionResult> Delete(int key)
        {
            var result = await unitOfWork.Articles.DeleteAsync(key);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
