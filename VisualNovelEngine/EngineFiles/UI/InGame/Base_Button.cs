using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using VisualNovelEngine.EngineFiles.Collections;

namespace VisualNovelEngine.EngineFiles.UI.InGame
{
    class Base_Button
    {
        //Variables
        public Sprite myArt;
        public TextSprite myText;
        public delegate void ClickEvent();
        ClickEvent myClickEvent;
        MouseState mousePre;

        //Initialization method
        public Base_Button(Sprite art, ClickEvent thing)
        {
            //Store the passed in references
            myArt = art;
            myClickEvent = thing;
        }
        //Alternative Initialization method for having text on the buttons
        public Base_Button(Sprite obj, TextSprite textObj, ClickEvent thing)
        {
            //Store the passed in references
            myArt = obj;
            myText = textObj;
            myClickEvent = thing;

            //Adjust text position and alignment
            myText.Pos = new Vector2((myArt.Size.X + myArt.Size.Width / 2), (myArt.Size.Y + myArt.Size.Height / 2));
            myText.Origin = myText.Font.MeasureString(myText.Text) / 2;
        }

        /// <summary>
        /// Update method to check cursor interaction
        /// </summary>
        public void CheckState()
        {
            //Get the mouse position and input state
            MouseState mouseCur = Mouse.GetState();
            Point cursorPos = new Point(mouseCur.X, mouseCur.Y);

            //Check if the mouse is within this buttons are
            if (myArt.Size.Contains(cursorPos))
            {
                //Check if the left mouse button was clicked in the previous frame, and released in the current frame
                if (mouseCur.LeftButton == ButtonState.Released && mousePre.LeftButton == ButtonState.Pressed)
                    myClickEvent();
            }
            //Update our previous state record so we can check again next frame
            mousePre = mouseCur;
        }
    }
}
