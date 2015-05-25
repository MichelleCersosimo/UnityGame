using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class ActionCollectMacro : PlayerAction {
	
	
	public Texture2D cursorTexture;
	ShowMenu slave; 
	GameObject player;
	GameObject target;
	bool error;
	float near_distance = 2f;
	
	Vector3 position;
	int chunkIndexX;
	int chunkIndexY;
	int tileIndexX;
	int tileIndexY;
	
	bool position_selected;
	bool invalid_position;
	bool reached_target;
	bool end;
	bool action_interrupted;

	public GameObject panel;
	public RectTransform canvasRT;
	private RectTransform panelRT;
	private Vector2 pointerOffset;
	
	public void performAction(GameObject player) {
		
		this.player = player;
		this.target = target;
		error = false;
		
		if(true) {	
			reached_target = false;
			action_interrupted = false;
			position_selected = false;
			invalid_position = false;
			end = false;
			
			StartCoroutine( SelectPosition() );
			StartCoroutine( ApproachPosition(player) );
			StartCoroutine( AnalizeTarget(player) );
			
		}
	}
	
	IEnumerator SelectPosition () {
		
		PlayerController pc = PlayerController.Player_Controller.GetComponent<PlayerController>();
		
		pc.gettingMousePositionOnWorld = true;
		Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
		
		while(pc.chunkIndexX == null) {
			yield return null;
		}
		
		Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
		
		position = pc.pointInWorld;
		chunkIndexX = (int)pc.chunkIndexX;
		chunkIndexY = (int)pc.chunkIndexY;
		tileIndexX  = (int)pc.tileIndexX;
		tileIndexY  = (int)pc.tileIndexY;
		position_selected = true;
		
		RaycastHit hit;
		Ray ray; 
		if(Input.GetMouseButtonDown(0))
		{
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray, out hit))
				//Debug.Log("Mouse Down Hit the following object: " + hit.collider.name);
			// debo llevar acabo la accion con ese collider. 
			if (hit.collider.name != "WaterSurfaceChunk(Clone)") {
				invalid_position = true;
			}
		}
		
		
		pc.clearMousePositionsOnWorld ();
		pc.gettingMousePositionOnWorld = false;
	}
	
	IEnumerator ApproachPosition (GameObject player) {
		
		while(!position_selected) {
			yield return null;
		}
		
		if (invalid_position) {
			
			Debug.Log ("Invalid position to take water sample.");
			action_interrupted = true;
			
		} else {
			//Debug.Log ("Valid position YAY!.");
			PlayerMovement pm = player.GetComponent<PlayerMovement> ();
			pm.setNewDestination (position, near_distance);
			
			while (!pm.reachedDestination && !pm.movementInterrupted) {
				yield return null;
			}
			
			
			action_interrupted = true;
			reached_target = true;
		}
	}
	
	IEnumerator AnalizeTarget (GameObject player) {
		
		PlayerController pc = PlayerController.Player_Controller.GetComponent<PlayerController>();
		
		while(!reached_target && !action_interrupted) {
			yield return null;
		}
		
		if (reached_target) { // si llegue al lugarsito donde voy a tomar la muestra
			GameController.gameController.GetComponent<ShowMenu> ().openMenuMacroSample ();
			
			end = true;
		}
		
	}
}