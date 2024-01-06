using Harmony;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LMD_ModMenu
{
    public sealed class MenuManager
    {
        
        bool enabled = false;
        MainMenu menu;
        Dictionary<string,ModInfo> modInfoWindows = new Dictionary<string,ModInfo>();

        private static readonly MenuManager instance = new MenuManager();
        static MenuManager() { }
        private MenuManager()
        {
            menu = new MainMenu();
            int count = 1;
            foreach (MelonBase melon in MelonBase.RegisteredMelons)
            {
                modInfoWindows.Add(melon.Info.Name, new ModInfo(melon, count));
                count++;
            }
        }

        public void RegisterAction(string name, string buttonName, int callbackID, Action<int> callback)
        {
            modInfoWindows[name].addAction(buttonName, callbackID, callback);
        }

        public static MenuManager Instance
        {
            get
            {
                return instance;
            }
        }

        public void toggleEnabled()
        {
            enabled = !enabled;
        }
        public void DrawAll()
        {
            if (enabled)
            {
                menu.Draw();
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;


                foreach (string name in menu.getClicked())
                {
                    modInfoWindows[name].toggleEnbled();
                }

                foreach (ModInfo modInfo in modInfoWindows.Values)
                {
                    if (modInfo.enabled)
                    {
                        modInfo.Draw();
                    }
                }
            }
        }
    }
    class MainMenu
    {
        Rect windowRect = new Rect(20, 20, 500, 1000);
        List<string> clickedMods = new List<string>();
        
        MelonBase[] allMelons;
        public MainMenu()
        {
            allMelons = MelonBase.RegisteredMelons.ToArray();
        }
        public void Draw()
        {
            windowRect = GUI.Window(0, windowRect, (GUI.WindowFunction)ListMods, "Mod Menu");
            
        }

        void ListMods(int windowID)
        {
            GUILayout.BeginVertical();
            foreach (MelonBase melon in allMelons)
            {

                if (GUILayout.Button(melon.Info.Name))
                {
                    clickedMods.Add(melon.Info.Name);
                    
                }
            }
            GUILayout.EndVertical();
            GUI.DragWindow();

            
        }

        public string[] getClicked()
        {
            string[] temp = clickedMods.ToArray();
            clickedMods.Clear();
            return temp;
        }
    }

    class ModInfo
    {
        Rect windowRect = new Rect(20, 20, 500, 500);
        public bool enabled { get; private set; }
        MelonBase mod;
        int windowID;

        bool loaded = true;

        Dictionary<string, (int, Action<int>)> actions = new Dictionary<string, (int, Action<int>)>();
        public ModInfo(MelonBase melon, int windowID)
        {
            this.mod = melon;
            this.windowID = windowID;
        }
        public void toggleEnbled()
        {
            enabled = !enabled;
        }

        void toggleLoaded()
        {
            if (loaded)
            {
                mod.Unregister();
                loaded = false;
            }
            else
            {
                mod.Register();
                loaded = true;
            }
        }

        public void addAction(string buttonName, int callbackID, Action<int> callback)
        {
            actions.Add(buttonName, (callbackID, callback));
        }

        public void Draw()
        {
            if (enabled)
            {
                windowRect = GUI.Window(windowID, windowRect, (GUI.WindowFunction)ModSettings, mod.Info.Name + " " + mod.Info.Version + " by " + mod.Info.Author);
            }
        }

        void ModSettings(int windowID)
        {
            if (GUI.Button(new Rect(0, 0, 60, 20), "X"))
            {
                toggleEnbled();
            }
            GUILayout.BeginVertical();
            if (GUILayout.Button(loaded ? "disable" : "enable"))
            {
                toggleLoaded();
            }
            GUILayout.Label("Actions");
            foreach(KeyValuePair<string,(int,Action<int>)> action in actions)
            {
                if (GUILayout.Button(action.Key))
                {
                    action.Value.Item2(action.Value.Item1);
                }
            }
            GUILayout.EndVertical();
            GUI.DragWindow();
        }
    }
} 
