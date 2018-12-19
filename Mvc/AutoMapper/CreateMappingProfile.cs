using AutoMapper;
using Domain.Entities;
using Mvc.Areas.AccessControl.ViewModels;
using System.Linq;

namespace Mvc.AutoMapper
{
	public class CreateMappingProfile : Profile
	{
		public CreateMappingProfile()
		{
			CreateMap<User, UserViewModel>().ReverseMap();
		}
	}
}
