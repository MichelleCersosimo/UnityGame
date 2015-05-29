using UnityEngine;
using System.Collections;

public class MiniJuego_BichoController : MonoBehaviour {
	public GameObject Bicho6;
	//direcciones de movimiento
	public Vector2 up = new Vector2(0f,1.5f);
	public Vector2 down = new Vector2 (0f,-1.5f);
	public Vector2 left = new Vector2(-1.5f,0f);
	public Vector2 right = new Vector2 (1.5f,0f);
	public Vector2 upleft = new Vector2(-1.5f,1.5f);
	public Vector2 downleft = new Vector2 (-1.5f,-1.5f);
	public Vector2 upright = new Vector2(1.5f,1.5f);
	public Vector2 downright = new Vector2 (-1.5f,1.5f);
	//id del bicho
	public int id = 6;
	
	
	// Use this for initialization
	void Start () {
		int dirNueva = (int)Random.Range (0,8);
		switch (dirNueva) {
		case 0 :
			GetComponent<Rigidbody2D> ().velocity = up;
			break;
		case 1:
			GetComponent<Rigidbody2D> ().velocity = down;
			break;
		case 2:
			GetComponent<Rigidbody2D> ().velocity = left;
			break;
		case 3:
			GetComponent<Rigidbody2D> ().velocity = right;
			break;
		case 4 :
			GetComponent<Rigidbody2D> ().velocity = upleft;
			break;
		case 5:
			GetComponent<Rigidbody2D> ().velocity = downleft;
			break;
		case 6:
			GetComponent<Rigidbody2D> ().velocity = upright;
			break;
		case 7:
			GetComponent<Rigidbody2D> ().velocity = downright;
			break;
		default	:
			break;
		}
	}
	
	
	void FixedUpdate () {
		
		
	}
	
	
	//cambia de direccion de macroinvertebrado al chocar con algun objeto
	void OnCollisionEnter2D (Collision2D collision) {
		int dirNueva = (int)Random.Range (0,8);
		switch (dirNueva) {
		case 0 :
			GetComponent<Rigidbody2D> ().velocity = up;
			break;
		case 1:
			GetComponent<Rigidbody2D> ().velocity = down;
			break;
		case 2:
			GetComponent<Rigidbody2D> ().velocity = left;
			break;
		case 3:
			GetComponent<Rigidbody2D> ().velocity = right;
			break;
		case 4 :
			GetComponent<Rigidbody2D> ().velocity = upleft;
			break;
		case 5:
			GetComponent<Rigidbody2D> ().velocity = downleft;
			break;
		case 6:
			GetComponent<Rigidbody2D> ().velocity = upright;
			break;
		case 7:
			GetComponent<Rigidbody2D> ().velocity = downright;
			break;
		default	:
			break;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
			if (other.gameObject.tag == "Pinza") {
			print (other);
			Destroy (this.gameObject);
		}
	}

}
