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
    public class PartnersController : ControllerBase
    {
        private readonly IServices _services;
        private readonly IMapper _mapper;
        public PartnersController(IServices services, IMapper mapper)
        {
            _services = services;
            _mapper = mapper;
        }

        [Route("all/")]
        [HttpGet]
        public async Task<IActionResult> GetPartners()
        {
            var partners = _services.GetPartners();
            var partnersDTO = _mapper.Map<IEnumerable<PartnersDTO>>(partners);
            var response = new ApiResponse<IEnumerable<PartnersDTO>>(partnersDTO);

            return Ok(response);
        }

        [Route("one/")]
        [HttpGet]
        public async Task<IActionResult> GetPartner(int Id)
        {
            var partner = await _services.GetPartner(Id);
            var partnerDTO = _mapper.Map<PartnersDTO>(partner);
            var response = new ApiResponse<PartnersDTO>(partnerDTO);

            return Ok(response);
        }

        [Route("new/")]
        [HttpPost]
        public async Task<IActionResult> PostPartners(PartnersDTO partnersDTO)
        {
            var partners = _mapper.Map<Partners>(partnersDTO);
            var isValid = await _services.AddPartners(partners);
            var response = new ApiResponse<bool>(isValid);

            return Ok(response);
        }

        [Route("update/")]
        [HttpPut]
        public async Task<IActionResult> PutPartners(PartnersDTO partnersDTO)
        {
            var partners = _mapper.Map<Partners>(partnersDTO);
            var isValid = await _services.UpdatePartners(partners);
            var response = new ApiResponse<bool>(isValid);

            return Ok(response);
        }

        [Route("delete/")]
        [HttpDelete]
        public async Task<IActionResult> DeletePartners(int Id)
        {
            var isValid = await _services.DeletePartners(Id);
            var response = new ApiResponse<bool>(isValid);

            return Ok(response);
        }
    }
}
