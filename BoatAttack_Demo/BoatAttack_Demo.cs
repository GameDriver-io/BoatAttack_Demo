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
            api.EnableHooks(HookingObject.ALL); // New input system
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

            api.Wait(1000);
            // Change the name of the boat to GameDriver - not currently working
            // /*[@name='MainMenuUI']/*[@name='Canvas']/*[@name='Race']/*[@name='Boat']/*[@name='Options']/*[@name='MenuOption_Name']/*[@name='Text']
            //api.SetInputFieldText("(//*[@name='Text'])[3]/fn:component('TMPro.TextMeshProUGUI')", "GameDriver" );

            api.WaitForObjectValue("//*[@name='BaseButton_Race']", "active", true);
            api.ClickObject(MouseButtons.LEFT, "//*[@name='BaseButton_Race']", 30);
            api.Wait(5000);

            Assert.AreEqual("level_Island", api.GetSceneName());

        }

        [Test, Order(1)]
        public void SimpleMovementTest() // TODO: refactor and abstract
        {
            api.WaitForObjectValue("(//*[@name='text_title'])[1]", "active", true);
            api.Wait(2000);

            // Point at the checkpoints, and go
            Vector3 initialPos = api.GetObjectPosition("//boat[@name='Player 1​']");

            // Checkpoint 1
            Vector3 checkpoint1 = api.GetObjectPosition("(/Untagged[@name='checkpoint(Clone)'])[1]", CoordinateConversion.None);
            while (api.GetObjectDistance("//boat[@name='Player 1​']", "(/Untagged[@name='checkpoint(Clone)'])[1]") > 5)
            {
                api.CallMethod("//boat[@name='Player 1​']/fn:component('UnityEngine.Transform')", "LookAt", new Vector3[] { checkpoint1 });
                api.AxisPress("<Keyboard>/upArrow", 1f, (ulong)api.GetLastFPS() * 3);
                api.Wait(2000);
            }

            // Arrow marker 4
            Vector3 arrow1 = NearThing(api.GetObjectPosition("//*[@name='Arrow (4)']", CoordinateConversion.None));
            while (api.GetObjectDistance("//boat[@name='Player 1​']", "//*[@name='Arrow (4)']") > 5)
            {
                api.CallMethod("//boat[@name='Player 1​']/fn:component('UnityEngine.Transform')", "LookAt", new Vector3[] { arrow1 });
                api.AxisPress("<Keyboard>/upArrow", 1f, (ulong)api.GetLastFPS() * 2);
                api.Wait(2000);
            }

            // Checkpoint 2
            Vector3 checkpoint2 = api.GetObjectPosition("(/Untagged[@name='checkpoint(Clone)'])[2]", CoordinateConversion.None);
            while (api.GetObjectDistance("//boat[@name='Player 1​']", "(/Untagged[@name='checkpoint(Clone)'])[2]") > 5)
            {
                api.CallMethod("//boat[@name='Player 1​']/fn:component('UnityEngine.Transform')", "LookAt", new Vector3[] { checkpoint2 });
                api.AxisPress("<Keyboard>/upArrow", 1f, (ulong)api.GetLastFPS() * 3);
                api.Wait(2000);
            }

            // Arrow marker 7
            Vector3 arrow2 = NearThing(api.GetObjectPosition("//*[@name='Arrow (8)']", CoordinateConversion.None));
            while (api.GetObjectDistance("//boat[@name='Player 1​']", "//*[@name='Arrow (8)']") > 5)
            {
                api.CallMethod("//boat[@name='Player 1​']/fn:component('UnityEngine.Transform')", "LookAt", new Vector3[] { arrow2 });
                api.AxisPress("<Keyboard>/upArrow", 1f, (ulong)api.GetLastFPS() * 2);
                api.Wait(2000);
            }

            // Checkpoint 3
            Vector3 checkpoint3 = api.GetObjectPosition("(/Untagged[@name='checkpoint(Clone)'])[3]", CoordinateConversion.None);
            while (api.GetObjectDistance("//boat[@name='Player 1​']", "(/Untagged[@name='checkpoint(Clone)'])[3]") > 5)
            {
                api.CallMethod("//boat[@name='Player 1​']/fn:component('UnityEngine.Transform')", "LookAt", new Vector3[] { checkpoint3 });
                api.AxisPress("<Keyboard>/upArrow", 1f, (ulong)api.GetLastFPS() * 3);
                api.Wait(2000);
            }

            // Arrow marker 11
            Vector3 arrow3 = NearThing(api.GetObjectPosition("//*[@name='Arrow (10)']", CoordinateConversion.None));
            while (api.GetObjectDistance("//boat[@name='Player 1​']", "//*[@name='Arrow (10)']") > 5)
            {
                api.CallMethod("//boat[@name='Player 1​']/fn:component('UnityEngine.Transform')", "LookAt", new Vector3[] { arrow3 });
                api.AxisPress("<Keyboard>/upArrow", 1f, (ulong)api.GetLastFPS() * 2);
                api.Wait(2000);
            }

            // Checkpoint 4
            Vector3 checkpoint4 = api.GetObjectPosition("(/Untagged[@name='checkpoint(Clone)'])[4]", CoordinateConversion.None);
            while (api.GetObjectDistance("//boat[@name='Player 1​']", "(/Untagged[@name='checkpoint(Clone)'])[4]") > 5)
            {
                api.CallMethod("//boat[@name='Player 1​']/fn:component('UnityEngine.Transform')", "LookAt", new Vector3[] { checkpoint4 });
                api.AxisPress("<Keyboard>/upArrow", 1f, (ulong)api.GetLastFPS() * 3);
                api.Wait(2000);
            }

            // Rock
            Vector3 nearRock = NearThing(api.GetObjectPosition("(//*[@name='Rock_Small_02_LOD1'])[3]", CoordinateConversion.None));
            while (api.GetObjectDistance("//boat[@name='Player 1​']", "(//*[@name='Rock_Small_02_LOD1'])[3]") > 5)
            {
                api.CallMethod("//boat[@name='Player 1​']/fn:component('UnityEngine.Transform')", "LookAt", new Vector3[] { nearRock });
                api.AxisPress("<Keyboard>/upArrow", 1f, (ulong)api.GetLastFPS() * 2);
                api.Wait(1000);
            }

            // Start/finish checkpoint
            Vector3 checkpoint5 = api.GetObjectPosition("/Untagged[@name='checkpoint(Clone)']", CoordinateConversion.None);
            while (api.GetObjectDistance("//boat[@name='Player 1​']", "/Untagged[@name='checkpoint(Clone)']") > 5)
            {
                api.CallMethod("//boat[@name='Player 1​']/fn:component('UnityEngine.Transform')", "LookAt", new Vector3[] { checkpoint5 });
                api.AxisPress("<Keyboard>/upArrow", 1f, (ulong)api.GetLastFPS() * 3);
                api.Wait(2000);
            }

            Assert.AreNotEqual(api.GetObjectPosition("//boat[@name='Player 1​']"), initialPos, "Boat Movement Failed!");
        }

        /*
         * Not working well, need to rethink
        [Test, Order(2)]
        public void ChaseTest()
        {
            var boatName = api.GetObjectFieldValue<string>("(//*[@name='BoatHull'])[2]/../@name");
            Console.WriteLine($"The boat is named {boatName}");

            // Wait for a competitor boat to pass, then chase it
            while (api.GetObjectDistance("//boat[@name='Player 1​']", $"/boat[@name='{boatName}']") > 10)
            {
                api.Wait(500);
            }

            // Need an exit condition, i.e. "&& api.GetObjectDistance("//boat[@name='Player 1​']", "/Untagged[@name='checkpoint(Clone)']") < 4"
            while (api.GetObjectDistance("//boat[@name='Player 1​']", $"/boat[@name='{boatName}']") < 10)
            {
                api.CallMethod("//boat[@name='Player 1​']/fn:component('UnityEngine.Transform')", "LookAt", new Vector3[] { api.GetObjectPosition($"/boat[@name='{boatName}']") });
                api.AxisPress("<Keyboard>/upArrow", 1f, (ulong)api.GetLastFPS() * 1);
                api.Wait(1000);
            }
        }
        */

        [OneTimeTearDown]
        public void Disconnect()
        {
            api.DisableHooks(HookingObject.ALL);
            api.Disconnect();
        }


        public Vector3 NearThing(Vector3 thing)
        {
            Vector3 nearVector = new Vector3(thing.x, thing.y, thing.z - 5);
            return nearVector;
        }

    }
}
