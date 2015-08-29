using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	
	//float force=3500;
	public float velocity = 200;

	SpriteRenderer _renderer;
	Rigidbody2D _rigidbody;

	void Awake(){
		_renderer=GetComponent<SpriteRenderer>();
		_rigidbody=GetComponent<Rigidbody2D>();
	}

	void OnAllocate(){
		_rigidbody.velocity = transform.rotation * Vector3.right * velocity;
		//_rigidbody.AddForce(transform.rotation * Vector3.right * force);
	}

	void Update () {

		if(!_renderer.isVisible)
			ObjectPool.Instance.Release(gameObject);
			//Destroy(gameObject);

	}

	void OnCollisionEnter2D(Collision2D coll) {
		ObjectPool.Instance.Release(gameObject);
		//Destroy(gameObject);
	}
}