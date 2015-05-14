using System;
using System.Threading.Tasks;
using System.Management.Automation;
using OpenStack.Compute;
using OpenStack;

namespace OpenStackCmdlets
{
	[Cmdlet(VerbsCommon.Get, "Flavors")]
	public class GetFlavorsCmdlet : OpenStackCommandBase
	{
		protected async override Task ProcessRecordAsync()
		{
			IOpenStackClient client = this.GetOpenStackClient ();
			var computeServiceClient = client.CreateServiceClient<IComputeServiceClient>();

			var flavors = await computeServiceClient.GetFlavors();

			foreach (var flavor in flavors) {
				WriteObject (flavor);
			}
		}
	}
}

