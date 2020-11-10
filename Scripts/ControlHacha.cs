using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlHacha : MonoBehaviour {
	ControlOrco ctr;
	// Use this for initialization
	void Start () {
		ctr = GameObject.Find ("orc").GetComponent<ControlOrco> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.name.Equals ("arbol")) {
			ctr.setControlArbol(other.gameObject.GetComponent<ControlArbol>());
		}
	}
	
	void OnTriggerexit2D(Collider2D other){
		if(other.gameObject.name.Equals("arbol")){
			ctr.setControlArbol (null);
		}
	}

}
