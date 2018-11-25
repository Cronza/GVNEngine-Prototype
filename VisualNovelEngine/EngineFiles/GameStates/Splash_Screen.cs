using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualNovelEngine.EngineFiles.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace VisualNovelEngine.EngineFiles.GameStates
{
    class Splash_Screen : IGame_State_Base
    {
        //Variables 
        private Engine_Core engCoreRef;
        IGame_State_Base myInterface;
        VideoPlayer player;


        /// <summary>
        /// Initialization Method
        /// </summary>
        public Splash_Screen(Engine_Core coreRef)
        {
            //Cast to the interface in order to call interface functions
            myInterface = this;

            //Retain a reference to the Engine_Core
            engCoreRef = coreRef;
            
            //Run the core state function
            Console.WriteLine("Activating Splash Screen!");
            myInterface.Main();
        }

        /// <summary>
        /// The main function for the state
        /// </summary>
        void IGame_State_Base.Main()
        {
            //Create the video texture sprite to be rendered
            Sprite videoTexture = new Sprite() { Texture = null, Size = new Rectangle(0, 0, 1366, 768), Color = Color.White };

            //Play the splash screen video targetting the video texture sprite
            player = engCoreRef.PlayVideo("Videos/GVNEngine_Splash_Screen", "Splash_Video", videoTexture);

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        void IGame_State_Base.Update(GameTime gameTime)
        {
            if (player.State == MediaState.Stopped)
            {
                //Go to the Front-End state
                Console.WriteLine("PLAYER HAS STOPPED - LEAVING SPLASH SCREEN STATE");
                myInterface.ChangeState(Engine_State_Manager.GameState.FrontEnd);
            }
        }
        /// <summary>
        /// Change the state by passing a state ID back to the state manager
        /// </summary>
        void IGame_State_Base.ChangeState(Engine_State_Manager.GameState newState)
        {
            //Clean up the created objects before transitioning
            myInterface.CleanUp();

            //Called the state managers update method, giving it the state code
            engCoreRef.stateManager.UpdateGameState(newState);    
        }

        /// <summary>
        /// Update the items to be drawn that were passed in by this class
        /// </summary>
        void IGame_State_Base.UpdateDrawCalls()
        {

        }

        /// <summary>
        /// Destroys all objects created during the execution of this state
        /// </summary>
        void IGame_State_Base.CleanUp()
        {
            engCoreRef.RemoveDrawItem("Splash_Video", "video");
            player.Stop();
        }
    }
}
