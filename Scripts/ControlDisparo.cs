using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlDisparo : MonoBehaviour {
	Collider2D disparandoA = null;
	public float probabilidadDeDisparo = 1f;

	ControlEnemigo ctr;

	// Use this for initialization
	void Start () {
		ctr = GameObject.Find ("enemigo").GetComponent<ControlEnemigo> ();
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.name.Equals ("arbol") && disparandoA == null) {
			decidaSiDispara (other);
		}
		if (other.gameObject.name.Equals ("orc") && disparandoA == null) {
			disparar ();
			disparandoA = other;
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if(other == disparandoA){
			disparandoA = null;
		}
	}

	void decidaSiDispara(Collider2D other){
		if (Random.value < probabilidadDeDisparo) {
			disparar ();
			disparandoA = other;
		}
	}

      void disparar(){
		ctr.disparar ();
	}
	// Update is called once per frame

}
