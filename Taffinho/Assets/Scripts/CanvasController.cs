using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CanvasController : MonoBehaviour
{

	/**
	 * Classe utilizada para controlar os Canvas em cena.
	 * 
	 * Todos os Canvas devem ser declarados aqui.
	 * Um objeto Static para que possam ser acessador por outros scripts pela classe.
	 * 
	 * 						IMPORTANTE PRA CARALHO!!
	 * 
	 * NÃO MEXER NESSA PORRA DEPOIS QUE ESTIVER PRONTO!!
	 * A menos que seja pra adicionar mais Canvas, aí é só seguir o padrão
	 * 
	 * 							Declarar um Canvas:
	 * 1 - Criar objeto public static
	 * 2 - No void Start() fazer o objetoUi receber via getComponent<objetoCanvas>;
	 * 3 - Adicionar o enabled = Canvas == selectedCanvas no método HideAllCanvasExcept
	 * 
	 */

//	####################################################################################################################
//												Declaração dos Canvas
//	####################################################################################################################

	public static 	MonoBehaviour selectMenu;
	public static MainMenu mainMenu;

	public static WorldMenu worldMenu;

//	####################################################################################################################
//														Start ()
//	####################################################################################################################

	void Start ()
	{

		selectMenu = GetComponent<SelectMenu> () as SelectMenu;
		mainMenu = GetComponent<MainMenu> () as MainMenu;
		worldMenu = GetComponent<WorldMenu> () as WorldMenu;

		HideAllCanvasExcept (mainMenu);
		

	}

	public void switchMenu(int id)
	{
		mainMenu.enabled = 0 == id;
		worldMenu.enabled = 1 == id;
		selectMenu.enabled = 2 == id;
	}
//	####################################################################################################################
//										Hide All Canvas Except (selectedCanvas)
//	####################################################################################################################

	/**
	 * Define todos os Canvas como FALSE, exceto o Canvas passado como Parâmetro
	 * 
	 * Canvas.enabled recebe o resultado da comparação entre ele próprio e o Canvas parâmetro.
	 * Caso a comparação seja True, o Canvas ficará ativo, e todos os outros serão desativados.
	 *
	 *
	 * 									FAVOR MANTER A IDENTAÇÃO ALINHADA
	 */

	public static void HideAllCanvasExcept (MonoBehaviour selectedCanvas) 
	{

		selectMenu.enabled 	= (selectMenu == selectedCanvas);

	}


//	####################################################################################################################
//											Set All Canvas on Object
//	####################################################################################################################

	public static void SetAllCanvasOnObject ()
	{
		

	}

}

//	####################################################################################################################
//														Anything Else ()
//	####################################################################################################################