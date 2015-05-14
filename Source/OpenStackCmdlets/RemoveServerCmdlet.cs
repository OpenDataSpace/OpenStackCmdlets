using System;
using System.Threading.Tasks;
using System.Management.Automation;
using OpenStack.Compute;
using OpenStack;

namespace OpenStackCmdlets
{
	[Cmdlet(VerbsCommon.Remove, "Server")]
	public class RemoveServerCmdlet : OpenStackCommandBase
	{
		[Parameter(Mandatory=true,
			ParameterSetName = "OpenStackServer",
			ValueFromPipelineByPropertyName = true,
			Position = 0,
			HelpMessage = "Name of the server that will be created")]
		public String id { get; set; }

		protected async override Task ProcessRecordAsync()
		{
			IOpenStackClient client = this.GetOpenStackClient ();
			var computeServiceClient = client.CreateServiceClient<IComputeServiceClient>();

			Task resp = computeServiceClient.DeleteServer(id);
			await resp;
			if (resp.IsCompleted) {
				Console.WriteLine ("Server with Id " + id + " successfully deleted.");
			}
		}
	}
}

