using System;
using System.Threading.Tasks;
using System.Management.Automation;
using OpenStack.Compute;
using OpenStack.Network;
using OpenStack;

namespace OpenStackCmdlets
{
	[Cmdlet(VerbsCommon.New, "FloatingIp")]
	public class NewFloatingIpCmdlet : OpenStackCommandBase
	{
		[Parameter(Mandatory=true,
			ParameterSetName = "OpenStackServer",
			ValueFromPipelineByPropertyName = true,
			Position = 0,
			HelpMessage = "Id of the Network that the Floating IP will be in")]
		public String networkId { get; set; }

		protected async override Task ProcessRecordAsync()
		{
			IOpenStackClient client = this.GetOpenStackClient ();
			var networkServiceClient = client.CreateServiceClient<INetworkServiceClient>();

			var floatingIp = await networkServiceClient.CreateFloatingIp (networkId);
			
			Console.WriteLine ("Floating IP " + floatingIp.FloatingIpAddress + " successfully created.");
			
		}
	}
}

