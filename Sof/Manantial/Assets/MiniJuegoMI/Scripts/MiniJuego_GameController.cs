using UnityEngine;
using System.Collections;

public class MiniJuego_GameController : MonoBehaviour {

	public GameObject pinza;
	public GameObject Bicho1;
	public GUIText[] scores;


	// Use this for initialization
	void Start () {
		pinza = Instantiate (pinza);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
