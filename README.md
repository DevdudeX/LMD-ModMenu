# Mod Menu for Lonely Mountains: Downhill
This is a mod made for Melon Loader that adds a quick menu for managing a mods available functions.  
The menu is designed to be an optional dependency for any mod that wants to expose function buttons to the user in a visual format.  

## Setup Instructions
#### Preparing
Your game folder can be found by right-clicking on the game in steam and going 'Manage -> Browse local files'  
Alternatively, for other platforms\storefronts, you will simply need to navigate to your game installation folder manually.  

**It is recommended that you do not use ML v0.6.2+**  

The latest versions seem to break many of the other mods for LMD.  
Install Melon Loader **v0.6.1** to your LMD game install folder.  
Look under 'Automated Installation':  
https://melonwiki.xyz/#/  


If successful the Melon Loader splash screen should appear on launch.  
You may need to run the game once without the mod then exit.  
(See *Known Issues & Fixes* if your game freezes on quit)  

Download `LMD-ModMenu.dll` from the [Releases](https://github.com/DevdudeX/LMD-ModMenu/releases/latest) and add it to the `Mods` folder in your LMD game folder.   


#### Keybinds
| Action                                  | Keyboard & Mouse      | Gamepad                   |
| ---                                     | ---                   | ---                       |
| Toggle mod menu                         | M                     | -                         |


#### Known Issues & Fixes
- Controls are currently not rebindable.  
- Game freezes on quitting: Add the `--quitfix` [MelonLoader launch option](https://github.com/LavaGang/MelonLoader#launch-options).  
On Steam: right-click on LMD ► Properties ► Launch Options ► Paste the command (with `--` infront!).
