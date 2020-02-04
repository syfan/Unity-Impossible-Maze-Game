﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float speed;
	public Text countText;
	public Text winText;

    public AudioClip pickupSound;
	public AudioSource _as;

	private Rigidbody rb;
	private int count;

	void Start() {
		rb = GetComponent<Rigidbody>();
		count = 0;
		SetCountText ();
		winText.text = "";
        _as = GetComponent<AudioSource>();
	}

	void FixedUpdate() {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);


        // CHANGE THIS BACK TO A POSITIVE ONE
		rb.AddForce (movement * speed * -1);

		if(Input.GetKeyDown(KeyCode.R)){
			Application.LoadLevel (0);
		}

		if (rb.transform.position.y < 0){
			winText.text = "You win!";
			count += 1000;
			SetCountText ();
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Pickup"))
		{
			other.gameObject.SetActive (false);
			count ++;
			SetCountText();
            _as.PlayOneShot(pickupSound);
		}
	}

	void SetCountText() {
		countText.text = "Score: " + count.ToString ();
	}
}
