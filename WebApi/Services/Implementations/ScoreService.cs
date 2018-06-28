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
    public class ScoreService : IScoreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper     _mapper;

        public ScoreService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper     = mapper;
        }

        public async Task<Score> GetByIdAsync(int id)
        {
            var score = await _unitOfWork.Scores.GetByIdAsync(id);
            if (score == null)
            {
                throw new ResourceNotFoundException($"Score with id: {id} not found");
            }

            return score;
        }

        public async Task<IEnumerable<Score>> GetAllAsync() => await _unitOfWork.Scores.GetAllAsync();

        public async Task<Score> CreateAsync(ScoreDto scoreDto)
        {
            var score = MapFromDtoToScore(scoreDto);
            await _unitOfWork.Scores.CreateAsync(score);
            await _unitOfWork.SaveChangesAsync();
            return score;
        }

        // private methods

        private Score MapFromDtoToScore(ScoreDto scoreDto) => _mapper.Map<Score>(scoreDto);
    }
}