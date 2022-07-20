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
        static string mode = "IDE";
        //static string mode = "standalone";

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
            else api.Connect("localhost", 19734, false, 30);


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

        Vector3 checkpoint1 = new Vector3(-107, 0, 40);
        Vector3 checkpoint2 = new Vector3(-35, 0, 35);
        Vector3 checkpoint3 = new Vector3(91, 0, 1);
        Vector3 checkpoint4 = new Vector3(27, 0, -63);
        Vector3 checkpoint5 = new Vector3(-76, 0, -30);
       
        [Test, Order(1)]
        public void SimpleMovementTest()
        {
           
            api.WaitForObject("//boat[@name='Player 1​']");
            Vector3 initialPos = api.GetObjectPosition("//boat[@name='Player 1​']");
            
            api.WaitForObject("/*[@name='Race_Canvas(Clone)']/*[@name='Gameplay']/*[@name='Speedometer']");
            api.Wait(7000);   
            
            api.SetObjectFieldValue("/*[@name='Player 1​']/*[@name='NavmeshCarver']/fn:component('UnityEngine.AI.NavMeshObstacle')", "enabled", false);
            
            //Checkpoint1
            api.NavAgentMoveToPoint("//boat[@name='Player 1​']", checkpoint1, true);
            api.SetObjectFieldValue("//boat[@name='Player 1​']/fn:component('UnityEngine.AI.NavMeshAgent')", "speed", 10);
            while (api.GetObjectDistance("//boat[@name='Player 1​']", "/*[@name='Checkpoint1']") > 1)
            {
                api.Wait(500);
            }
            
            //Checkpoint2
            api.NavAgentMoveToPoint("//boat[@name='Player 1​']", checkpoint2, true);
            while (api.GetObjectDistance("//boat[@name='Player 1​']", "/*[@name='Checkpoint2']") > 1)
            {
                api.Wait(1000);
            }

            //Checkpoint3
            api.NavAgentMoveToPoint("//boat[@name='Player 1​']", checkpoint3, true);
            while (api.GetObjectDistance("//boat[@name='Player 1​']", "/*[@name='Checkpoint3']") > 1)
            {
                api.Wait(1000);
            }

            //Checkpoint4
            api.NavAgentMoveToPoint("//boat[@name='Player 1​']", checkpoint4, true);
            while (api.GetObjectDistance("//boat[@name='Player 1​']", "/*[@name='Checkpoint4']") > 1)
            {
                api.Wait(1000);
            }

            //Checkpoint5
            api.NavAgentMoveToPoint("//boat[@name='Player 1​']", checkpoint5, true);
            while (api.GetObjectDistance("//boat[@name='Player 1​']", "/*[@name='Checkpoint5']") > 1)
            {
                api.Wait(1000);
            }

            //Assert.AreNotEqual(api.GetObjectPosition("//boat[@name='Player 1​']"), initialPos, "Boat Movement Failed!");

        }

        [OneTimeTearDown]

        //Remove hooks from agent and return control to the player.
        public void Disconnect()
        {
            api.DisableHooks(HookingObject.ALL);
            api.Disconnect();
        }

    }
}
