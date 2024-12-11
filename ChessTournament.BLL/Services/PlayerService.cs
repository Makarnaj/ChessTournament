using ChessTournament.BLL.Exceptions;
using ChessTournament.BLL.Interfaces;
using ChessTournament.DAL.Repositories.Interfaces;
using ChessTournament.Domain.Enums;
using ChessTournament.Domain.Models;
using Isopoh.Cryptography.Argon2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTournament.BLL.Services
{
    public class PlayerService : IPlayerService
    {
        IPlayerRepository _playerRepository;

        public PlayerService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task<IEnumerable<Player>> GetAllAsync()
        {
            try
            {
                return await _playerRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new CustomSqlException($"Erreur lors de la récupération de la liste : {ex.Message}");
            }
        }

        public async Task<Player?> GetByIdAsync(int id)
        {
            try
            {
                return await _playerRepository.GetByIdAsync(id);
            }
            catch(Exception ex)
            {
                throw new CustomSqlException($"Erreur lors de la récupération de l'utilisateur : {ex.Message}");
            }
        }
        public async Task<Player> CreateAsync(Player player)
        {
            try
            {
                if (player == null)
                {
                    throw new ArgumentNullException(nameof(player), "Le player ne peut pas être null");
                }
                if (string.IsNullOrWhiteSpace(player.Pseudo))
                {
                    throw new ArgumentException("Pseudo obligatoire");
                }
                if (!Enum.IsDefined(typeof(Gender), player.Gender))
                {
                    throw new ArgumentException("Genre invalide");
                }
                Player playerCheckPseudo = await _playerRepository.GetByPseudoAsync(player.Pseudo);
                if (playerCheckPseudo != null)
                {
                    throw new ArgumentException("Pseudo déja utilisé");
                }
                Player playerCheckEmail = await _playerRepository.GetByEmailAsync(player.Email);
                if (playerCheckEmail!= null)
                {
                    throw new ArgumentException("Email déja utilisé");
                }
                if (!Enum.IsDefined(typeof(PlayerRole), player.Role))
                {
                    throw new ArgumentException("Role invalide");
                }
                string passwordHash = Argon2.Hash(player.PasswordHash);
                player.PasswordHash = passwordHash;

                return await _playerRepository.CreateAsync(player);
            }
            catch (Exception ex)
            {
                throw new CustomSqlException($"Erreur lors de la création du player : {ex.Message}");
            }
        }
    }
}
