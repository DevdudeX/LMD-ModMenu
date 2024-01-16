// Misc
using System;

// Mod
using MelonLoader;

// Unity
using UnityEngine;
using UnityEngine.UI;

using Il2CppTMPro;

// Megagon
using Il2CppMegagon.Downhill.UI;
using Il2CppMegagon.Downhill.UI.Animations;
using Il2CppMegagon.Downhill.UI.Screens;
using Il2CppMegagon.Downhill.UI.Screens.Helper;

namespace LMD_ModMenu
{
	public class MainMenuTools : MelonMod
	{
		//public MainMenuTools(IntPtr ptr) : base(ptr){}


		public GameObject CreateMainMenuButton(string name, string buttonText, Action callback)
		{
			GameObject mainMenuOptionsBtn = GameObject.Find("UI(Clone)/Canvas3D/Wrapper/MainMenuScreen(Clone)/MainMenuScreen/Layout_ButtonList/ListButton_Options");

			// Clone the existing button
			GameObject newMenuButton = GameObject.Instantiate(mainMenuOptionsBtn, mainMenuOptionsBtn.transform.parent);
			newMenuButton.name = name;
			GameObject buttonTextDisplay = newMenuButton.transform.Find("Elements/TextMeshPro Text_OptionsButton").gameObject;
			TextMeshProUGUI tmpComponent = buttonTextDisplay.GetComponent<TextMeshProUGUI>();
			tmpComponent.m_text = buttonText;
			tmpComponent.alpha = 1;

			// Update the megagon script
			newMenuButton.GetComponentInChildren<InteractiveUIElement>().field_Private_String_0 = buttonText;
			newMenuButton.GetComponentInChildren<InteractiveUIElement>().m_locaKeyString = "#MODS";	// FIXME

			// Add onClick function to open the mod menu
			buttonTextDisplay.GetComponent<Button>().onClick.AddListener(callback);

			// Destroy all Megagon scripts as I have no way of knowing how they work (thanks il2cpp)
			// Probably aren't important ¯\_(ツ)_/¯
			RemoveChildComponents<ListButton>(newMenuButton);
			RemoveChildComponents<UIAnimation_FadeContainer>(newMenuButton);

			return newMenuButton;
		}

		public GameObject CreateMainMenuScreen(string screenName, string menuName, string screenHeader, string subHeader, Action leaveScreenCallback)
		{
			GameObject mainMenuSettingsScreen = GameObject.Find("UI(Clone)/Canvas3D/Wrapper/SettingsScreen(Clone)");

			// Clone the existing menu screen
			GameObject newMenuScreen = GameObject.Instantiate(mainMenuSettingsScreen, mainMenuSettingsScreen.transform.parent);
			newMenuScreen.name = screenName;
			newMenuScreen.transform.Find("SettingsMenu").gameObject.name = menuName;

			// Remove useless scripts
			RemoveChildComponents<SettingsScreen>(newMenuScreen);
			RemoveChildComponents<ControlsDisplayAnimations>(newMenuScreen);
			RemoveChildComponents<DummyShowHideAnimation>(newMenuScreen);
			RemoveChildComponents<ScreenElement>(newMenuScreen);
			RemoveChildComponents<InteractiveUIElement>(newMenuScreen);
			RemoveChildComponents<UIAnimation_FadeContainer>(newMenuScreen);
			RemoveChildComponents<UIAnimation_MoveContainer>(newMenuScreen);
			RemoveChildComponents<TextMeshProLocalized>(newMenuScreen);

			// Set menu header text
			TextMeshProUGUI menuSubHeader_TMP = newMenuScreen.transform.Find("ModOptionsMenu/MenuHeader/TextMeshPro Text_SubHeadline").GetComponent<TextMeshProUGUI>();
			TextMeshProUGUI menuHeader_TMP = newMenuScreen.transform.Find("ModOptionsMenu/MenuHeader/TextMeshPro Text_GameMenuHeadline").GetComponent<TextMeshProUGUI>();
			menuHeader_TMP.m_text = screenHeader;
			menuSubHeader_TMP.m_text = subHeader;

			// Setup back button
			TextMeshProUGUI backBtn_TextDisp = newMenuScreen.transform.Find("ModOptionsMenu/BackButton/Image_ControlsExplanationLeftBackground/TextMeshPro Text_Back").GetComponent<TextMeshProUGUI>();
			backBtn_TextDisp.alpha = 1;
			backBtn_TextDisp.m_text = "Back";	// It seems to default to {0}Back (guessing it's localization related)

			// Disable apply button (FIXME?)
			GameObject applyBtn = newMenuScreen.transform.Find("ModOptionsMenu/ApplyButton/Image_ControlsExplanationRightBackground (1)/TextMeshPro Text_Apply").gameObject;
			applyBtn.SetActive(false);


			// Find and hide existing screen menu buttons
			string btnNav = "ModOptionsMenu/Layout_Categories/";
			GameObject categoryBtn_Game = newMenuScreen.transform.Find(btnNav+"CategoryButton_Game/TextMeshPro Text_Display").gameObject;
			GameObject categoryBtn_Display = newMenuScreen.transform.Find(btnNav+"CategoryButton_Display/TextMeshPro Text_Display").gameObject;
			GameObject categoryBtn_Graphics = newMenuScreen.transform.Find(btnNav+"CategoryButton_Graphics/TextMeshPro Text_Display").gameObject;
			GameObject categoryBtn_Controls = newMenuScreen.transform.Find(btnNav+"CategoryButton_Controls/TextMeshPro Text_Display").gameObject;
			GameObject categoryBtn_Audio = newMenuScreen.transform.Find(btnNav+"CategoryButton_Audio/TextMeshPro Text_Display").gameObject;
			categoryBtn_Game.SetActive(false);
			categoryBtn_Display.SetActive(false);
			categoryBtn_Graphics.SetActive(false);
			categoryBtn_Controls.SetActive(false);
			categoryBtn_Audio.SetActive(false);




			// Add onClick function to return to the main menu
			backBtn_TextDisp.GetComponent<Button>().onClick.AddListener((UnityEngine.Events.UnityAction)leaveScreenCallback);

			// Hide the mod menu screen by default
			newMenuScreen.SetActive(false);

			return newMenuScreen;
		}



		/// <summary>
		/// Generic method to remove all child components of a certain type.
		/// Credit: https://onewheelstudio.com/blog/2020/12/27/c-generics-and-unity
		/// </summary>
		/// <param name="parent">The parent GameObject.</param>
		/// <typeparam name="T">The component type to remove.</typeparam>
		public void RemoveChildComponents<T>(GameObject parent) where T : Component
		{
			T[] components = parent.GetComponentsInChildren<T>();
			foreach (var c in components) {
				GameObject.Destroy(c);
			}
		}
	}
}