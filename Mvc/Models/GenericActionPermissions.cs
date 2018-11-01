using System.Collections.Generic;

namespace Mvc.Models
{
	public class GenericActionPermissions
	{
		public ICollection<KeyValuePair<EGenericAction, string>> PermissionList { get; private set; } = new List<KeyValuePair<EGenericAction, string>>();

		public void Add(EGenericAction genericAction, string operation) => PermissionList.Add(new KeyValuePair<EGenericAction, string>(genericAction, operation));
	}
}
