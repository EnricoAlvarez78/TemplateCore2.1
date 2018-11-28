using Microsoft.Extensions.Options;
using MyValueObjectsCollection.Common;
using Shared.Interfaces;
using System.Collections.Generic;

namespace Shared.AppSettings
{
	public class AppSettings : IAppSettings
	{
		private readonly IOptions<ConnectionStrings> _connectionStrings;
		private readonly IOptions<Settings> _settings;

		public AppSettings(IOptions<ConnectionStrings> connectionStrings, IOptions<Settings> settings)
		{
			_connectionStrings = connectionStrings;
			_settings = settings;
		}

		public ConnetionString GetConnetionString()
		{
			return new ConnetionString(_connectionStrings.Value.ToString());
		}

		public Dictionary<string, string> GetSettings()
		{
			var settingsDictionary = new Dictionary<string, string>();

			return settingsDictionary;
		}
	}
}
