using System;
using System.Threading.Tasks;
using System.Management.Automation;
using OpenStack.Compute;
using OpenStack;

namespace OpenStackCmdlets
{
	[Cmdlet(VerbsLifecycle.Restart, "Server")]
	public class RestartServerCmdlet : OpenStackCommandBase
	{
		[Parameter(Mandatory=true,
			ParameterSetName = "OpenStackServer",
			ValueFromPipelineByPropertyName = true,
			Position = 0,
			HelpMessage = "Id of the server that you want to restart")]
		public String serverId { get; set; }

		[Parameter(Mandatory=true,
			ParameterSetName = "OpenStackServer",
			ValueFromPipelineByPropertyName = true,
			Position = 1,
			HelpMessage = "The type of reboot that you want to perform (HARD or SOFT)")]
		public String rebootType { get; set; }

		protected async override Task ProcessRecordAsync()
		{
			IOpenStackClient client = this.GetOpenStackClient ();
			var computeServiceClient = client.CreateServiceClient<IComputeServiceClient>();

			Task resp = computeServiceClient.RebootServer(serverId, rebootType);
			await resp;
			if (resp.IsCompleted) {
				Console.WriteLine ("Server with Id " + serverId + " successfully rebooted.");
			}
		}
	}
}

