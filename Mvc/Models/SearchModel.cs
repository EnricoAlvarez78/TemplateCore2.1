using CrossLayerHelpers.Enumerators;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mvc.Models
{
	public class SearchModel
	{
		[Display(Name = "PageIndex")]
		[Range(1, int.MaxValue, ErrorMessage = "O valor do indice da página deve ser entre {1} e {2}")]
		public int? PageIndex { get; } = 0;

		[Display(Name = "PageSize")]
		[Range(1, int.MaxValue, ErrorMessage = "O valor da quantidade de items por página deve ser entre {1} e {2}")]
		public int? PageSize { get; } = 0;

		[Display(Name = "Filters")]
		public ICollection<FilterModel> Filters { get; } = new List<FilterModel>();

		[Display(Name = "Sort")]
		public Dictionary<ESortDirection, string> Sort { get; } = new Dictionary<ESortDirection, string>();

		public SearchModel(int? pageIndex, int? pageSize, ICollection<FilterModel> filters, Dictionary<ESortDirection, string> sort)
		{
			PageIndex = pageIndex;
			PageSize = pageSize;
			Filters = filters;
			Sort = sort;
		}
	}
}
