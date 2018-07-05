using System;
using AutoMapper;
using Models.DataTransferObjects;
using Models.Entities;
using Models.Enumerations;

namespace BusinessLogicLayer.Mappings.Resolvers
{
    public class StringToGameOutcomeResolver : IValueResolver<ResultDto, Result, GameOutcome>
    {
        public GameOutcome Resolve(ResultDto source, Result destination, GameOutcome destMember, ResolutionContext context)
        {
            if (!Enum.TryParse(typeof(GameOutcome), source.GameOutcome, out var parsedResult))
            {
                return GameOutcome.Defeat;
            }

            return (GameOutcome) parsedResult;
        }
    }
}