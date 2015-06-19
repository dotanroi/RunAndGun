using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {


	public float rotation;
	public float force=2200;

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
		//transform.position = transform.position+transform.right*speed*Time.deltaTime;

		if(!_renderer.isVisible)
			Destroy(gameObject);

//		float rotation = transform.rotation.eulerAngles.z;
//		rotation+=Random.Range(-4,5);
//		transform.rotation = Quaternion.Euler(0,0,rotation);
	}
}