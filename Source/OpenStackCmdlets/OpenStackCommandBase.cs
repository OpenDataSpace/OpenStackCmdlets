using System;
using System.Management.Automation;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Threading;
using OpenStack;

namespace OpenStackCmdlets
{
	public abstract class OpenStackCommandBase : PSCmdlet
	{
		public const string CLIENT_VAR_NAME = "_OPENSTACK_CLIENT";

		IOpenStackClient _openStackClient;

		protected virtual Task ProcessRecordAsync()
		{
			return Task.Delay(0);
		}

		protected sealed override void ProcessRecord()
		{
			AsyncPump.Run(async delegate {
				await ProcessRecordAsync();
			});
		}

		private void ValidateClient(IOpenStackClient client)
		{
			if (client == null) {
				throw new RuntimeException ("No OpenStack Client set. " +
				"Did you forget to connect to an OpenStack environment?");
			}
		}

		protected void SetOpenStackClient(IOpenStackClient client)
		{
			_openStackClient = client;
			SessionState.PSVariable.Set(CLIENT_VAR_NAME, _openStackClient);
		}

		public IOpenStackClient GetOpenStackClient()
		{
			var variable = SessionState.PSVariable.Get(CLIENT_VAR_NAME);
			var client = variable == null ? null : variable.Value as IOpenStackClient;
			ValidateClient(client);
			return client;
		}

	}
}

