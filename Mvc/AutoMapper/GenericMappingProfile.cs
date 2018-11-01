using AutoMapper;
using CrossLayerHelpers.Enumerators;
using CrossLayerHelpers.Filters;
using Mvc.Models;
using System.Collections.Generic;
using System.Linq;

namespace Mvc.AutoMapper
{
	public class GenericMappingProfile : Profile
	{
		public GenericMappingProfile()
		{
			CreateMap<SearchModel, GetPaginatedFilter>().ConstructUsing(x =>
				new GetPaginatedFilter(x.PageIndex,
									   x.PageSize,
									   x.Filters.Select(f => new Filter(f.Field, f.Value)).ToList(),
									   x.Sort.Select(s => new KeyValuePair<string, ESortDirection>(s.Value, s.Key))
																					.ToDictionary(d => d.Key, d => d.Value)));
		}
	}
}
