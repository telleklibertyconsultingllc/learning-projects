using AutoMapper;
using DTOM = EX.DTO.Model;
using ENT = EX.Entities;
using System;
using EX.Extensions;

namespace EX.Mapper.Profiles
{
    public class AuthorsProfile : Profile
    {
        public AuthorsProfile()
        {
            CreateMap<ENT.Author, DTOM.AuthorDto>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(
                    dest => dest.Age,
                    opt => opt.MapFrom(src => src.DateOfBirth.GetCurrentAge()));
        }
    }
}
