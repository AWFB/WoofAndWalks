﻿using AutoMapper;
using WoofsAndWalksAPI.DTOs;
using WoofsAndWalksAPI.Extensions;
using WoofsAndWalksAPI.Models;

namespace WoofsAndWalksAPI.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<AppUser, MemberDto>().ForMember(dest => dest.PhotoUrl,
            opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url))
            // calculate age extension method used here rather than in model for optimisation
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
        CreateMap<Photo, PhotoDto>();

        CreateMap<MemberUpdateDto, AppUser>();
        CreateMap<RegisterDto, AppUser>();
        
        CreateMap<Message, MessageDto>()
            .ForMember(dest => dest.SenderPhotoUrl,
                opt => opt.MapFrom(src => src.Sender.Photos.FirstOrDefault(x => x.IsMain).Url))
            .ForMember(dest => dest.RecipientPhotoUrl,
            opt => opt.MapFrom(src => src.Recipient.Photos.FirstOrDefault(x => x.IsMain).Url));
        
        // return correct date time format as UTC
        // Sticks a Z on the end of the string in the websocket to show its in UTC
        // important as different browsers handle datetime differently
        CreateMap<DateTime, DateTime>().ConvertUsing(d => DateTime.SpecifyKind(d, DateTimeKind.Utc));
    }
}
