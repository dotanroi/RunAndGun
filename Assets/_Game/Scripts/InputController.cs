using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {

	bool _isFiring;
	ProlongedInputValue _jump = new ProlongedInputValue();
	ProlongedInputValue _down = new ProlongedInputValue();

	float _startY;
	float _currentY;
	float _currentAngle=0;

	Vector2 _swipeFirstPressPos;


	public float rotationFactor = 0.5f;
	public float swipeSensitivity = 0.01f;

	public enum UpDownTypes{UpDownSwipe,JumpPressDownSwip,Buttons}

	public UpDownTypes upDownType = UpDownTypes.UpDownSwipe;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {

		if(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
			UpdateTouch();
		else
			UpdateMouse();
		
		if(_isFiring){
			_currentAngle=(_startY - _currentY)*rotationFactor;
		}
	}

	void FixedUpdate(){
		_jump.Update(Time.fixedTime);
		_down.Update(Time.fixedTime);
	}

	void UpdateMouse(){

		if(Input.mousePosition.x<Screen.width/2)
		{
			if(Input.GetMouseButtonDown(0)){
				_isFiring=true;
				_startY=Input.mousePosition.y;
			}
			_currentY = Input.mousePosition.y;
			
			if(Input.GetMouseButtonUp(0))
				_isFiring=false;
		}
		else 
		{

		}
		if (!IsJump)
			IsJump = Input.GetButtonDown("Jump");

		if(!IsDown)
			IsDown = Input.GetKeyDown (KeyCode.DownArrow);

	}

	void UpdateTouch(){

		for (int i = 0; i < Input.touchCount; i++) {
			Touch touch = Input.GetTouch(i);
			switch(touch.phase){
			case TouchPhase.Began:
				if(touch.position.x<Screen.width/2){
					_startY = touch.position.y;
					_isFiring=true;
				}
				else
					HandleTouchUpDownBegan(touch);
					//IsJump=true;
				break;
			case TouchPhase.Ended:
				if(touch.position.x<Screen.width/2)
					_isFiring=false;
				else
					HandleTouchUpDownEnded(touch);
				break;
			}
			if(touch.position.x<Screen.width/2)
				_currentY = touch.position.y;
		}
	}

	void HandleTouchUpDownBegan(Touch touch){
		_swipeFirstPressPos = new Vector2(touch.position.x,touch.position.y);
	}

	void HandleTouchUpDownEnded(Touch touch){
		Vector2 secondPressPos = new Vector2(touch.position.x,touch.position.y);
		Vector2 currentSwipe = new Vector3(secondPressPos.x - _swipeFirstPressPos.x, secondPressPos.y - _swipeFirstPressPos.y);
		currentSwipe.Normalize();

		switch(upDownType){
		case UpDownTypes.UpDownSwipe:
			if(currentSwipe.y > swipeSensitivity)
				IsJump=true;
			else if(currentSwipe.y < -swipeSensitivity)
				IsDown = true;
			break;
		case UpDownTypes.JumpPressDownSwip:
			if(currentSwipe.y < -swipeSensitivity)
				IsDown = true;
			else 
				IsJump=true;
			break;
		}
	}

	public bool IsFiring {
		get {
			return _isFiring;
		}
	}

	public float CurrentAngle {
		get {
			return _currentAngle;
		}
	}

	public bool IsJump {
		get {
			return _jump.IsOn;
		}
		private set{
			_jump.IsOn=value;
		}
	}

	public bool	IsDown {
		get {
			return _down.IsOn;
		}
		private set{
			_down.IsOn = value;
		}
	}
}

class ProlongedInputValue{
	bool _isOn=false;
	float _resetTime = 0;

	public void Update(float time){
		if(_resetTime>0)
			IsOn=false;
		else{
			_resetTime-=time;
		}
	}

	public bool IsOn {
		get {
			return _isOn;
		}
		set {
			if(!value){
				_isOn = false;
				_resetTime=0;
			}
			else{
				if(!_isOn){
					_isOn=true;
					_resetTime=1.5f;
				}
			}
		}
	}
}
