

using Il2CppMegagon.Downhill.UI;
using MelonLoader;
using UnityEngine;

namespace LMD_ModMenu
{
    class MenuManager
    {
        KeyCode toggleMenuButton = KeyCode.M;
        bool active = false;
        MainMenu menu;
        public MenuManager()
        {
            menu = new MainMenu();
        }
        public void toggleActive()
        {
            active = !active;
        }
        public void DrawAll()
        {
            if (active)
            {
                menu.Draw();
                Cursor.visible = true;
            }
        }
    }
    class MainMenu
    {
        Rect windowRect = new Rect(20, 20, 500, 1000);
        public void Draw()
        {
            windowRect = GUI.Window(0, windowRect, (GUI.WindowFunction)ListMods, "Mod Menu");
        }

        void ListMods(int windowID)
        {
            GUILayout.BeginVertical();
            foreach (MelonBase melon in MelonBase.RegisteredMelons)
            {
                if (GUILayout.Button(melon.Info.Name))
                {

                }
            }
            GUILayout.EndVertical();
            GUI.DragWindow();
        }
    }
} 
