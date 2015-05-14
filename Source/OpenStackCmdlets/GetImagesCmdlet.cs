using System;
using System.Threading.Tasks;
using System.Management.Automation;
using OpenStack.Compute;
using OpenStack;

namespace OpenStackCmdlets
{
	[Cmdlet(VerbsCommon.Get, "Images")]
	public class GetImagesCmdlet : OpenStackCommandBase
	{
		protected async override Task ProcessRecordAsync()
		{
			IOpenStackClient client = this.GetOpenStackClient ();
			var computeServiceClient = client.CreateServiceClient<IComputeServiceClient>();

			var images = await computeServiceClient.GetImages();

			foreach (var image in images) {
				WriteObject (image);
			}
		}
	}
}

