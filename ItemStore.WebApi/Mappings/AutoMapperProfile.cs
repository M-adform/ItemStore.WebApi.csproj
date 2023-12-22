using AutoMapper;
using ItemStore.WebApi.Models.DTOs.RequestDTOs;
using ItemStore.WebApi.Models.DTOs.ResponseDTOs;
using ItemStore.WebApi.Models.Entities;

namespace ItemStore.WebApi.csproj.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddItemRequest, Item>();

            CreateMap<UpdateItemRequest, Item>();

            CreateMap<Item, GetItemResponse>();
        }
    }
}
