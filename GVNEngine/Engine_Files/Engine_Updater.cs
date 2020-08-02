/*
Author: Garrett Fredley
Purpose: This script handles updates engine properties such as resolution, inputs, etc
 */
using System;
using System.Collections.Generic;
using GVNXMLData;

namespace GVNEngine.EngineFiles
{
    public class Engine_Updater
    {
        //Settings Variables
        public Dictionary<string, bool> defaultSettings;

        //Class References
        private Engine_Core engCoreRef;

        //Initializer Function
        public Engine_Updater(Engine_Core core)
        {
            //Record the Reference to the Engine Core
            engCoreRef = core;

            //Declare what settings the engine supports
            defaultSettings = new Dictionary<string, bool>
            {
                { "Resolution", true },
                { "EnableUserResizing", true }
            };
        }

        //Set the default settings of the engine window
        //TO-DO: Add support for post-default custom settings via the 'newSettings' argument 
        public void UpdateWindowSettings(bool setDefault, Dictionary<string, bool> newSettings)
        {
            //Load the engine properties file
            EngineProperties engProperties = engCoreRef.Content.Load<EngineProperties>("Engine_Properties");

            //If this is the initial update, use the default settings dictionary
            newSettings = defaultSettings;

            //Update each support setting
            foreach(KeyValuePair<string, bool> setting in defaultSettings)
            {
                //Only attempt to checking the setting if the value is true
                if (setting.Value == true)
                {
                    #region Resolution
                    if (setting.Key == "Resolution")
                    {
                        //Set the window resolution to the found properties
                        engCoreRef.graphics.PreferredBackBufferWidth = engProperties.Resolution[0];
                        engCoreRef.graphics.PreferredBackBufferHeight = engProperties.Resolution[1];
                        engCoreRef.graphics.ApplyChanges();
                    }
                    #endregion
                    if (setting.Key == "EnableUserResizing")
                    {
                        Console.WriteLine("To Be Implemented");
                    }
                    else
                    {
                        //User provided an unknown setting
                        Console.WriteLine("UNKNOWN SETTING ARGUMENT PROVIDED");
                    }
                }
            }


            #region Legacy XML Reader Code
            /*
            //Get the Path to the engine properties file
            string filePath = System.IO.Directory.GetCurrentDirectory();
            filePath = filePath.Replace(@"bin\Windows\x86\Debug", @"properties\") + "engine_properties.xml";

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
            */
            #endregion

        }
    }
}
