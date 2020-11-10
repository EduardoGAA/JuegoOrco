using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ControlEnemigo : MonoBehaviour {
	public float vel = -1f;
	Rigidbody2D rgb;
	Animator anim;

	public Slider slider;
	public Text txt;
	public int energy = 100;

	public GameObject bulletPrototype;

	CreacionFire crf;
	AudioSource aSource;
	public AudioClip disparando;
	public AudioClip ouch;
	public AudioClip dying;
	// Use this for initialization
	void Start () {
		rgb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		aSource = GetComponent<AudioSource> ();

		energy = 100;
	}
	
	// Update is called once per frame
	void Update(){
		if (energy <= 0) {
			energy = 0;
			anim.ResetTrigger ("disparar");
			anim.ResetTrigger ("caminar");
			anim.ResetTrigger ("apuntar");
			anim.SetTrigger ("morir");
			aSource.PlayOneShot (dying);
		}
		slider.value = energy;
		txt.text = energy.ToString ();


	}

	void FixedUpdate () {
		Vector2 v = new Vector2 (vel, 0);
		rgb.velocity = v;

		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Caminando") && Random.value < 1f / (60f * 3f)) {
			anim.SetTrigger ("apuntar");
		}
		else if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Apuntando")) {
			if (Random.value < 1f / 3f) {
				anim.SetTrigger ("disparar");
			} else {
				anim.SetTrigger ("caminar");
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		flip ();
	}

	void flip(){
		vel *= -1;
		var s = transform.localScale;
		s.x *= -1;
		transform.localScale = s;
	}

	public void disparar(){
		anim.SetTrigger ("apuntar");
	}

	public void emitirBala(){
		GameObject bulletCopy = Instantiate (bulletPrototype);
		bulletCopy.transform.position = new Vector3(transform.position.x, transform.position.y, -1) ; 
		bulletCopy.GetComponent<ControlBala> ().direction = new Vector3 (transform.localScale.x, 0, 0);
		aSource.PlayOneShot (disparando);
		energy--;
	}

	public void BajarPuntosPorOrcoCerca(){
		aSource.PlayOneShot (ouch);
		energy -=15;
	}
}
