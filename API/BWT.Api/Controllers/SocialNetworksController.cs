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
    public class SocialNetworksController : ControllerBase
    {
        private readonly IServices _services;
        private readonly IMapper _mapper;
        public SocialNetworksController(IServices services, IMapper mapper)
        {
            _services = services;
            _mapper = mapper;
        }

        [Route("all/")]
        [HttpGet]
        public async Task<IActionResult> GetSocialNetworks()
        {
            var socials = _services.GetSocialNetworks();
            var socialsDTO = _mapper.Map<IEnumerable<SocialNetworksDTO>>(socials);
            var response = new ApiResponse<IEnumerable<SocialNetworksDTO>>(socialsDTO);

            return Ok(response);
        }

        [Route("one/")]
        [HttpGet]
        public async Task<IActionResult> GetSocialNetwork(int Id)
        {
            var social = await _services.GetSocialNetwork(Id);
            var socialDTO = _mapper.Map<SocialNetworksDTO>(social);
            var response = new ApiResponse<SocialNetworksDTO>(socialDTO);

            return Ok(response);
        }

        [Route("new/")]
        [HttpPost]
        public async Task<IActionResult> PostSocialNetworks(SocialNetworksDTO socialDTO)
        {
            var social = _mapper.Map<SocialNetworks>(socialDTO);
            var isValid = await _services.AddSocialNetworks(social);
            var response = new ApiResponse<bool>(isValid);

            return Ok(response);
        }

        [Route("update/")]
        [HttpPut]
        public async Task<IActionResult> PutSocialNetworks(SocialNetworksDTO socialDTO)
        {
            var social = _mapper.Map<SocialNetworks>(socialDTO);
            var isValid = await _services.UpdateSocialNetworks(social);
            var response = new ApiResponse<bool>(isValid);

            return Ok(response);
        }

        [Route("delete/")]
        [HttpDelete]
        public async Task<IActionResult> DeleteSocialNetworks(int Id)
        {
            var isValid = await _services.DeleteSocialNetworks(Id);
            var response = new ApiResponse<bool>(isValid);

            return Ok(response);
        }

    }
}
