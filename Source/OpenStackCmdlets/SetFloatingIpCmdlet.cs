using System;
using System.Threading.Tasks;
using System.Management.Automation;
using OpenStack.Compute;
using OpenStack;

namespace OpenStackCmdlets
{
	[Cmdlet(VerbsCommon.Set, "FloatingIp")]
	public class SetFloatingIpCmdlet : OpenStackCommandBase
	{
		[Parameter(Mandatory=true,
			ParameterSetName = "OpenStackServer",
			ValueFromPipelineByPropertyName = true,
			Position = 0,
			HelpMessage = "Id of the server that will be associated with the given IP address")]
		public String serverId { get; set; }

		[Parameter(Mandatory=true,
			ParameterSetName = "OpenStackServer",
			ValueFromPipelineByPropertyName = true,
			Position = 1,
			HelpMessage = "Floating IP that will be associated with the server")]
		public String floatingIp { get; set; }

		protected async override Task ProcessRecordAsync()
		{
			IOpenStackClient client = this.GetOpenStackClient ();
			var computeServiceClient = client.CreateServiceClient<IComputeServiceClient>();

			Task request = computeServiceClient.AssignFloatingIp(serverId, floatingIp);
			await request;

			if (request.IsCompleted) {
				Console.WriteLine ("Server " + serverId + " is now associated with the IP " + floatingIp);
			}
		}
	}
}

