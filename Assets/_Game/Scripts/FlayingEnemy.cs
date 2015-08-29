using UnityEngine;
using System.Collections;

public class FlayingEnemy : MonoBehaviour {

	SpriteRenderer _renderer;

	int _life;

	void Awake(){
		_renderer = GetComponent<SpriteRenderer>();
	}

	// Use this for initialization
	void Start () {

	}

	void OnAllocate(){
		_life=2;
		LeanTween.moveLocalY(gameObject,transform.position.y+2.5f,0.75f).setEase(LeanTweenType.easeInOutQuad).setLoopPingPong();
	}

	void OnCollisionEnter2D(Collision2D coll) {
		_life--;
		if(_life<0)
			Remove();
	}

	// Update is called once per frame
	void Update () {

		Vector3 screenLeft = Camera.main.ScreenToWorldPoint(new Vector3(0,0,0));

		if(transform.position.x+_renderer.bounds.size.x<screenLeft.x)
			Remove();
	}

	void Remove(){
		LeanTween.cancel(gameObject);
		ObjectPool.Instance.Release(gameObject);
	}

}
