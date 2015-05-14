using System;
using System.Threading.Tasks;
using OpenStack.Identity;
using OpenStack;
using OpenStack.Compute;
using OpenStack.Network;
using System.Collections.Generic;
using OpenStack.Common;
using OpenStack.Common.ServiceLocation;
using System.Management.Automation;
using System.Security;

namespace OpenStackCmdlets
{
	[Cmdlet(VerbsCommunications.Connect, "OpenStack")]
	public class ConnectOpenStackCmdlet : OpenStackCommandBase
	{
		[Parameter(Mandatory=true,
			ParameterSetName = "OpenStackIdentity",
			ValueFromPipelineByPropertyName = true,
			Position = 0,
			HelpMessage = "Auth URL of OpenStack Identity Service")]
		public String authUrl { get; set; }

		[Parameter(Mandatory=true,
			ParameterSetName = "OpenStackIdentity",
			ValueFromPipelineByPropertyName = true,
			Position = 1,
			HelpMessage = "Username of your OpenStack Identity Service account")]
		public string userName { get; set; }

		[Parameter(Mandatory=true,
			ParameterSetName = "OpenStackIdentity",
			ValueFromPipelineByPropertyName = true,
			Position = 2,
			HelpMessage = "Password of your OpenStack Identity Service Account")]
		public string password { get; set; }

		[Parameter(Mandatory=true,
			ParameterSetName = "OpenStackIdentity",
			ValueFromPipelineByPropertyName = true,
			Position = 3,
			HelpMessage = "TenantId of your OpenStack Identity Service")]
		public string tenantId { get; set; }

		protected async override Task ProcessRecordAsync()
		{
			Uri authUri = new Uri (authUrl.ToString());
			var credential = new OpenStackCredential(authUri, userName, password, tenantId);

			var client = OpenStackClientFactory.CreateClient(credential);

			Task connectionTask = client.Connect();

			await connectionTask;

			if (connectionTask.IsCompleted) {
				Console.WriteLine ("Successfully connected to your OpenStack environment.");
			}

			var identityServiceClient = client.CreateServiceClient<IIdentityServiceClient> ();
			var resp = await identityServiceClient.Authenticate ();

			credential.SetAccessTokenId(resp.AccessTokenId);

			SetOpenStackClient (client);
		}
	}
}

