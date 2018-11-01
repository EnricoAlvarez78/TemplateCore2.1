using System.Collections.Generic;

namespace Mvc.Models
{
	public class ManyResultsViewModel<TEntity> where TEntity : class
	{
		public long Total { get; set; } = 0;

		public IEnumerable<TEntity> Entities { get; set; } = null;
	}
}
