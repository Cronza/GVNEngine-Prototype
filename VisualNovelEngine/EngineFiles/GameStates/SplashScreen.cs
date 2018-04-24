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
    class SplashScreen : IGame_State_Base
    {
        //Variables 
        private Engine_Core engCoreRef;
        private bool monitorVideo = false;
        VideoPlayer player;


        /// <summary>
        /// Initialization Method
        /// </summary>
        public SplashScreen(Engine_Core coreRef)
        {
            //Cast to the interface in order to call interface functions
            IGame_State_Base myInterface = this;

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
            Sprite videoTexture = new Sprite() { Texture = null, Size = new Rectangle(0, 0, 1366, 768), Pos = new Vector2(0, 0), Color = Color.White };

            //Play the splash screen video targetting the video texture sprite
            player = engCoreRef.PlayVideo("Videos/GVNEngine_Splash_Screen", videoTexture);

            //Enable monitoring of the video player
            monitorVideo = true;
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        void IGame_State_Base.Update(GameTime gameTime)
        {
            if (player.State != MediaState.Stopped)
            {
                Console.WriteLine("PLAYER HAS STOPPED - LEAVING SPLASH SCREEN STATE");
            }
        }
        /// <summary>
        /// Change the state by passing a state ID back to the state manager
        /// </summary>
        void IGame_State_Base.ChangeState()
        {

        }

        /// <summary>
        /// End the State by destroy this class
        /// </summary>
        void IGame_State_Base.EndState()
        {

        }

        /// <summary>
        /// Update the items to be drawn that were passed in by this class
        /// </summary>
        void IGame_State_Base.UpdateDrawCalls()
        {

        }
    }
}
