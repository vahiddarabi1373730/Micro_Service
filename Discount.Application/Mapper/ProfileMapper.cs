using AutoMapper;
using Discount.Application.Protos.discount.proto;
using Discount.Core.Entities;


namespace Discount.Application.Mapper;

public class ProfileMapper : Profile
{
    public ProfileMapper()
    {
        CreateMap<Coupon,CouponModel>().ReverseMap();
    }
}