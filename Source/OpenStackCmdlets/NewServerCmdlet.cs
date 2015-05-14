using System;
using System.Threading.Tasks;
using System.Management.Automation;
using OpenStack.Compute;
using OpenStack;

namespace OpenStackCmdlets
{
	[Cmdlet(VerbsCommon.New, "Server")]
	public class NewServerCmdlet : OpenStackCommandBase
	{
		[Parameter(Mandatory=true,
			ParameterSetName = "OpenStackServer",
			ValueFromPipelineByPropertyName = true,
			Position = 0,
			HelpMessage = "Name of the server that will be created")]
		public String name { get; set; }
		[Parameter(Mandatory=true,
			ParameterSetName = "OpenStackServer",
			ValueFromPipelineByPropertyName = true,
			Position = 1,
			HelpMessage = "Id of the image that will be used to create the server")]
		public String imageId { get; set; }
		[Parameter(Mandatory=true,
			ParameterSetName = "OpenStackServer",
			ValueFromPipelineByPropertyName = true,
			Position = 0,
			HelpMessage = "Id of the flavor that will be used to create the server")]
		public String flavorId { get; set; }
		[Parameter(Mandatory=true,
			ParameterSetName = "OpenStackServer",
			ValueFromPipelineByPropertyName = true,
			Position = 0,
			HelpMessage = "The network id in which the server will be created")]
		public String networkId { get; set; }
		[Parameter(Mandatory=true,
			ParameterSetName = "OpenStackServer",
			ValueFromPipelineByPropertyName = true,
			Position = 0,
			HelpMessage = "Name of the Server that will be created")]
		public string[] securityGroups { get; set; }

		protected async override Task ProcessRecordAsync()
		{
			IOpenStackClient client = this.GetOpenStackClient ();
			var computeServiceClient = client.CreateServiceClient<IComputeServiceClient>();

			var server = await computeServiceClient.CreateServer(name, imageId, flavorId, networkId, securityGroups);
			WriteObject (server);
		}
	}
}

