namespace YourWaifu2x.UITests
{
    public class Given_MainPage : TestBase
    {
        [Test]
        public void When_SmokeTest()
        {
            NavigateToSample("Overview");

            TakeScreenshot("Start");

            App.WaitThenTap("MaterialContainedButton");

            TakeScreenshot("Finish");
        }
    }
}
