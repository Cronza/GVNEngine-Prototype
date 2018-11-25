using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using VisualNovelEngine.EngineFiles.Collections;

namespace VisualNovelEngine.EngineFiles.UI.InGame
{
    class Dialogue_UI
    {
        //Variables
        private Engine_Core engCoreRef;
        private Sprite dialogueBoxArt;
        private Sprite nameBoxArt;
        private TextSprite dialogueText;
        private TextSprite nameText;
        private string dialogue;

        //Main Initialization Method
        public Dialogue_UI(Engine_Core coreRef)
        {
            //Grab References
            engCoreRef = coreRef;

            //Create the dialogue art
            dialogueBoxArt = new Sprite()
            {
                Texture = engCoreRef.Content.Load<Texture2D>("UI/Front_End/Test_Button"),
                Color = Color.White,
                Size = new Rectangle(0, (engCoreRef.GraphicsDevice.Viewport.Height / 2) + (engCoreRef.GraphicsDevice.Viewport.Height / 4), engCoreRef.GraphicsDevice.Viewport.Width, (engCoreRef.GraphicsDevice.Viewport.Height / 4))
            };
            nameBoxArt = new Sprite()
            {
                Texture = engCoreRef.Content.Load<Texture2D>("UI/Front_End/Test_Button"),
                Color = Color.White,
                Size = new Rectangle(0, dialogueBoxArt.Size.Location.Y - (dialogueBoxArt.Size.Size.Y / 4), dialogueBoxArt.Size.Size.X / 6, dialogueBoxArt.Size.Size.Y / 4)
            };

            //Create the dialogue text
            nameText = new TextSprite()
            {
                Font = engCoreRef.Content.Load<SpriteFont>("UI/Fonts/Menu_Button_01"),
                Text = "",
                Color = Color.White,
                Pos = new Vector2((nameBoxArt.Size.X + nameBoxArt.Size.Width / 2), (nameBoxArt.Size.Y + nameBoxArt.Size.Height / 2))
            };
            nameText.Origin = nameText.Font.MeasureString(nameText.Text) / 2;
            engCoreRef.textDrawStack.Add("Character_Text", nameText);

            dialogueText = new TextSprite()
            {
                Font = engCoreRef.Content.Load<SpriteFont>("UI/Fonts/Menu_Button_01"),
                Text = "",
                Color = Color.White,
                Pos = new Vector2(dialogueBoxArt.Size.X + 20, dialogueBoxArt.Size.Y + 10)
            };
            engCoreRef.textDrawStack.Add("Dialogue_Text", dialogueText);

            //Render the dialogue U.I
            engCoreRef.drawStack.Add("Dialogue_Box", dialogueBoxArt);
            engCoreRef.drawStack.Add("Name_Box", nameBoxArt);
        }
        /*
        /// <summary>
        /// Add character to the dialogue display
        /// </summary>
        public void AddCharacter(string name)
        {
            
        }
        */

    }
}
