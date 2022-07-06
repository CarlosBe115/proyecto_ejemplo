using AutoMapper;
using BWT.Api.Responses;
using BWT.Core.DTOs;
using BWT.Core.Entities;
using BWT.Core.Filters;
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
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IServices _services;
        private readonly IMapper _mapper;
        public GamesController(IServices services, IMapper mapper)
        {
            _services = services;
            _mapper = mapper;
        }

        [Route("all/")]
        [HttpGet]
        public async Task<IActionResult> GetGames([FromQuery] GamesFilters filters)
        {
            var games = _services.GetGames(filters);
            var gamesDTO = _mapper.Map<IEnumerable<GamesDTO>>(games);
            var response = new ApiResponse<IEnumerable<GamesDTO>>(gamesDTO);

            return Ok(response);
        }

        [Route("one/")]
        [HttpGet]
        public async Task<IActionResult> GetGame(int Id)
        {
            var game = await _services.GetGame(Id);
            var gameDTO = _mapper.Map<GamesDTO>(game);
            var response = new ApiResponse<GamesDTO>(gameDTO);

            return Ok(response);
        }

        [Route("new/")]
        [HttpPost]
        public async Task<IActionResult> PostAccount(GamesDTO gamesDTO)
        {
            var games = _mapper.Map<Games>(gamesDTO);
            var isValid = await _services.AddGames(games);
            var response = new ApiResponse<bool>(isValid);

            return Ok(response);
        }

        [Route("update/")]
        [HttpPut]
        public async Task<IActionResult> PutAccount(GamesDTO gamesDTO)
        {
            var games = _mapper.Map<Games>(gamesDTO);
            var isValid = await _services.UpdateGames(games);
            var response = new ApiResponse<bool>(isValid);

            return Ok(response);
        }

        [Route("delete/")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAccount(int Id)
        {
            var isValid = await _services.DeleteGames(Id);
            var response = new ApiResponse<bool>(isValid);

            return Ok(response);
        }

    }
}
