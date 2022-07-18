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
        ApiClient api;

        //Some test parameters and their overrides
        //static string mode = "IDE";
        static string mode = "standalone";

        static string host = "localhost";
        //static string host = "172.20.10.5";

        public string testMode = TestContext.Parameters.Get("Mode", mode);
        public string testHost = TestContext.Parameters.Get("Host", host);
        public string pathToExe = TestContext.Parameters.Get("pathToExe", null);
        

        [OneTimeSetUp]
        public void Connect()
        {
            api = new ApiClient();
            if (pathToExe != null)
            {
                ApiClient.Launch(pathToExe);
                api.Connect("localhost", 19734, false, 30);
            }
            else if (testMode == "IDE")
            {
                api.Connect("localhost", 19734, true);
            }
            api.Connect("localhost", 19734, false, 30);


            api.EnableHooks(HookingObject.ALL); // New input system
            api.Wait(3000);
        }

        [Test, Order(0)]
        public void StartGame()
        {
            //Wait for "Race Mode" category to become active, then click it
            api.WaitForObjectValue("//*[@name='Catergory Button']", "active", true);
            api.ClickObject(MouseButtons.LEFT, "//*[@name='Catergory Button']", 30);

            //Wait for "Single Player" option to become active, then click it
            api.WaitForObjectValue("//*[@name='Image_square_singleplayer']", "active", true);
            api.ClickObject(MouseButtons.LEFT, "//*[@name='Image_square_singleplayer']", 30);

            //Wait for "Next" button to become active, then click it
            api.WaitForObjectValue("//*[@name='BaseButton_Next']", "active", true);
            api.ClickObject(MouseButtons.LEFT, "//*[@name='BaseButton_Next']", 30);

            api.Wait(1000);

            //Wait for "Race" button to become active, then click it
            api.WaitForObjectValue("//*[@name='BaseButton_Race']", "active", true);
            api.ClickObject(MouseButtons.LEFT, "//*[@name='BaseButton_Race']", 30);
            api.Wait(5000);

            Assert.AreEqual("level_Island", api.GetSceneName());

        }

        [Test, Order(1)]
        public void SimpleMovementTest() 
        {
            api.WaitForObjectValue("(//*[@name='text_title'])[1]", "active", true);
            api.Wait(2000);

            // Point at the checkpoints, and go
            Vector3 initialPos = api.GetObjectPosition("//boat[@name='Player 1​']");

            // Checkpoint 1
            Vector3 checkpoint1 = new Vector3(-107, 0, 40);

            while (api.GetObjectDistance("//boat[@name='Player 1​']", "/*[@name='Checkpoint1']") > 5)
            {     
                api.CallMethod("//boat[@name='Player 1​']/fn:component('UnityEngine.Transform')", "LookAt", new Vector3[] { checkpoint1 });
                api.AxisPress("<Keyboard>/upArrow", 1f, (ulong)api.GetLastFPS() * 10);

                api.Wait((int)api.GetLastFPS() * 5);
            }
            api.CaptureScreenshot(@"C:\Users\Hunter Golden\Boat_Attack_Demo\checkpoint1.jpg", false, true);

            // Arrow marker 4
            Vector3 arrow1 = new Vector3(-75, 0, 74);

            while (api.GetObjectDistance("//boat[@name='Player 1​']", "/*[@name='Checkpoint2']") > 5)
            {
                api.CallMethod("//boat[@name='Player 1​']/fn:component('UnityEngine.Transform')", "LookAt", new Vector3[] { arrow1 });
                api.AxisPress("<Keyboard>/upArrow", 1f, (ulong)api.GetLastFPS() * 3);
                
                api.Wait((int)api.GetLastFPS() * 3);
            }

            // Checkpoint 2
            
            Vector3 checkpoint2 = new Vector3(-35, 0, 35);

            while (api.GetObjectDistance("//boat[@name='Player 1​']", "/*[@name='Checkpoint3']") > 5)
            {
                api.CallMethod("//boat[@name='Player 1​']/fn:component('UnityEngine.Transform')", "LookAt", new Vector3[] { checkpoint2 });
                api.AxisPress("<Keyboard>/upArrow", 1f, (ulong)api.GetLastFPS() * 5);
               
                api.Wait((int)api.GetLastFPS() * 5);
            }

            // Arrow marker 7
            
            Vector3 arrow2 = NearThing(api.GetObjectPosition("//*[@name='Checkpoint4']", CoordinateConversion.None));
            
            while (api.GetObjectDistance("//boat[@name='Player 1​']", "/*[@name='Checkpoint4']") > 5)
            {
                api.CallMethod("//boat[@name='Player 1​']/fn:component('UnityEngine.Transform')", "LookAt", new Vector3[] { arrow2 });
                api.AxisPress("<Keyboard>/upArrow", 1f, (ulong)api.GetLastFPS() * 5);
                
                api.Wait((int)api.GetLastFPS() * 5);
            }

            // Checkpoint 3
            
            Vector3 checkpoint3 = new Vector3(91, 0, 1);

            while (api.GetObjectDistance("//boat[@name='Player 1​']", "/*[@name='Checkpoint5']") > 5)
            {
                api.CallMethod("//boat[@name='Player 1​']/fn:component('UnityEngine.Transform')", "LookAt", new Vector3[] { checkpoint3 });
                api.AxisPress("<Keyboard>/upArrow", 1f, (ulong)api.GetLastFPS() * 5);
                
                api.Wait((int)api.GetLastFPS() * 5);
            }

            // Arrow marker 11
            
            Vector3 arrow3 = new Vector3(137, 0, -30);

            while (api.GetObjectDistance("//boat[@name='Player 1​']", "/*[@name='Checkpoint6']") > 5)
            {
                api.CallMethod("//boat[@name='Player 1​']/fn:component('UnityEngine.Transform')", "LookAt", new Vector3[] { arrow3 });
                api.AxisPress("<Keyboard>/upArrow", 1f, (ulong)api.GetLastFPS() * 5);
                
                api.Wait((int)api.GetLastFPS() * 5);
            }

            // Checkpoint 4
            
            Vector3 checkpoint4 = new Vector3(27, 0, -63);

            while (api.GetObjectDistance("//boat[@name='Player 1​']", "/*[@name='Checkpoint7']") > 5)
            {
                api.CallMethod("//boat[@name='Player 1​']/fn:component('UnityEngine.Transform')", "LookAt", new Vector3[] { checkpoint4 });
                api.AxisPress("<Keyboard>/upArrow", 1f, (ulong)api.GetLastFPS() * 5);
                
                api.Wait((int)api.GetLastFPS() * 5);
            }

            // Rock
  
            Vector3 nearRock = api.GetObjectPosition("/*[@name='Checkpoint8']", CoordinateConversion.None);
 
            while (api.GetObjectDistance("//boat[@name='Player 1​']", "/*[@name='Checkpoint8']") > 5)
            {
                api.CallMethod("//boat[@name='Player 1​']/fn:component('UnityEngine.Transform')", "LookAt", new Vector3[] { nearRock });
                api.AxisPress("<Keyboard>/upArrow", 1f, (ulong)api.GetLastFPS() * 5);
                
                api.Wait((int)api.GetLastFPS() * 5);
            }

            // Start/finish checkpoint
            
            Vector3 checkpoint5 = new Vector3(-76, 0, -30);

            while (api.GetObjectDistance("//boat[@name='Player 1​']", "/*[@name='Checkpoint9']") > 5)
            {
                api.CallMethod("//boat[@name='Player 1​']/fn:component('UnityEngine.Transform')", "LookAt", new Vector3[] { checkpoint5 });
                api.AxisPress("<Keyboard>/upArrow", 1f, (ulong)api.GetLastFPS() * 5);
                
                api.Wait((int)api.GetLastFPS() * 5);
            }

            Assert.AreNotEqual(api.GetObjectPosition("//boat[@name='Player 1​']"), initialPos, "Boat Movement Failed!");
        }


        [OneTimeTearDown]

        //Remove hooks from agent and return control to the player.
        public void Disconnect()
        {
            api.DisableHooks(HookingObject.ALL);
            api.Disconnect();
        }

        
        
        
        
        
        
        
        //helper function
        public Vector3 NearThing(Vector3 thing)
        {
            Vector3 nearVector = new Vector3(thing.x, thing.y, thing.z - 5);
            return nearVector;
        }

    }
}
