using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public Player player;
	InputController _inputController;

	public GameObject flyingEnemyPrefab;
	public GameObject dragonPrefab;

	void Awake(){
		Application.targetFrameRate=60;
		_inputController = GetComponent<InputController>();

		player.OnPlayerHitEvent+=OnPlayerHit;
	}


	void Start () {
		StartCoroutine("GameLoopCoro");
	}

	IEnumerator GameLoopCoro(){
		while(true){
			yield return StartCoroutine(FlyingEnemysCoro());
			yield return new WaitForSeconds(2f);
			yield return StartCoroutine(DragonCoro());
			yield return new WaitForSeconds(2f);
		}
	}

	IEnumerator FlyingEnemysCoro(){
		float diffY = Random.Range(1f,6f);

		for (int i = 0; i < 4; i++) {
			Vector3 startPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height,15));
			startPoint.x+=1f;
			startPoint.y-=diffY;
			ObjectPool.Instance.Allocate(flyingEnemyPrefab,startPoint,Quaternion.identity);
			yield return new WaitForSeconds(0.25f);
		}
	} 

	IEnumerator DragonCoro(){

		Vector3 startPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height,15));
		startPoint.x+=3f;
		startPoint.y-=2f;

		GameObject dragonGo = ObjectPool.Instance.Allocate(dragonPrefab,startPoint,Quaternion.identity);
		dragonGo.transform.SetParent(Camera.main.transform,true);
		Dragon dragon = dragonGo.GetComponent<Dragon>();

		dragon.Init();

		while(dragon.IsAlive)
			yield return 0;

	}

	// Update is called once per frame
	void Update () {
		UpdateInput();
	}

	void UpdateInput(){
		player.IsFiring = _inputController.IsFiring;
		player.FireAngle = _inputController.CurrentAngle;
		player.IsJump = _inputController.IsJump;
		player.IsDown = _inputController.IsDown;
	}

	void OnPlayerHit ()
	{
		StopCoroutine("GameLoopCoro");
	}

	void OnDestroy(){
		player.OnPlayerHitEvent-=OnPlayerHit;
	}

}
