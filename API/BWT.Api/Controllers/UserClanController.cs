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
    public class UserClanController : ControllerBase
    {
        private readonly IServices _services;
        private readonly IMapper _mapper;
        public UserClanController(IServices services, IMapper mapper)
        {
            _services = services;
            _mapper = mapper;
        }

        [Route("all/")]
        [HttpGet]
        public async Task<IActionResult> GetUserClans()
        {
            var userClans = _services.GetUserClans();
            var userClansDTO = _mapper.Map<IEnumerable<UserClanDTO>>(userClans);
            var response = new ApiResponse<IEnumerable<UserClanDTO>>(userClansDTO);

            return Ok(response);
        }

        [Route("one/")]
        [HttpGet]
        public async Task<IActionResult> GetUserClan(int Id)
        {
            var userClan = await _services.GetUserClan(Id);
            var userClanDTO = _mapper.Map<UserClanDTO>(userClan);
            var response = new ApiResponse<UserClanDTO>(userClanDTO);

            return Ok(response);
        }

        [Route("new/")]
        [HttpPost]
        public async Task<IActionResult> PostUserClan(UserClanDTO userClanDTO)
        {
            var userClan = _mapper.Map<UserClan>(userClanDTO);
            var isValid = await _services.AddUserClan(userClan);
            var response = new ApiResponse<bool>(isValid);

            return Ok(response);
        }

        [Route("update/")]
        [HttpPut]
        public async Task<IActionResult> PutUserClan(UserClanDTO userClanDTO)
        {
            var userClan = _mapper.Map<UserClan>(userClanDTO);
            var isValid = await _services.UpdateUserClan(userClan);
            var response = new ApiResponse<bool>(isValid);

            return Ok(response);
        }

        [Route("delete/")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUserClan(int Id)
        {
            var isValid = await _services.DeleteUserClan(Id);
            var response = new ApiResponse<bool>(isValid);

            return Ok(response);
        }
    }
}
