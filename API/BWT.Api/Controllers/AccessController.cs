using AutoMapper;
using BWT.Api.Responses;
using BWT.Core.DTOs;
using BWT.Core.Entities;
using BWT.Core.Filters;
using BWT.Core.Services;
using BWT.Infrastructure.Cryptography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BWT.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessController : ControllerBase
    {
        private readonly IServices _services;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public AccessController(IServices services, IMapper mapper, IConfiguration configuration)
        {
            _configuration = configuration;
            _services = services;
            _mapper = mapper;
        }

        [Route("validation/")]
        [HttpPost]
        public async Task<IActionResult> Validation(AccessDTO accessDTO)
        {
            var access = _mapper.Map<Access>(accessDTO);
            var response = await _services.UserValidation(access);
            var responseDTO = _mapper.Map<AccessDTO>(response);

            //Asignacion Del Token
            if (responseDTO != null)
                responseDTO.TokenValidation = Tokens.JWT(responseDTO, _configuration);

            var apiresponse = new ApiResponse<AccessDTO>(responseDTO);

            return Ok(apiresponse);
        }

        [Route("new/")]
        [HttpPost]
        public async Task<IActionResult> PostAccount(AccessDTO accessDTO)
        {
            var access = _mapper.Map<Access>(accessDTO);
            var isValid = await _services.AddAccess(access);
            var response = new ApiResponse<bool>(isValid);

            return Ok(response);
        }

        [Route("initialpr/")]
        [HttpGet]
        public async Task<IActionResult> InitialPasswordRecovery(string email)
        {
            var isSend = await _services.GeneratorTokenPasswordRecovery(email);
            var apiresponse = new ApiResponse<string>(isSend);

            return Ok(apiresponse);
        }

        [Route("finalpr/")]
        [HttpGet]
        public async Task<IActionResult> FinalPasswordRecovery(string token, string password1, string password2)
        {
            var isChange = await _services.ChangePassword(token, password1, password2);
            var apiresponse = new ApiResponse<bool>(isChange);

            return Ok(apiresponse);
        }

        [Authorize]
        [Route("all/")]
        [HttpGet]
        public async Task<IActionResult> GetAccounts([FromQuery]AccessFilters filters)
        {
            var accounts = _services.GetAccesses(filters);
            var accountsDTO = _mapper.Map<IEnumerable<AccessDTO>>(accounts);
            var response = new ApiResponse<IEnumerable<AccessDTO>>(accountsDTO);

            return Ok(response);
        }

        [Authorize]
        [Route("one/")]
        [HttpGet]
        public async Task<IActionResult> GetAccount(int Id)
        {
            var account = await _services.GetAccess(Id);
            var accountDTO = _mapper.Map<AccessDTO>(account);
            var response = new ApiResponse<AccessDTO>(accountDTO);

            return Ok(response);
        }

        [Authorize]
        [Route("update/")]
        [HttpPut]
        public async Task<IActionResult> PutAccount(AccessDTO accessDTO)
        {
            var access = _mapper.Map<Access>(accessDTO);
            var isValid = await _services.UpdateAccess(access);
            var response = new ApiResponse<bool>(isValid);

            return Ok(response);
        }

        [Authorize]
        [Route("delete/")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAccount(int Id)
        {
            var isValid = await _services.DeleteAccess(Id);
            var response = new ApiResponse<bool>(isValid);

            return Ok(response);
        }

    }
}
