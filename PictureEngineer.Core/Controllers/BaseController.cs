
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PictureEngineer.Common;

namespace PictureEngineer.Core.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// response api data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected JsonResult Responsed(object data)
        {
            return Json(new JMessage
            {
                success = true,
                code = 200,
                message = "",
                data = data,
                errors = false
            }, new Newtonsoft.Json.JsonSerializerSettings());
        }
        //
        // Summary:
        //     Response API
        //
        // Parameters:
        //   data:
        //     The object to serialize.
        //
        // Returns:
        //     data as JSON Response APi format for the response.
        protected JsonResult ResponsedSuccess(string message, object data = null)
        {
            return Json(new JMessage
            {
                success = true,
                code = 200,
                message = message,
                data = data,
                errors = false
            }, new Newtonsoft.Json.JsonSerializerSettings());
        }

        //
        // Summary:
        //     Response Success API
        //
        // Parameters:
        //   data:
        //     The object to serialize.
        //
        // Returns:
        //     data as JSON Response APi format for the response.
        protected JsonResult ResponsedError(string message, object data = null, bool errors = false)
        {
            return Json(new JMessage
            {
                success = false,
                code = 404,
                message = message,
                data = data,
                errors = errors
            }, new Newtonsoft.Json.JsonSerializerSettings());
        }

        //
        // Summary:
        //     Response Error APi
        //
        // Parameters:
        //   data:
        //     The object to serialize.
        //
        // Returns:
        //     data as JSON Response APi format for the response.
        protected JsonResult ResponsedError(object message, object data = null, bool errors = false)
        {
            return Json(new JMessage
            {
                success = false,
                code = 404,
                message = message,
                data = data,
                errors = errors
            }, new Newtonsoft.Json.JsonSerializerSettings());
        }
    }
}