using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Patterns.Structural.Interfaces.Facades;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.UnitsOfWork.Interfaces;
using Models.DataTransferObjects;
using Models.DataTransferObjects.Creating;
using Models.Entities;

namespace BusinessLogicLayer.Services.Implementations
{
    public class ResultService : IResultService
    {
        private readonly IMapper                 _mapper;
        private readonly IUnitOfWork             _unitOfWork;
        private readonly IUserAndGameVerificator _userAndGameVerificator;

        public ResultService(IMapper mapper, IUnitOfWork unitOfWork, IUserAndGameVerificator userAndGameVerificator)
        {
            _mapper                 = mapper;
            _unitOfWork             = unitOfWork;
            _userAndGameVerificator = userAndGameVerificator;
        }

        public async Task<ResultDto> GetByIdAsync(int id) => _mapper.Map<ResultDto>(await _unitOfWork.Results.GetByIdAsync(id)) ??
                                                             throw new ResourceNotFoundException($"Result with {nameof(id)}: {id} not found");

        public async Task<IEnumerable<ResultDto>> GetAllAsync() => _mapper.Map<IEnumerable<ResultDto>>(await _unitOfWork.Results.GetAllAsync());

        public async Task<ResultDto> CreateAsync(ResultCreatingDto resultDto)
        {
            await _userAndGameVerificator.CheckIfUserAndGameExist(resultDto.UserId, resultDto.GameId);
            var resultForSaving = _mapper.Map<Result>(resultDto);
            await _unitOfWork.Results.CreateAsync(resultForSaving);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ResultDto>(resultForSaving);
        }

        public async Task<IEnumerable<ResultDto>> GetResultsByUserIdAndGameIdAsync(int userId, int gameId)
        {
            await _userAndGameVerificator.CheckIfUserAndGameExist(userId, gameId);
            return _mapper.Map<IEnumerable<ResultDto>>(await _unitOfWork.Results.GetByConditionAsync(result => result.UserId == userId &&
                                                                                                               result.GameId == gameId));
        }
    }
}