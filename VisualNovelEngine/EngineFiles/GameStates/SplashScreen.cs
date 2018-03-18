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
    class SplashScreen : IGameStateBase
    {
        //Variables 
        private Engine_Core engCoreRef;
        //Video video;
        //VideoPlayer player;


        /// <summary>
        /// Initialization Method
        /// </summary>
        public SplashScreen(Engine_Core coreRef)
        {
            //Cast to the interface in order to call interface functions
            IGameStateBase myInterface = this;

            //Retain a reference to the Engine_Core
            engCoreRef = coreRef;
            
            //Run the core state function
            Console.WriteLine("Activating Splash Screen!");
            myInterface.Main();
        }

        /// <summary>
        /// The main function for the state
        /// </summary>
        void IGameStateBase.Main()
        {

            //Add the Engine Logo to the draw stack
            //Sprite logo = new Sprite() {Texture = engCoreRef.Content.Load<Texture2D>("Engine_Art/GVNEngine_Logo_Light"), Size = new Rectangle(0, 0, 1366, 768), Pos = new Vector2(0, 0), Color = Color.White };
            //engCoreRef.drawStack.Add(logo);

            //video = engCoreRef.Content.Load<Video>("Videos/GVNEngine_Splash_Screen");
            //player = new VideoPlayer();

            //Playing Video
            //Console.WriteLine("Playing Splash Screen Video");
            //player.Play(video);

            Sprite videoTexture = new Sprite() { Texture = null, Size = new Rectangle(0, 0, 1366, 768), Pos = new Vector2(0, 0), Color = Color.White };

            //if (player.State != MediaState.Stopped)
            //{ 
            engCoreRef.PlayVideo("Videos/GVNEngine_Splash_Screen", videoTexture);

            //}


        }

        /// <summary>
        /// Change the state by passing a state ID back to the state manager
        /// </summary>
        void IGameStateBase.ChangeState()
        {

        }

        /// <summary>
        /// End the State by destroy this class
        /// </summary>
        void IGameStateBase.EndState()
        {

        }

        /// <summary>
        /// Update the items to be drawn that were passed in by this class
        /// </summary>
        void IGameStateBase.UpdateDrawCalls()
        {

        }
    }
}
