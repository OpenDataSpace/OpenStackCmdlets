using System;
using TestShell;
using System.Management.Automation;
using System.Configuration;
using NUnit.Framework;

namespace OpenStackCmdlets.Tests
{
	public class TestBase
	{
		private AppSettingsSection _appSettings;
		public AppSettingsSection AppSettings
		{
			get
			{
				if (_appSettings == null)
				{
					ExeConfigurationFileMap configMap = new ExeConfigurationFileMap();
					configMap.ExeConfigFilename = @"test.config";
					_appSettings = ConfigurationManager.OpenMappedExeConfiguration(configMap,
						ConfigurationUserLevel.None).AppSettings;
				}
				return _appSettings;
			}
		}

		public string TestAuthUrl { get { return AppSettings.Settings["authUrl"].Value; } }

		public string TestUsername { get { return AppSettings.Settings["username"].Value; } }

		public string TestPassword { get { return AppSettings.Settings["password"].Value; } }

		public string TestTenantId { get { return AppSettings.Settings["tenantId"].Value; } }

		private TestShellInterface _shell;
		public TestShellInterface Shell
		{
			get
			{
				if (_shell == null)
				{
					_shell = new TestShellInterface();
				}
				return _shell;
			}
		}

		private IOpenStackClient _openStackClient;
		public IOpenStackClient OpenStackClient
		{
			get
			{
				if (_openStackClient == null)
				{
					// Connect
					Uri authUri = new Uri (authUrl.ToString());
					var credential = new OpenStackCredential(TestAuthUrl, TestUserName, TestPassword, TestTenantId);

					var client = OpenStackClientFactory.CreateClient(credential);

					Task connectionTask = client.Connect();


				}
			}
			set
			{
				_openStackClient = value;
			}
		}

		public TestBase()
		{
			Shell.SetPreExecutionCommands(GetConnectToOpenStackCmd());
		}

		public static string CmdletName(Type cmdletType)
		{
			var attribute = System.Attribute.GetCustomAttribute(cmdletType, typeof(CmdletAttribute))
				as CmdletAttribute;
			return string.Format("{0}-{1}", attribute.VerbName, attribute.NounName);
		}

		public string GetConnectToOpenStackCmd()
		{
			return String.Format("{0} {1} {2} {3} {4};",
				CmdletName(typeof(ConnectOpenStackCmdlet)), TestAuthUrl, TestUsername,
				TestPassword, TestTenantId);
		}

	}
}

