using UnityEngine;
using System.Collections;

public class FlayingEnemy : MonoBehaviour {

	SpriteRenderer _renderer;

	Enemy _enemy;

	void Awake(){

		_enemy = GetComponent<Enemy>();

		_renderer = GetComponent<SpriteRenderer>();
	}

	void OnAllocate(){
		LeanTween.moveLocalY(gameObject,transform.position.y-2.5f,0.75f).setEase(LeanTweenType.easeInOutQuad).setLoopPingPong();
	}

	// Update is called once per frame
	void Update () {

		Vector3 screenLeft = Camera.main.ScreenToWorldPoint(new Vector3(0,0,0));

		if(transform.position.x+_renderer.bounds.size.x<screenLeft.x)
			_enemy.Remove();
	}

}
