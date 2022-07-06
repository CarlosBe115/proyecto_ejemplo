using AutoMapper;
using BWT.Api.Responses;
using BWT.Core.DTOs;
using BWT.Core.Entities;
using BWT.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BWT.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IServices _services;
        private readonly IMapper _mapper;
        public RolController(IServices services, IMapper mapper)
        {
            _services = services;
            _mapper = mapper;
        }

        [Route("users/")]
        [HttpGet]
        public async Task<IActionResult> GetRols()
        {
            var rols = _services.GetRols();
            var rolsDTO = _mapper.Map<IEnumerable<RolDTO>>(rols);
            var response = new ApiResponse<IEnumerable<RolDTO>>(rolsDTO);

            return Ok(response);
        }

        [Route("members/")]
        [HttpGet]
        public async Task<IActionResult> GetUCRols()
        {
            var rols = _services.GetUCRols();
            var rolsDTO = _mapper.Map<IEnumerable<UCRolDTO>>(rols);
            var response = new ApiResponse<IEnumerable<UCRolDTO>>(rolsDTO);

            return Ok(response);
        }


    }
}
