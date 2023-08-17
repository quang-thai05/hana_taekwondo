﻿using Microsoft.AspNetCore.Mvc;
using StudentManagement.DTOs.Output;
using StudentManagement.Models;
using StudentManagement.Services;
using StudentManagement.Services.Interfaces;

namespace StudentManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClassController : Controller
    {
        private readonly IClassService _classService;

        public ClassController(IClassService classService)
        {
            _classService = classService;
        }

        [HttpPost]
        public ActionResult<ApiResponseModel> AddStudentToClass(Student _student, Class _class)
        {
            try
            {
                var students = _classService.AddNewStudentToClass(_student, _class);
                return Ok(new ApiResponseModel
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Add Student Success!",
                    Data = students,
                    IsSuccess = true
                });
            }
            catch (Exception ex)
            {
                return Conflict(new ApiResponseModel
                {
                    Code = StatusCodes.Status409Conflict,
                    Message = ex.Message,
                    IsSuccess = false
                });
            }
        }
    }
}
