using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public Player player;
	InputController _inputController;

	void Awake(){
		Application.targetFrameRate=60;
		_inputController = GetComponent<InputController>();
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		player.IsFiring = _inputController.IsFiring;
		player.FireAngle = _inputController.CurrentAngle;
		player.IsJump = _inputController.IsJump;
	}
}
