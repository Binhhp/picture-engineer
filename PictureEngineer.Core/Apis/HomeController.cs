using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PictureEngineer.Data.DTO;
using PictureEngineer.Data.Entities;
using PictureEngineer.Reponsitories;

namespace PictureEngineer.Core.Controllers
{
    [ApiController]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class HomeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork unitOfWork;

        public HomeController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get services
        /// </summary>
        /// <returns></returns>
        [HttpGet("api/services")]
        public ActionResult<IEnumerable<ServicesDto>> GetServices()
        {
            try
            {
                IEnumerable<Services> servicesList = unitOfWork.Services.GetAll();

                return Ok(_mapper.Map<IEnumerable<ServicesDto>>(servicesList));
            }
            catch 
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Get services
        /// </summary>
        /// <returns></returns>
        [HttpGet("api/faqs")]
        public ActionResult<IEnumerable<FAQsDto>> GetFAQs()
        {
            try
            {
                IEnumerable<FAQs> faqs = unitOfWork.FAQs.GetAll();

                return Ok(_mapper.Map<IEnumerable<FAQsDto>>(faqs));
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Detail Service
        /// </summary>
        /// <param name="meta"></param>
        /// <returns></returns>
        [HttpGet("api/services/{meta}")]
        public ActionResult<ServicesDto> GetDetailService(string meta)
        {
            try
            {
                Services service = unitOfWork.Services.GetAll().Where(x => x.Meta.Equals(meta)).First();

                if(service == null)
                {
                    return NotFound();
                }

                ServicesDto dto = _mapper.Map<ServicesDto>(service);
                var faqs = unitOfWork.FAQs.GetAll().Where(x => x.ServiceID == service.Id).ToList();

                dto.FAQs = faqs;
                return Ok(dto);
            }
            catch 
            {
                return StatusCode(500, "Internal server error");
            }
        }
        
    }
}