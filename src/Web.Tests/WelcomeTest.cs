namespace TestingInNetCore.Web.Tests {
	using FluentAssertions;
	using Xunit;

	public class WelcomeTest {

		[Fact]
		public void WelcomeToXUnit() {
			true.Should().Be(true, "true should be true");
		}


	}
}
