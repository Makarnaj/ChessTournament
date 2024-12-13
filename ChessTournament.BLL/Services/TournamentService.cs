using Azure;
using ChessTournament.BLL.Exceptions;
using ChessTournament.BLL.Interfaces;
using ChessTournament.DAL.Repositories;
using ChessTournament.DAL.Repositories.Interfaces;
using ChessTournament.Domain.Enums;
using ChessTournament.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTournament.BLL.Services
{
    public class TournamentService : ITournamentService
    {
        ITournamentRepository _repository;

        public TournamentService(ITournamentRepository tournamentRepository)
        {
            _repository = tournamentRepository;
        }

        public async Task<Tournament?> GetByIdAsync(int id)
        {
            try
            {
                return await _repository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new CustomSqlException($"Erreur lors de la récup de l'utilisateur");

            }
        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                return await _repository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new CustomSqlException($"Erreur lors de la suppression de l'utilisateur");
            }
        }
        public async Task<Tournament> CreateAsync(Tournament tournament)
        {
            try
            {
                if (tournament == null)
                {
                    throw new ArgumentNullException(nameof(tournament), "Le tournoi ne peut pas être null.");
                }

                // Validation des règles métiers
                if (string.IsNullOrWhiteSpace(tournament.Name))
                {
                    throw new ArgumentException("Le nom du tournoi est obligatoire.");
                }

                if (tournament.MinPlayers < 2 || tournament.MinPlayers > 32)
                {
                    throw new ArgumentException("Le nombre minimum de joueurs doit être entre 2 et 32.");
                }

                if (tournament.MaxPlayers < tournament.MinPlayers || tournament.MaxPlayers > 32)
                {
                    throw new ArgumentException("Le nombre maximum de joueurs doit être entre 2 et 32, et supérieur ou égal au minimum.");
                }

                if (tournament.MinELO.HasValue && tournament.MaxELO.HasValue &&
                    tournament.MaxELO > tournament.MaxELO)
                {
                    throw new ArgumentException("L'ELO minimum doit être inférieur ou égal à l'ELO maximum.");
                }

                if (tournament.RegistrationEndDate <= tournament.CreationDate.AddDays(tournament.MinPlayers))
                {
                    throw new ArgumentException("La date de fin des inscriptions doit être postérieure au délai minimum basé sur le nombre de joueurs.");
                }

                // Initialisation des propriétés par défaut
                tournament.CurrentRound = 0;
                tournament.Status = TournamentStatus.WaitingForPlayers;
                tournament.CreationDate = DateTime.Now;
                tournament.UpdateDate = DateTime.Now;

                // Vérification des contraintes spécifiques (exemple d'email pour les joueurs éligibles)
                // Peut être un envoi d'email ici pour notifier des joueurs éligibles si nécessaire

                // Enregistrement du tournoi dans la base
                return await _repository.CreateAsync(tournament);
            }
            catch (Exception ex)
            {
                // Gestion des erreurs et log si nécessaire
                throw new CustomSqlException($"Erreur lors de la création du tournoi : {ex.Message}");
            }
        }
        public async Task<Tournament?> UpdateAsync(Tournament updatedTournament)
        {
            try
            {
                if (updatedTournament == null)
                {
                    throw new ArgumentNullException(nameof(updatedTournament), "Les données du tournoi ne peuvent pas être nulles.");
                }

                // Récupération du tournoi existant
                Tournament existingTournament = await _repository.GetByIdAsync(updatedTournament.Id);
                if (existingTournament == null)
                {
                    throw new KeyNotFoundException($"Aucun tournoi trouvé avec l'ID {updatedTournament.Id}.");
                }

                // Validation des règles métiers
                if (string.IsNullOrWhiteSpace(updatedTournament.Name))
                {
                    throw new ArgumentException("Le nom du tournoi est obligatoire.");
                }

                if (updatedTournament.MinPlayers < 2 || updatedTournament.MinPlayers > 32)
                {
                    throw new ArgumentException("Le nombre minimum de joueurs doit être entre 2 et 32.");
                }

                if (updatedTournament.MaxPlayers < updatedTournament.MinPlayers || updatedTournament.MaxPlayers > 32)
                {
                    throw new ArgumentException("Le nombre maximum de joueurs doit être entre 2 et 32, et supérieur ou égal au minimum.");
                }

                if (updatedTournament.MinELO.HasValue && updatedTournament.MaxELO.HasValue &&
                    updatedTournament.MinELO > updatedTournament.MaxELO)
                {
                    throw new ArgumentException("L'ELO minimum doit être inférieur ou égal à l'ELO maximum.");
                }

                if (updatedTournament.RegistrationEndDate <= DateTime.Now.AddDays(updatedTournament.MinPlayers))
                {
                    throw new ArgumentException("La date de fin des inscriptions doit être postérieure au délai minimum basé sur le nombre de joueurs.");
                }

                if (existingTournament.Status != TournamentStatus.WaitingForPlayers)
                {
                    throw new InvalidOperationException("Seuls les tournois en attente de joueurs peuvent être modifiés.");
                }

                // Mise à jour des propriétés
                existingTournament.Name = updatedTournament.Name;
                existingTournament.Location = updatedTournament.Location;
                existingTournament.MinPlayers = updatedTournament.MinPlayers;
                existingTournament.MaxPlayers = updatedTournament.MaxPlayers;
                existingTournament.MinELO = updatedTournament.MinELO;
                existingTournament.MaxELO = updatedTournament.MaxELO;
                existingTournament.Categories = updatedTournament.Categories;
                existingTournament.WomenOnly = updatedTournament.WomenOnly;
                existingTournament.RegistrationEndDate = updatedTournament.RegistrationEndDate;
                existingTournament.UpdateDate = DateTime.Now;

                // Sauvegarde dans la base
                return await _repository.UpdateAsync(existingTournament);
            }
            catch (Exception ex)
            {
                throw new CustomSqlException($"Erreur lors de la mise à jour du tournoi : {ex.Message}");
            }
        }
        public async Task<List<Tournament>> GetTournamentsAsync()
        {
            return (await _repository.GetAllAsync())
                                .Where(t => t.Status != TournamentStatus.Finished)
                                .OrderByDescending(t => t.UpdateDate)
                                .Take(10).ToList();
        }
        public async Task<Tournament?> GetTournamentDetailsAsync(int tournamentId)
        {
            // Récupérer le tournoi avec les joueurs
            return (await _repository.GetByIdAsync(tournamentId));
                
        }
    }
}
