using System;
using System.Threading.Tasks;
using System.Management.Automation;
using OpenStack.Compute;
using OpenStack.Network;
using OpenStack;

namespace OpenStackCmdlets
{
	[Cmdlet(VerbsCommon.Remove, "FloatingIp")]
	public class RemoveFloatingIpCmdlet : OpenStackCommandBase
	{
		[Parameter(Mandatory=true,
			ParameterSetName = "OpenStackServer",
			ValueFromPipelineByPropertyName = true,
			Position = 0,
			HelpMessage = "Id of the Floating IP")]
		public String floatingIpId { get; set; }

		protected async override Task ProcessRecordAsync()
		{
			IOpenStackClient client = this.GetOpenStackClient ();
			var networkServiceClient = client.CreateServiceClient<INetworkServiceClient>();

			Task request = networkServiceClient.DeleteFloatingIp(floatingIpId);
			await request;

			if (request.IsCompleted) {
				Console.WriteLine ("Floating IP with Id " + floatingIpId + " successfully deleted.");
			}
		}
	}
}

