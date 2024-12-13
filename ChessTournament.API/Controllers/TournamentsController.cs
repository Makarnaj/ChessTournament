using ChessTournament.API.DTO;
using ChessTournament.API.Mappers;
using ChessTournament.BLL.Exceptions;
using ChessTournament.BLL.Interfaces;
using ChessTournament.BLL.Services;
using ChessTournament.DAL.Repositories.Interfaces;
using ChessTournament.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChessTournament.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TournamentsController : ControllerBase
    {
        private readonly ITournamentService _tournamentService;
        private readonly ICategoryService _categoryService;

        public TournamentsController(ITournamentService tournamentService, ICategoryService categoryService)
        {
            _tournamentService = tournamentService;
            _categoryService = categoryService;
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TournamentViewDTO>> GetById([FromRoute] int id)
        {
            try
            {
                Tournament? tournament = await _tournamentService.GetByIdAsync(id);

                if (tournament != null)
                {
                    return Ok(tournament.ToDTO());
                }
                return NotFound(new { message = $"Le tournoi avec l'Id {id} n'existe pas" });
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
        
        public async Task<ActionResult<TournamentViewDTO>> Create([FromBody] TournamentCreateFormDTO tournamentDTO)
        {
            try
            {
                if (tournamentDTO is null || !ModelState.IsValid)
                {
                    return BadRequest(new { message = "Les données de l'utilisateur sont invalides" });
                }

                var categories = await _categoryService.GetByIdsAsync(tournamentDTO.CategoryIds);
                if (categories == null)
                {
                    return BadRequest(new { message = "Pas de catégorie enregistrée" });
                }

                // Mapper le DTO en entité Tournament
                Tournament tournamentToAdd = tournamentDTO.ToTournament(categories);
                Tournament createdTournament = await _tournamentService.CreateAsync(tournamentToAdd);


                return CreatedAtAction(
                    nameof(GetById),
                    new { id = tournamentToAdd.Id },
                    tournamentToAdd.ToDTO()
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
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                bool isDeleted = await _tournamentService.DeleteAsync(id);

                if (!isDeleted)
                {
                    return NotFound(new { message = $"Le tournoi avec l'Id {id} n'existe pas" });
                }
                return Ok(new { message = $"Le tournoi avec l'Id {id} a été supprimé" });
            }
            catch (CustomSqlException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Une erreur interne est survenue lors de la suppression du tournoi");
            }
        
        }
        [HttpGet("tournaments")]
        public async Task<ActionResult<List<TournamentListViewDTO>>> GetTournaments()
        {
            try
            {
                var tournaments = await _tournamentService.GetTournamentsAsync();
                return Ok(tournaments.Select(t => t.ToListDTO()));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
        [HttpGet("GetDetails/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TournamentDetailsDTO>> GetTournamentDetails([FromRoute] int id)
        {
            try
            {
                var tournamentDetails = await _tournamentService.GetTournamentDetailsAsync(id);

                if (tournamentDetails == null)
                {
                    return NotFound(new { message = "Tournoi introuvable." });
                }

                return Ok(tournamentDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
