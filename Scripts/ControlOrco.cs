using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ControlOrco : MonoBehaviour {
	Rigidbody2D rgb; 
	Animator anim;
	public float maxVel = 5f;
	bool haciaDerecha = true;

	public Slider slider;
	public Text txt;

	public int energy = 100;
	ControlArbol ctrArbol = null;
	public GameObject hacha = null;


	public int costoGolpeAlAire = 1;
	public int costoGolpeAlArbol = 3;
	public int premioArbol = 15;
	public int costoBala = 20;

	bool enFire1 = false;

	public bool jumping = false;
	public float yJumpForce = 350;
	Vector2 jumpForce;
	public bool isOnTheFloor = false;

	AudioSource aSource;
	public GameObject retroalimentacionEnergiaPrefab;
	Transform retroalimentacionSpawnPoint;

	public AudioClip cortandoUnArbol;
	public AudioClip ouch;
	public AudioClip dying;

	// Use this for initialization
	void Start () {
		rgb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		aSource = GetComponent<AudioSource> ();
		hacha = GameObject.Find ("/orc/orc_body/orc _R_arm/orc _R_hand/orc_weapon");
		energy = 100;
		retroalimentacionSpawnPoint = GameObject.Find ("spawnPoint").transform;
		jumpForce = new Vector2 (0, 0);
		rgb.freezeRotation = true;
	}

	void Update(){
		if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("Muriendo")) {
			if (energy <= 0) {
				energy = 0;
				anim.SetTrigger ("morir");
				aSource.PlayOneShot (dying);
			}
		} else
			return;
		
		if (Mathf.Abs (Input.GetAxis ("Fire1")) > 0.01f) {
			if (enFire1 == false) {
				enFire1 = true;
				hacha.GetComponent<CircleCollider2D> ().enabled = false;
				anim.SetTrigger ("attack");
				if (ctrArbol != null) {
					if (ctrArbol.golpeOrco ()) {
						IncrementarEnergia(premioArbol);
						//energy += premioArbol;
						if (energy > 100)
							energy = 100;
					} else {
						//energy -= costoGolpeAlArbol;
						IncrementarEnergia(costoGolpeAlArbol * -1);
						aSource.PlayOneShot (cortandoUnArbol);
					}
				} else {
					//energy -= costoGolpeAlAire;
					IncrementarEnergia(costoGolpeAlAire * -1);
				}
			}
		} else {
			enFire1 = false;
		}
		if (energy < 0)
			energy = 0;
		slider.value = energy;
		txt.text = energy.ToString ();
	}

	private void IncrementarEnergia(int incremento) {
		energy += incremento;
		InstanciarRetroalimentacionEnergia(incremento);
	}
	private void InstanciarRetroalimentacionEnergia(int incremento) {
		GameObject retroalimentcionGO = null;
		if (retroalimentacionSpawnPoint!=null)
			retroalimentcionGO = (GameObject)Instantiate(retroalimentacionEnergiaPrefab, retroalimentacionSpawnPoint.position, retroalimentacionSpawnPoint.rotation);
		else
			retroalimentcionGO = (GameObject)Instantiate(retroalimentacionEnergiaPrefab, transform.position, transform.rotation);

		retroalimentcionGO.GetComponent<RetroalimentacionEnergia>().cantidadCambiodeEnergia = incremento;
	}

	private void verificarInputParaSaltar(){
		isOnTheFloor = rgb.velocity.y == 0;

		if (Input.GetAxis ("Jump") > 0.01f) {
			if (!jumping && isOnTheFloor) {
				jumping = true;
				jumpForce.x = 0f;
				jumpForce.y = yJumpForce;
				rgb.AddForce (jumpForce);
			}
		} else {
			jumping = false;
		}
	}

	public void habilitarTriggerHacha(){
		hacha.GetComponent<CircleCollider2D> ().enabled = true;
	}

	void FixedUpdate () {
		if (energy > 0) {
			verificarInputParaCaminar();
			verificarInputParaSaltar();   
		}
	}

	private void verificarInputParaCaminar() {
		float h = Input.GetAxis("Horizontal");
		Vector2 vel = new Vector2(0, rgb.velocity.y);
		h *= maxVel;
		vel.x = h;
		rgb.velocity = vel;
		//Debug.DrawLine(transform.position,
			//transform.position + new Vector3(vel.x, vel.y));
		anim.SetFloat ("speed", vel.x);

		if (haciaDerecha && h < 0) 
			flip ();
		if (!haciaDerecha && h > 0) 
			flip ();
		if(!haciaDerecha && h < 0)
			anim.SetFloat ("speed", (vel.x)*-1);
	}

	void flip(){
		var s = transform.localScale;
		s.x *= -1;
		transform.localScale = s;
		haciaDerecha = !haciaDerecha;
	}

	public void setControlArbol(ControlArbol ctr){
		ctrArbol = ctr;
	}

	public void recibirBala(){
		//energy -= costoBala;
		IncrementarEnergia(costoBala * -1);
		aSource.PlayOneShot (ouch);
		//aSource.PlayOneShot (ouch);
	}
}
