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
    public class ResultService : IResultService
    {
        private readonly IMapper     _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ResultService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper     = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> GetByIdAsync(int id) => await _unitOfWork.Results.GetByIdAsync(id) ??
                                                          throw new ResourceNotFoundException($"Result with {nameof(id)}: {id} not found");


        public async Task<IEnumerable<Result>> GetAllAsync() => await _unitOfWork.Results.GetAllAsync();

        public async Task<Result> CreateAsync(ResultDto resultDto)
        {
            var resultForSaving = MapFromDtoToResult(resultDto);
            await _unitOfWork.Results.CreateAsync(resultForSaving);
            await _unitOfWork.SaveChangesAsync();
            return resultForSaving;
        }

        private Result MapFromDtoToResult(ResultDto resultDto) => _mapper.Map<Result>(resultDto);
    }
}