// Mod
using MelonLoader;

// Unity
using Il2CppSystem;
using UnityEngine;

namespace LMD_ModMenu
{
	public class ModMenu : MelonMod
	{
		public MenuManager MenuManager { get; private set; }
		KeyCode toggleMenuButton = KeyCode.M;

		public override void OnInitializeMelon()
		{
			MenuManager = MenuManager.Instance;
			MelonEvents.OnGUI.Subscribe(MenuManager.DrawAll);
		}

		public override void OnUpdate()
		{
			if (Input.GetKeyDown(toggleMenuButton))
			{
				MenuManager.ToggleDrawEnabled();
			}
		}
	}
}
