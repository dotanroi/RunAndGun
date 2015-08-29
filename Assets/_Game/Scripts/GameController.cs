using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public Player player;
	InputController _inputController;

	public GameObject flyingEnemyPrefab;

	void Awake(){
		Application.targetFrameRate=60;
		_inputController = GetComponent<InputController>();
	}

	// Use this for initialization
	void Start () {
		StartCoroutine(CreateEnemyCoro());
	}

	IEnumerator CreateEnemyCoro(){
		while(true){


			float screenY = Random.Range(50f,Screen.height-120f);
			//Vector3 startPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width+20f,screenY,15));
			//ObjectPool.Instance.Allocate(flyingEnemyPrefab,startPoint,Quaternion.identity);
			//yield return new WaitForSeconds(0.5f);

			for (int i = 0; i < 4; i++) {
				Vector3 startPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width+20f,screenY,15));
				ObjectPool.Instance.Allocate(flyingEnemyPrefab,startPoint,Quaternion.identity);
				yield return new WaitForSeconds(0.25f);
			}
			yield return new WaitForSeconds(5f);
		}
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

}
