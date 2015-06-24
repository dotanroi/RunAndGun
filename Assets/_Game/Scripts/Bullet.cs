using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	
	float force=3500;

	SpriteRenderer _renderer;
	Rigidbody2D _rigidbody;

	void Awake(){
		_renderer=GetComponent<SpriteRenderer>();
		_rigidbody=GetComponent<Rigidbody2D>();
	}

	// Use this for initialization
	void Start () {
		_rigidbody.AddForce(transform.rotation * Vector3.right * force);
	}

	void Update () {

		if(!_renderer.isVisible)
			Destroy(gameObject);

	}
}