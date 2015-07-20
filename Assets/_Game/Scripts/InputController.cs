using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {

	bool _isDown;
	bool _isJump;
	float _startY;
	float _currentY;
	float _currentAngle=0;

	float _resetJumpTime = 0;

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
		if(_resetJumpTime>0)
			IsJump=false;
		else{
			_resetJumpTime-=Time.fixedTime;
		}

		
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

		}
		if (!IsJump)
			IsJump = Input.GetButtonDown("Jump");
	}

	void Jump(bool isJump){

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
					IsJump=true;
				break;
			case TouchPhase.Ended:
				if(touch.position.x<Screen.width/2)
					_isDown=false;
				break;
			}
			if(touch.position.x<Screen.width/2)
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
		private set{
			if(!value){
				_isJump = false;
				_resetJumpTime=0;
			}
			else{
				if(!_isJump){
					_isJump=true;
					_resetJumpTime=1.5f;
				}
			}
		}
	}
}
