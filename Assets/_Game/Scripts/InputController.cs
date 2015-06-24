using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {

	bool _isDown;
	bool _isJump;
	float _startY;
	float _currentY;
	float _currentAngle=0;

	public float rotationFactor = 0.5f;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {

		if(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
			UpdateTouch();
		else
			UpdateMouse();
		
		if(_isDown){
			_currentAngle=(_startY - _currentY)*rotationFactor;
		}
	}

	void FixedUpdate(){
		_isJump=false;
	}

	void UpdateMouse(){

		if(Input.mousePosition.x<Screen.width/2)
		{
			if(Input.GetMouseButtonDown(0)){
				_isDown=true;
				_startY=Input.mousePosition.y;
			}
			_currentY = Input.mousePosition.y;
			
			if(Input.GetMouseButtonUp(0))
				_isDown=false;
		}
		else 
		{
			//_isJump = Input.GetMouseButtonDown(0);
		}
		if (!_isJump)
			_isJump = Input.GetButtonDown("Jump");
	}

	void UpdateTouch(){

		for (int i = 0; i < Input.touchCount; i++) {
			Touch touch = Input.GetTouch(i);
			switch(touch.phase){
			case TouchPhase.Began:
				if(touch.position.x<Screen.width/2){
					_startY = touch.position.y;
					_isDown=true;
				}
				else
					_isJump=true;
				break;
			case TouchPhase.Ended:
				if(touch.position.x<Screen.width/2)
					_isDown=false;
				break;
			}
			_currentY = touch.position.y;
		}
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

	public bool IsJump {
		get {
			return _isJump;
		}
	}
}
