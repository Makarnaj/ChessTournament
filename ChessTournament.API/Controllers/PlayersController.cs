using ChessTournament.API.DTO;
using ChessTournament.API.Mappers;
using ChessTournament.BLL.Exceptions;
using ChessTournament.BLL.Interfaces;
using ChessTournament.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace ChessTournament.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersController : ControllerBase
    {

        private readonly IPlayerService _playerService;

        public PlayersController(IPlayerService playerService)
        {
            _playerService = playerService;
        }
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PlayerViewDTO>> GetById([FromRoute] int id)
        {
            try
            {
                Player? player = await _playerService.GetByIdAsync(id);

                if (player != null)
                {
                    return Ok(player.ToDTO());
                }
                return NotFound(new { message = $"L'utilisateur avec l'Id {id} n'existe pas" });
            }
            catch (CustomSqlException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Une erreur interne est survenue lors de la récupération de l'utilisateur");
            }
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[Authorize(Roles = "Member,Checkmate")]
        public async Task<ActionResult<PlayerViewDTO>> Create([FromBody] PlayerCreateFormDTO playerDTO)
        {
            try
            {
                if (playerDTO is null || !ModelState.IsValid)
                {
                    return BadRequest(new { message = "Les données de l'utilisateur sont invalides" });
                }

                Player? playerToAdd = await _playerService.CreateAsync(playerDTO.ToPlayer());

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = playerToAdd.Id },
                    playerToAdd.ToDTO()
                    );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (CustomSqlException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Une erreur interne est survenue lors de la création de l'utilisateur");
            }
        }
    }
}
