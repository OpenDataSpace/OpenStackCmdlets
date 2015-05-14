using System;
using System.Threading.Tasks;
using System.Management.Automation;
using OpenStack.Compute;
using OpenStack.Network;
using OpenStack;
using System.Linq;

namespace OpenStackCmdlets
{
	[Cmdlet(VerbsCommon.Get, "Networks")]
	public class GetNetworksCmdlet : OpenStackCommandBase
	{
		protected async override Task ProcessRecordAsync()
		{
			IOpenStackClient client = this.GetOpenStackClient ();
			var networkServiceClient = client.CreateServiceClient<INetworkServiceClient>();

			var networks = await networkServiceClient.GetNetworks ();

			if (!networks.Any ()) {
				Console.WriteLine ("No Networks found");
			}
			foreach (var network in networks) {
				WriteObject (network);
			}
		}
	}
}

