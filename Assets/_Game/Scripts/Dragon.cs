using UnityEngine;
using System.Collections;

public class Dragon : MonoBehaviour {

	public Transform fireSource;
	public float fireRate=1.3f;

	public GameObject bulletPrefab;

	Enemy _enemy;

	void Awake(){
		_enemy = GetComponent<Enemy>();
	}

	public void Init(){
		MoveIn();
		Hover();
	}

	void MoveIn(){
		LeanTween.moveLocalX(gameObject,transform.localPosition.x-6f,2.2f).setEase(LeanTweenType.easeOutQuad).setOnComplete(OnMoveIncomplete);
	}

	void OnMoveIncomplete(){
		InvokeRepeating("DoFire",0,fireRate);
	}

	void Hover(){
		LeanTween.moveLocalY(gameObject,transform.localPosition.y-2.5f,0.75f).setEase(LeanTweenType.easeInOutQuad).setLoopPingPong();
	}

	void DoFire(){
		GameObject go = ObjectPool.Instance.Allocate(bulletPrefab,fireSource.position,Quaternion.identity);
	}

	void OnRelease(){
		CancelInvoke();
		LeanTween.cancel(gameObject);
	}

	public bool IsAlive{
		get{
			return _enemy.IsAlive;
		}
	}
}
