using MelonLoader;
using UnityEngine;


namespace LMD_ModMenu
{
    public class ModMenu : MelonMod
    {
        MenuManager menuManager;
        public override void OnInitializeMelon()
        {
            menuManager = new MenuManager();
        }
        public override void OnGUI()
        {
            MelonEvents.OnGUI.Subscribe(menuManager.DrawAll);
        }

        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.M)) menuManager.toggleActive();
        }
    }
}
