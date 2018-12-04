using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CanvasController : MonoBehaviour
{
	public static MainMenu mainMenu;

	public static WorldMenu worldMenu;

	void Start ()
	{
		mainMenu = GetComponent<MainMenu> () as MainMenu;
		worldMenu = GetComponent<WorldMenu> () as WorldMenu;	
	}

	public void switchMenu(int id)
	{
		mainMenu.enabled = 0 == id;
		worldMenu.enabled = 1 == id;
	}
}