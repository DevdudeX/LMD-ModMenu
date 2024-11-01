// Mod
using MelonLoader;
using LonelyMountains_ModMenu;

// Unity
using Il2CppSystem;
using UnityEngine;

[assembly: MelonInfo(
	typeof(ModMenu),
	name:"Mod Menu",
	version:"0.0.3",
	author:"redish2098, DevdudeX",
	downloadLink:"github.com/DevdudeX/LM-ModMenu"
)]
[assembly: MelonGame("Megagon Industries")]
namespace LonelyMountains_ModMenu
{
	public class ModMenu : MelonMod
	{
		// Keep this updated!
		private const string MOD_VERSION = "0.0.3";

		public MenuManager MenuManager { get; private set; }
		private KeyCode toggleMenuButton = KeyCode.M;
		private KeyCode closeMenuButton = KeyCode.Escape;

		public override void OnInitializeMelon()
		{
			MenuManager = MenuManager.Instance;
			MelonEvents.OnGUI.Subscribe(MenuManager.DrawAll);
			MelonEvents.OnGUI.Subscribe(DrawVersionText, 100);
		}

		public override void OnUpdate()
		{
			if (Input.GetKeyDown(toggleMenuButton))
			{
				MenuManager.ToggleVisibility();
			}

			if (MenuManager.IsVisible && Input.GetKeyDown(closeMenuButton))
			{
				MenuManager.SetVisibility(false);
			}
		}

		private static void DrawVersionText()
		{
			GUI.Label(new Rect(20, 8, 1000, 200), "<b><color=white><size=15>Mod Menu v"+ MOD_VERSION +"</size></color></b>");
		}
	}
}
