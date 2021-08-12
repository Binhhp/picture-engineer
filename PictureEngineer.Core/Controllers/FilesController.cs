using Firebase.Storage;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using PictureEngineer.Reponsitories.Firebase;
using PictureEngineer.Controllers;
using PictureEngineer.Data;
using PictureEngineer.Data.Entities;
using PictureEngineer.Reponsitories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;

namespace PictureEngineer.Core.Controllers.Admin
{
    public class FilesController : BaseController
    {
        private readonly IUnitOfWork unitOfWork;

        public FilesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("api/files/{FilePath}")]
        public async Task<IActionResult> OnPostFiles([FromRoute]string FilePath)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(FilePath))
                    return BadRequest();

               await unitOfWork.Files.Create(new Files
                {
                    DateCreate = DateTime.Now,
                    FileName = FilePath
                });

                return NoContent();
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [Route("/admin/Files")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet("/admin/GetFiles")]
        public async Task<JsonResult> GetFiles()
        {
            try
            {
                var files = await unitOfWork.Files.GetAll();
                return Responsed(files);
            }
            catch(Exception ex)
            {
                return ResponsedError(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("/admin/ViewFile/{fileId}")]
        public async Task<JsonResult> ViewFile(int fileId)
        {
            try
            {
                string file = await unitOfWork.Files.GetFilePath(fileId);
                if (file == null) return ResponsedError("Không tìm thấy file.");

                return Responsed(file);
            }
            catch(Exception ex)
            {
                return ResponsedError(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("/admin/DownloadFile/{fileId}")]
        public async Task<JsonResult> DownloadFile(int fileId)
        {
            try
            {
                string file = await unitOfWork.Files.GetFilePath(fileId);
                if (file == null) return ResponsedError("Không tìm thấy file.");

                WebClient webClient = new WebClient();
                byte[] image = webClient.DownloadData(file);

                var output = new JObject
                {
                    { "FileName", "download" },
                    { "FileData", image }
                };

                return Responsed(output);
            }
            catch (Exception ex)
            {
                return ResponsedError(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("/admin/DeleteFile/{fileId}")]
        public async Task<JsonResult> DeleteFile(int fileId)
        {
            try
            {

               await  unitOfWork.Files.Delete(fileId);

                return ResponsedSuccess("Xóa file thành công.");
            }
            catch (Exception ex)
            {
                return ResponsedError(ex.Message);
            }
        }
    }
}
