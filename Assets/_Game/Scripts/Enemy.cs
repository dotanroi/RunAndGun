using UnityEngine;
using System.Collections;
using System;

public class Enemy : MonoBehaviour
{
	public int startLife = 3;
	public Color hitColor;

	int _life;
	SpriteRenderer _renderer;
	Color _orgColor;

	void Awake(){
		_renderer = GetComponent<SpriteRenderer>();
		_orgColor = _renderer.color;
	}

	void Start(){
		_life=startLife;
	}

	void OnAllocate(){
		_renderer.color = _orgColor;
		_life=startLife;
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		StopCoroutine("HitCoro");

		_life--;
		if(IsAlive)
			StartCoroutine("HitCoro");
		else 
			Remove();
	}

	IEnumerator HitCoro(){
		_renderer.color = hitColor;
		yield return new WaitForSeconds(0.01f);
		_renderer.color = _orgColor;
	}

	public void Remove(){
		LeanTween.cancel(gameObject);
		ObjectPool.Instance.Release(gameObject);
	}

	public bool IsAlive{
		get{
			return _life>0;
		}
	}

}

