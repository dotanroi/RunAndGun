using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public Player player;
	InputController _inputController;

	void Awake(){
		_inputController = GetComponent<InputController>();
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		player.IsFireing = _inputController.IsDown;
		player.FireAngle = _inputController.CurrentAngle;
	}
}
