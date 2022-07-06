using AutoMapper;
using BWT.Api.Responses;
using BWT.Core.DTOs;
using BWT.Core.Entities;
using BWT.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BWT.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        private readonly IServices _services;
        private readonly IMapper _mapper;
        public UserInfoController(IServices services, IMapper mapper)
        {
            _services = services;
            _mapper = mapper;
        }

        [Route("all/")]
        [HttpGet]
        public async Task<IActionResult> GetUsersInfo()
        {
            var users = _services.GetUsersInfo();
            var usersDTO = _mapper.Map<IEnumerable<UserInfoDTO>>(users);
            var response = new ApiResponse<IEnumerable<UserInfoDTO>>(usersDTO);

            return Ok(response);
        }

        [Route("one/")]
        [HttpGet]
        public async Task<IActionResult> GetUserInfo(int Id)
        {
            var user = await _services.GetUserInfo(Id);
            var userDTO = _mapper.Map<UserInfoDTO>(user);
            var response = new ApiResponse<UserInfoDTO>(userDTO);

            return Ok(response);
        }

        [Route("new/")]
        [HttpPost]
        public async Task<IActionResult> PostUserInfo(UserInfoDTO userDTO)
        {
            var user = _mapper.Map<UserInfo>(userDTO);
            var isValid = await _services.AddUserInfo(user);
            var response = new ApiResponse<bool>(isValid);

            return Ok(response);
        }

        [Route("update/")]
        [HttpPut]
        public async Task<IActionResult> PutUserInfo(UserInfoDTO userDTO)
        {
            var user = _mapper.Map<UserInfo>(userDTO);
            var isValid = await _services.UpdateUserInfo(user);
            var response = new ApiResponse<bool>(isValid);

            return Ok(response);
        }

        [Route("delete/")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUserInfo(int Id)
        {
            var isValid = await _services.DeleteUserInfo(Id);
            var response = new ApiResponse<bool>(isValid);

            return Ok(response);
        }

    }
}
