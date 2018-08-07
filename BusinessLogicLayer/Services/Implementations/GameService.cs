using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.UnitsOfWork.Interfaces;
using Models.DataTransferObjects;
using Models.Entities;

namespace BusinessLogicLayer.Services.Implementations
{
    public class GameService : IGameService
    {
        private readonly IMapper     _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GameService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper     = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Game> GetByIdAsync(int id) => await _unitOfWork.Games.GetByIdAsync(id) ??
                                                        throw new ResourceNotFoundException($"Game with {nameof(id)}: {id} not found");


        public async Task<IEnumerable<Game>> GetAllAsync() => await _unitOfWork.Games.GetAllAsync();

        public async Task<Game> CreateAsync(GameDto gameDto)
        {
            if (await DoesGameWithThisTitleExist(gameDto.Title))
            {
                throw new NameOfResourceIsTakenException($"Game with title: {gameDto.Title} already exists");
            }

            var gameForSaving = MapFromDtoToGame(gameDto);
            await _unitOfWork.Games.CreateAsync(gameForSaving);
            await _unitOfWork.SaveChangesAsync();
            return gameForSaving;
        }

        private async Task<bool> DoesGameWithThisTitleExist(string providedTitle) => await _unitOfWork.Games.GetByTitleAsync(providedTitle) != null;

        private Game MapFromDtoToGame(GameDto gameDto) => _mapper.Map<Game>(gameDto);
    }
}