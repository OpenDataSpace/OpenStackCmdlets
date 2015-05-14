using System;
using NUnit.Framework;
using System.Management.Automation;

namespace OpenStackCmdlets.Tests
{
	[TestFixture]
	public class NewServerTests : TestBase
	{
		public readonly static string NewServerCmd = "New-Server ";
		public readonly static string GetServersCmd = "Get-Server ";

		[Test]
		public void CreateNewServer()
		{
			var resNewServer = Shell.Execute(NewServerCmd + "\"test_server\" + " +
				"\"image-id\" +" +
				"\"network-id\" +" +
				"\"securityGroup\"");
			//var resGetServer = Shell.Execute (GetServerCmd + resNewServer[0].id));

			Assert.That(resNewServer, Is.Not.Null);
		}
	}
}

