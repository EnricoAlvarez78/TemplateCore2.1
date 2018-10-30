using MyValueObjectsCollection.Common;
using System.Collections.Generic;

namespace Shared.Interfaces
{
	public interface IAppSettings
	{
		ConnetionString GetConnetionString();

		Dictionary<string, string> GetSettings();
	}
}
