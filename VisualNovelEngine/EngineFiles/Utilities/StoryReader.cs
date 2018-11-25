/*
Author: Garrett Fredley
Purpose: 
 */

using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using VisualNovelEngine.EngineFiles.Collections;

namespace VisualNovelEngine.EngineFiles.Utilities
{
    class StoryReader
    {
        /// <summary>
        /// Deserializes the json file of the provided name and returns the generated object
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public StoryChapter GenerateStoryChapter(string fileName)
        {
            //Deserialize the json file of the provided name
            Console.WriteLine("Deserializing " + fileName + "...");
            StoryChapter newObj = JsonConvert.DeserializeObject<StoryChapter>(File.ReadAllText("C:\\Users\\garre\\Desktop\\Scripts\\GVNEngine\\VisualNovelEngine\\Content\\Story_Data\\" + fileName));

            //Return the generated object
            return newObj;
        }
    }
}
