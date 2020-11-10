using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlArbol : MonoBehaviour {
	public int numGolpesParaCaer = 3;
	public int numBalasParaCaer = 3;

	Animator anim;
	SpriteRenderer rend;

	AudioSource aSource;
	public AudioClip arbolCayendo;
	public AudioClip impactoBala;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		rend = GetComponent<SpriteRenderer> ();
		aSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool golpeOrco(){
		bool resp = false;
		numGolpesParaCaer--;
		if (numGolpesParaCaer <= 0) {
			anim.SetTrigger ("caer");
			resp = true;
			aSource.PlayOneShot (arbolCayendo);
		}
		return resp;
	}

	public bool recibirDisparo(){
		bool resp = false;
		numBalasParaCaer--;
		aSource.PlayOneShot (impactoBala);
		switch (numBalasParaCaer) {
		case 2:
			rend.color = new Color (1f / 242, 1f / 155, 1f / 155, 1f);
			break;
		case 1:
			rend.color = new Color (1f / 216, 1f / 10, 1f / 10);
			break;	
		}
		if (numBalasParaCaer <= 0) {
			anim.SetTrigger ("caer");
			resp = true;
			aSource.PlayOneShot (arbolCayendo);
		}
		return resp;
	}
}
