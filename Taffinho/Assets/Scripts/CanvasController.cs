using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CanvasController : MonoBehaviour
{

	/// <summary>
	/// Canvas Controller Class
	/// Here every canvas/menu will be declared and change it's enable attribute to true or false
	/// </summary>
	/// <param name="MainMenu">Object of Main Menu</param>
	/// <param name="WorldMenu">Object of World Menu</param>
	/// <param name="defaultMenu">Integer used to change menus at switchMenu </param>
	public static MainMenu 		mainMenu;
	public static WorldMenu 	worldMenu;
	public static int 			defaultMenu = 0;

	/// <summary>
	/// Initialization of Menu Objects
	/// Get the component of the object as it
	/// </summary>
	void Start ()
	{
		mainMenu 	= GetComponent<MainMenu> 	() as MainMenu;
		worldMenu 	= GetComponent<WorldMenu> 	() as WorldMenu;
		switchMenu	(defaultMenu);
	}

	/// <summary>
	/// Method that will change Menu/Canvas objects
	/// enable states to true/false according to it's number compared to id param
	/// </summary>
	/// <param name="id">Param used to compare itself to menu magic number</param>
	public void switchMenu(int id)
	{
		mainMenu.enabled 	= 0 	== id;
		worldMenu.enabled 	= 1 	== id;
	}
}