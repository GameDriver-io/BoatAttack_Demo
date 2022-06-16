using System;
using NUnit.Framework;
using gdio.unity_api;
using gdio.unity_api.v2;
using gdio.common.objects;

namespace BoatAttack_Demo
{
    [TestFixture]
    public class Tests
    {
        ApiClient api = new ApiClient();

        [OneTimeSetUp]
        public void Connect()
        {
            api.Connect("localhost", 19734, true);
            api.EnableHooks(HookingObject.MOUSE); // New input system
            /*
            api.LoggedMessage += (s, e) =>
            {
                Console.WriteLine(e.Message);
            };
            */
            api.Wait(3000);
        }

        [Test, Order(0)]
        public void StartGame()
        {
            api.WaitForObjectValue("//*[@name='Catergory Button']", "active", true);
            api.ClickObject(MouseButtons.LEFT, "//*[@name='Catergory Button']", 30);
            api.WaitForObjectValue("//*[@name='Image_square_singleplayer']", "active", true);
            api.ClickObject(MouseButtons.LEFT, "//*[@name='Image_square_singleplayer']", 30);

            //Check the number of levels, and increase it to 3
            // Check (should be 1) --> (//*[@name='Option'])[1]/fn:component('TMPro.TextMeshProUGUI')/@text
            // Click --> (//*[@name='Right'])[1]
            // Check again (should be 3) --> (//*[@name='Option'])[1]/fn:component('TMPro.TextMeshProUGUI')/@text

            api.WaitForObjectValue("//*[@name='BaseButton_Next']", "active", true);
            api.ClickObject(MouseButtons.LEFT, "//*[@name='BaseButton_Next']", 30);

            // Change the name of the boat to GameDriver
            // /*[@name='MainMenuUI']/*[@name='Canvas']/*[@name='Race']/*[@name='Boat']/*[@name='Options']/*[@name='MenuOption_Name']/*[@name='Text']

            api.WaitForObjectValue("//*[@name='BaseButton_Race']", "active", true);
            api.ClickObject(MouseButtons.LEFT, "//*[@name='BaseButton_Race']", 30);
            api.Wait(5000);

            Assert.AreEqual("level_Island", api.GetSceneName());

        }

        [Test, Order(1)]
        public void MovementTest()
        {
            api.WaitForObjectValue("(//*[@name='text_title'])[1]", "active", true);
            api.Wait(10000);

            //Input test - Not currently working with AxisPress, ButtonPress, or KeyPress
            api.ButtonPress("Keyboard/upArrow", 3000, 10);
            api.Wait(5000);
        }


        [OneTimeTearDown]
        public void Disconnect()
        {
            api.Disconnect();
        }
    }
}
