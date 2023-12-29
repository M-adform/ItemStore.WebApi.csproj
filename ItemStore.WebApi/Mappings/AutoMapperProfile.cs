using AutoMapper;
using ItemStore.WebApi.csproj.Models.DTOs.RequestDTOs;
using ItemStore.WebApi.csproj.Models.DTOs.ResponseDTOs;
using ItemStore.WebApi.csproj.Models.Entities;
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

            CreateMap<AddShopRequest, Shop>();

            CreateMap<Shop, GetShopResponse>();

            CreateMap<UpdateShopRequest, Shop>();
        }
    }
}