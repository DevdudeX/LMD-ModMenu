using MelonLoader;
using UnityEngine;


namespace LMD_ModMenu
{
    public class ModMenu : MelonMod
    {
        public MenuManager menuManager { get; private set; }
        KeyCode toggleMenuButton = KeyCode.M;
        
        public override void OnInitializeMelon()
        {
            menuManager = MenuManager.Instance;
            MelonEvents.OnGUI.Subscribe(menuManager.DrawAll);
        }

        public override void OnUpdate()
        {
            if (Input.GetKeyDown(toggleMenuButton)) menuManager.toggleEnabled();
        }

    }
}
