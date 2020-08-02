using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GVNEngine.EngineFiles.Collections;

namespace GVNEngine.EngineFiles.UI.InGame
{
    class Dialogue_UI
    {
        //Variables
        private Engine_Core engCoreRef;
        private ContentManager characterManager;
        private ContentManager backgroundManager;

        //U.I Variables
        private Sprite dialogueBoxArt;
        private Sprite nameBoxArt;
        private Sprite speakerArt;
        private Sprite backgroundArt;
        private TextSprite dialogueText;
        private TextSprite nameText;

        //Main Initialization Method
        public Dialogue_UI(Engine_Core coreRef)
        {
            //Grab References
            engCoreRef = coreRef;

            //Initialize the Content Managers 
            characterManager = new ContentManager(engCoreRef.Content.ServiceProvider);
            backgroundManager = new ContentManager(engCoreRef.Content.ServiceProvider);
            characterManager.RootDirectory = "Content";
            backgroundManager.RootDirectory = "Content";

            //Create the Dialogue Frame Art
            dialogueBoxArt = new Sprite()
            {
                Texture = engCoreRef.Content.Load<Texture2D>("UI/Front_End/Test_Button"),
                Color = Color.White,
                Size = new Rectangle(0, (engCoreRef.GraphicsDevice.Viewport.Height / 2) + (engCoreRef.GraphicsDevice.Viewport.Height / 4), engCoreRef.GraphicsDevice.Viewport.Width, (engCoreRef.GraphicsDevice.Viewport.Height / 4)),
            };
            nameBoxArt = new Sprite()
            {
                Texture = engCoreRef.Content.Load<Texture2D>("UI/Front_End/Test_Button"),
                Color = Color.White,
                Size = new Rectangle(0, dialogueBoxArt.Size.Location.Y - (dialogueBoxArt.Size.Size.Y / 4), dialogueBoxArt.Size.Size.X / 6, dialogueBoxArt.Size.Size.Y / 4)
            };

            //Create the empty, placeholder Character Art objects
            speakerArt = new Sprite()
            {
                Texture = new Texture2D(engCoreRef.GraphicsDevice, 1, 1),
                Color = Color.White,
                Size = new Rectangle(25, 25, 400, 768),
                RenderLayer = 0.1f
            };

            //Create the Background Art object
            backgroundArt = new Sprite()
            {
                Texture = backgroundManager.Load<Texture2D>("Backgrounds/Classroom_01"),
                Color = Color.White,
                Size = new Rectangle(0, 0, engCoreRef.GraphicsDevice.Viewport.Width, engCoreRef.GraphicsDevice.Viewport.Height),
                RenderLayer = 0.0f  
            };

            //Create the Dialogue Text elements
            nameText = new TextSprite()
            {
                Font = engCoreRef.Content.Load<SpriteFont>("UI/Fonts/Menu_Button_01"),
                Text = "",
                Color = Color.White,
                Pos = new Vector2((nameBoxArt.Size.X + nameBoxArt.Size.Width / 2), (nameBoxArt.Size.Y + nameBoxArt.Size.Height / 2))
            };
            nameText.Origin = nameText.Font.MeasureString(nameText.Text) / 2;
            dialogueText = new TextSprite()
            {
                Font = engCoreRef.Content.Load<SpriteFont>("UI/Fonts/Menu_Button_01"),
                Text = "",
                Color = Color.White,
                Pos = new Vector2(dialogueBoxArt.Size.X + 20, dialogueBoxArt.Size.Y + 10)
            };
            
            //Add the  dialogue U.I elements to the render stack
            engCoreRef.drawStack.Add("Dialogue_Box", dialogueBoxArt);
            engCoreRef.drawStack.Add("Name_Box", nameBoxArt);
            engCoreRef.textDrawStack.Add("Character_Text", nameText);
            engCoreRef.textDrawStack.Add("Dialogue_Text", dialogueText);
            engCoreRef.drawStack.Add("Speaker_Art", speakerArt);
            engCoreRef.drawStack.Add("Background_Art", backgroundArt);

        }

        /// <summary>
        /// Update the dialogue U.I text. Takes in a speaker name and dialogue text
        /// </summary>
        public void UpdateText(List<string> newText)
        {
            //Update the character text and the origin (so it fits centered in the name box)
            engCoreRef.textDrawStack["Character_Text"].Text = newText[0];
            nameText.Origin = nameText.Font.MeasureString(nameText.Text) / 2;

            //Update the dialogue text (Does not need to be centered)
            engCoreRef.textDrawStack["Dialogue_Text"].Text = newText[1];
        }
        
        /// <summary>
        /// Updates the rendered character art. Takes in the full name of the file to use (Relative path in the characters folder)
        /// </summary>
        /// <param name="fileName"></param>
        public void UpdateCharacterArt(string fileName)
        {
            //Texture = engCoreRef.Content.Load<Texture2D>("Backgrounds/Classroom_01"),
            //engCoreRef.drawStack["Character_Art"].Texture
            //Unload the current characters
            characterManager.Unload();

            //Load and Update the character key to point to the new character
            speakerArt.Texture = characterManager.Load<Texture2D>(fileName);


        }
    }
}
