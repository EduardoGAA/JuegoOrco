using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlBala : MonoBehaviour {

	public float speed = 6;
	public float lifetime = 2;
	public Vector3 direction = new Vector3 (-1, 0, 0);

	Vector3 stepVector;
	Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, lifetime);
		rb = GetComponent<Rigidbody2D> ();
		stepVector = speed * direction.normalized;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		rb.velocity = stepVector*-1;
	}

	private void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.name.Equals ("arbol")) {
			ControlArbol ctr = other.gameObject.GetComponent<ControlArbol> ();
			if (ctr != null)
				ctr.recibirDisparo ();
			Destroy (gameObject);
		}
		if (other.gameObject.name.Equals ("orc")) {
			ControlOrco ctr = other.gameObject.GetComponent<ControlOrco> ();
			if (ctr != null)
				ctr.recibirBala ();
			Destroy (gameObject);
		}
	}
}
