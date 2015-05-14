using System;
using System.Threading.Tasks;
using System.Management.Automation;
using OpenStack.Compute;
using OpenStack;

namespace OpenStackCmdlets
{
	[Cmdlet(VerbsCommon.Get, "Servers")]
	public class GetServersCmdlet : OpenStackCommandBase
	{
		protected async override Task ProcessRecordAsync()
		{
			IOpenStackClient client = this.GetOpenStackClient ();
			var computeServiceClient = client.CreateServiceClient<IComputeServiceClient>();

			var servers = await computeServiceClient.GetServers();

			foreach (var server in servers) {
				WriteObject (server);
			}
		}
	}
}

