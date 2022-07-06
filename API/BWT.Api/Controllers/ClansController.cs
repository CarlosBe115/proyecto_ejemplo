using AutoMapper;
using BWT.Api.Responses;
using BWT.Core.DTOs;
using BWT.Core.Entities;
using BWT.Core.Filters;
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
    public class ClansController : ControllerBase
    {
        private readonly IServices _services;
        private readonly IMapper _mapper;

        public ClansController(IServices services, IMapper mapper)
        {
            _services = services;
            _mapper = mapper;
        }


        [Route("all/")]
        [HttpGet]
        public async Task<IActionResult> GetClans([FromQuery]ClansFilters filters)
        {
            var clans = _services.GetClans(filters);
            var clansDTO = _mapper.Map<IEnumerable<ClansDTO>>(clans);
            var response = new ApiResponse<IEnumerable<ClansDTO>>(clansDTO);

            return Ok(response);
        }

        [Route("one/")]
        [HttpGet]
        public async Task<IActionResult> GetClan(int Id)
        {
            var clan = await _services.GetClan(Id);
            var clanDTO = _mapper.Map<ClansDTO>(clan);
            var response = new ApiResponse<ClansDTO>(clanDTO);

            return Ok(response);
        }

        [Route("new/")]
        [HttpPost]
        public async Task<IActionResult> PostClan(ClansDTO clansDTO)
        {
            var clan = _mapper.Map<Clans>(clansDTO);
            var isValid = await _services.AddClans(clan);
            var response = new ApiResponse<bool>(isValid);

            return Ok(response);
        }

        [Route("update/")]
        [HttpPut]
        public async Task<IActionResult> PutClans(ClansDTO clansDTO)
        {
            var clan = _mapper.Map<Clans>(clansDTO);
            var isValid = await _services.UpdateClans(clan);
            var response = new ApiResponse<bool>(isValid);

            return Ok(response);
        }

        [Route("delete/")]
        [HttpDelete]
        public async Task<IActionResult> DeleteClan(int Id)
        {
            var isValid = await _services.DeleteClans(Id);
            var response = new ApiResponse<bool>(isValid);

            return Ok(response);
        }

    }
}
