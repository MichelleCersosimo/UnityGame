using UnityEngine;
using System.Collections;
using System;
using System.Threading;
using NUnit.Framework;

namespace ActionStartMacroGameTest {

public class ActionStartMacroGameTest {

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

		[Test]
		bool ask() {
			// SETUP
			GameObject pnl;
			pnl = GameObject.Find ("actionsMenu");

			GameObject pnl2;
			pnl2 = GameObject.Find ("AskPanel");
			ActionStartMacroGame actionStartMacroGame = ActionStartMacroGame.Action_Start_Macro_Game.GetComponent<ActionStartMacroGame>();
			actionStartMacroGame.ask (pnl,pnl2); 
			bool result = false; 

			// ASSERT
			if (pnl2.activeSelf == true) {
				result = true; 
			}

			//AssertionStripper.Equals (result,true );
			//pnl.SetActive(false);
			//pnl2.SetActive(true);
			//pnl2.transform.position = new Vector3(250, 375, 0);
			return result;
		}
		[Test]
		public void analize(GameObject pnl,GameObject pnl2) {
			pnl.SetActive(false);
			pnl2.SetActive(true);
			pnl2.transform.position = new Vector3(250, 375, 0);
			
		}


}

}
