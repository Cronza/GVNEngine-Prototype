using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Xml;

namespace VisualNovelEngine.EngineFiles
{
    class Engine_Updater
    {
        //Initialize Property Variables
        public Dictionary<string, List<string>> settings;
        public string[] resolution;

        //Class References
        EngineCore engCore;

        //Initializer Function
        public Engine_Updater(EngineCore core)
        {
            //Record the Reference to the Engine Core
            engCore = core;

            //Declare what settings the engine supports
            settings = new Dictionary<string, List<string>>
            {
                { "Resolution", new List<string>() }
            };
        }

        //Set the default settings of the engine window || Update the window settings
        //TO-DO: Add support for custom settings via the 'newSettings' argument 
        public void UpdateWindowSettings(bool setDefault, Dictionary<string, List<string>> newSettings)
        {
            //Get the Path to the engine properties file
            string filePath = System.IO.Directory.GetCurrentDirectory();
            filePath = filePath.Replace(@"bin\DesktopGL\AnyCPU\Debug", @"properties\") + "engine_properties.xml";

            //Create the XML Reader
            XmlReader reader;
            reader = XmlReader.Create(filePath);

            //If the setDefault param is used, initialize values based on the engine_properties.xml
            if(setDefault)
            { 
                //While the XMLReader parses the file...
                while (reader.Read())
                {
                    //If the 'Resolution' node is found
                    if (reader.Name == "Resolution" && reader.NodeType != XmlNodeType.EndElement)
                    {
                        //Retrieve the 'Width' and 'height' properties of the 'Resolution' Node, then record the found values
                        List<string> tempRes = new List<string>();
                        Console.WriteLine(reader.Name);
                        reader.ReadToFollowing("width");
                        tempRes.Add(reader.ReadInnerXml());
                        reader.ReadToFollowing("height");
                        tempRes.Add(reader.ReadInnerXml());
                        settings[reader.Name] = tempRes;

                        //Set the window resolution to the found properties
                        engCore.graphics.PreferredBackBufferWidth = Int32.Parse(tempRes[0]);
                        engCore.graphics.PreferredBackBufferHeight = Int32.Parse(tempRes[1]);
                        engCore.graphics.ApplyChanges();
                    }
                }
            }
        }
    }
}
