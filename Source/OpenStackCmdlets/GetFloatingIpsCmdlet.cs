using System;
using System.Threading.Tasks;
using System.Management.Automation;
using OpenStack.Compute;
using OpenStack.Network;
using OpenStack;
using System.Linq;

namespace OpenStackCmdlets
{
	[Cmdlet(VerbsCommon.Get, "FloatingIps")]
	public class GetFloatingIpsCmdlet : OpenStackCommandBase
	{
		protected async override Task ProcessRecordAsync()
		{
			IOpenStackClient client = this.GetOpenStackClient ();
			var networkServiceClient = client.CreateServiceClient<INetworkServiceClient>();

			var floatingIps = await networkServiceClient.GetFloatingIps ();

			if (!floatingIps.Any ()) {
				Console.WriteLine ("No Floating IPs found");
			}
			foreach (var floatingIp in floatingIps) {
				WriteObject (floatingIp);
			}
		}
	}
}

