using UnityEngine;
using System.Collections;

public class ActionStartMacroGame : MonoBehaviour {

	public static GameObject Action_Start_Macro_Game;
	GameObject player;
	bool error;
	public TogglePanelButton slave; 
	Vector3 position;
	int chunkIndexX;
	int chunkIndexY;
	int tileIndexX;
	int tileIndexY;
	
	bool position_selected;
	bool invalid_position;
	bool reached_target;
	bool planted_target;
	bool action_interrupted;


	public void startGame() {
		//Application.LoadLevel("default");  // level de juego 
	
		
	}

	public void ask(GameObject pnl,GameObject pnl2) {
		pnl.SetActive(false);
		pnl2.SetActive(true);
		//txt= GameObject.Find ("Analisis");
		//txt.GetComponent<ChangeText> ().Change();
		pnl2.transform.position = new Vector3(250, 375, 0);
		
	}

	public void analize(GameObject pnl,GameObject pnl2) {
		pnl.SetActive(false);
		pnl2.SetActive(true);
		//txt= GameObject.Find ("Analisis");
		//txt.GetComponent<ChangeText> ().Change();
		pnl2.transform.position = new Vector3(250, 375, 0);
		
	}


}
