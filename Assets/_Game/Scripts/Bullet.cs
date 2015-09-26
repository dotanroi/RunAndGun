using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	
	//float force=3500;
	public float velocity = 200;

	SpriteRenderer _renderer;
	Rigidbody2D _rigidbody;
	bool _wasVisible;

	void Awake(){
		_renderer=GetComponent<SpriteRenderer>();
		_rigidbody=GetComponent<Rigidbody2D>();
	}

	void OnAllocate(){
		_rigidbody.velocity = transform.rotation * Vector3.right * velocity;
		_wasVisible=false;
	}

	void Update () {

		if(_wasVisible){
			if(!_renderer.isVisible)
				ObjectPool.Instance.Release(gameObject);
		}
		else if(_renderer.isVisible)
			_wasVisible=true;

	}

	void OnCollisionEnter2D(Collision2D coll) {
		ObjectPool.Instance.Release(gameObject);
	}
}