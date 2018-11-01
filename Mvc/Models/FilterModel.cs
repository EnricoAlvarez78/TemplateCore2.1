using System.ComponentModel.DataAnnotations;

namespace Mvc.Models
{
	public class FilterModel
	{
		[Display(Name = "Field")]
		public string Field { get; }

		[Display(Name = "Value")]
		public string Value { get; }

		public FilterModel(string field, string value)
		{
			Field = field;
			Value = value;
		}
	}
}
