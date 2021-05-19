namespace YourWaifu2x.UITests
{
    public class Given_CheckBox : TestBase
    {
        [Test]
        public void When_ClickMaterial()
        {
            NavigateToSample("CheckBox");
            ShowMaterialTheme();

            TakeScreenshot("Before Checked");

            var uncheckedBox = App.WaitThenTap("Material_Unchecked").ToQueryEx();

            TakeScreenshot("After Checked");

            Assert.IsTrue(uncheckedBox.GetDependencyPropertyValue<bool>("IsChecked"));
        }
    }
}
