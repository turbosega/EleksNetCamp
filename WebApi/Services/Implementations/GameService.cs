using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using WebApi.Exceptions;
using WebApi.Models.DataTransferObjects;
using WebApi.Models.Entities;
using WebApi.Services.Interfaces;
using WebApi.UnitsOfWork.Interfaces;

namespace WebApi.Services.Implementations
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper     _mapper;

        public GameService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper     = mapper;
        }

        public async Task<Game> GetByIdAsync(int id)
        {
            var game = await _unitOfWork.Games.GetByIdAsync(id);
            if (game == null)
            {
                throw new ResourceNotFoundException($"Game with id: {id} not found");
            }

            return game;
        }

        public async Task<IEnumerable<Game>> GetAllAsync() => await _unitOfWork.Games.GetAllAsync();

        public async Task<Game> CreateAsync(GameDto gameDto)
        {
            if (await IsGameWithThisLoginExists(gameDto.Title))
            {
                return null;
            }

            var gameForSaving = MapFromDtoToGame(gameDto);
            await _unitOfWork.Games.CreateAsync(gameForSaving);
            await _unitOfWork.SaveChangesAsync();
            return gameForSaving;
        }

        // private methods

        private async Task<bool> IsGameWithThisLoginExists(string providedTitle) => await _unitOfWork.Games.GetByTitleAsync(providedTitle) != null;

        private Game MapFromDtoToGame(GameDto gameDto) => _mapper.Map<Game>(gameDto);
    }
}