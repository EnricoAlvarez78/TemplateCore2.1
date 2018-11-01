using AutoMapper;

namespace Mvc.AutoMapper
{
	public class AutoMapperConfig
	{
		public static void RegisterMappings() => Mapper.Initialize(x => 
		{
			x.AddProfile<GenericMappingProfile>();
			x.AddProfile<CreateMappingProfile>();
		});
	}
}
