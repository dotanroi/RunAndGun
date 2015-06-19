using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {

	bool _isDown;
	float _startY;
	float _currentY;
	float _currentAngle=0;

	public float rotationFactor = 0.5f;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		UpdateMouse();
		if(_isDown){
			_currentAngle=(_startY - _currentY)*rotationFactor;
		}
	}

	void UpdateMouse(){
		if(Input.GetMouseButtonDown(0)){
			_isDown=true;
			_startY=Input.mousePosition.y;
		}
		_currentY = Input.mousePosition.y;

		if(Input.GetMouseButtonUp(0))
			_isDown=false;
	}



	public bool IsDown {
		get {
			return _isDown;
		}
	}

	public float CurrentAngle {
		get {
			return _currentAngle;
		}
	}
}
