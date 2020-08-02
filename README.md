# GVNEngine-Prototype
The GVNEngine is a visual novel engine built using the C# Monogame framework, which wraps the old XNA framework. This repo was used as an early prototyping grounds for developing the engine

![ScreenShot](README_Files/GVNEngine_Game_01.png?raw=true "GVNEditor")

## Feature List
### State Manager
- Capable of handling switching between various 'States', which represented types of scenes
- Supported the following states:
    - Splash Screen
    - Front-End / Main Menu
    - Dialogue / Game

### Dialogue System
- Used a pre-defined XML schema written in it's own project. This allowed flexible dialogue authoring for the end-user
