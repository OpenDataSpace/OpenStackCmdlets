using System;

namespace OpenStackCmdlets.Tests
{
	public class TestBaseWithConnect : TestBase
	{
		public TestBaseWithConnect () : base()
		{
			Shell.SetPreExecutionCommands(GetConnectToOpenStackCmd());
		}
	}
}

