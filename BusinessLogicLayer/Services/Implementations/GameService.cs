using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Services.Interfaces;
using BusinessLogicLayer.Utilities.Interfaces;
using DataAccessLayer.UnitsOfWork.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Models.DataTransferObjects;
using Models.DataTransferObjects.Creating;
using Models.Entities;

namespace BusinessLogicLayer.Services.Implementations
{
    public class GameService : IGameService
    {
        private readonly IMapper           _mapper;
        private readonly IUnitOfWork       _unitOfWork;
        private readonly IMemoryCache      _memoryCache;
        private readonly IRatingCalculator _ratingCalculator;


        public GameService(IMapper mapper, IUnitOfWork unitOfWork, IMemoryCache memoryCache, IRatingCalculator ratingCalculator)
        {
            _mapper           = mapper;
            _unitOfWork       = unitOfWork;
            _memoryCache      = memoryCache;
            _ratingCalculator = ratingCalculator;
        }

        public async Task<GameDto> GetByIdAsync(int id) => _mapper.Map<GameDto>(await _unitOfWork.Games.GetByIdAsync(id)) ??
                                                           throw new ResourceNotFoundException($"Game with {nameof(id)}: {id} not found");


        public async Task<IEnumerable<GameDto>> GetAllAsync() => _mapper.Map<IEnumerable<GameDto>>(await _unitOfWork.Games.GetAllAsync());

        public async Task<GameDto> CreateAsync(GameCreatingDto gameDto)
        {
            if (await DoesGameWithThisTitleExist(gameDto.Title))
            {
                throw new NameOfResourceIsTakenException($"Game with title: {gameDto.Title} already exists");
            }

            var gameForSaving = _mapper.Map<Game>(gameDto);
            await _unitOfWork.Games.CreateAsync(gameForSaving);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<GameDto>(gameForSaving);
        }

        //TODO: separate responsibilities of method
        public async Task<IEnumerable<(UserDto user, double rating)>> GetUsersWithRatingsByGameIdAsync(int gameId)
        {
            if (_memoryCache.TryGetValue($"ratings{gameId}", out IEnumerable<(UserDto user, double rating)> ratings))
                return ratings;
            ratings = await _ratingCalculator
                         .GetUsersWithRatingsByGameIdAsync(gameId, 5); // 5 is just for presentation because I won't have a lot of data
            var usersWithRatings = ratings.ToList();
            _memoryCache.Set($"ratings{gameId}", usersWithRatings,
                             new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5))); // just for demonstration purpose

            return usersWithRatings;
        }

        private async Task<bool> DoesGameWithThisTitleExist(string providedTitle) => await _unitOfWork.Games.GetByTitleAsync(providedTitle) != null;
    }
}