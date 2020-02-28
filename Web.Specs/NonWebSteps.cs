using TechTalk.SpecFlow;
using NUnit.Framework;
using System.Diagnostics;

namespace Web.Specs
{
    [Binding]
    public class NonWebSteps
    {
        [Given("I get an error code of -1")]
        public void GivenIGetAnErrorCode()
        {
            using (Process process = new Process())
            {
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.UseShellExecute = true;
                process.Start();
                process.WaitForExit(1);
            }
        }

        [Given("I want to test parsing")]
        public void GivenIWantToTestParsing()
        {
            Assert.That(1, Is.EqualTo(1));
        }
    }
}
