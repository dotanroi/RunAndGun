using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class Player : MonoBehaviour {


	public GameObject bulletPrefab;
	float fireRate=0.09f;

	PlatformerCharacter2D _character2D;
	//Rigidbody2D _rigidbody2D;

	bool _isFiring=false;
	float _fireAngle=0;
	bool _isJump=false;
	bool _isDown = false;

	void Awake(){
		_character2D=GetComponent<PlatformerCharacter2D>();
		//_rigidbody2D = GetComponent<Rigidbody2D>();
	}

	void Start () {
		InvokeRepeating("DoFire",0,fireRate);
	}

	void FixedUpdate(){
		_character2D.Move(1,false,_isJump,_isDown);
		_isJump=false;
	}

	void DoFire(){
		if(_isFiring){
			ObjectPool.Instance.Allocate(bulletPrefab,transform.position,Quaternion.Euler(0,0,_fireAngle));
			//GameObject bulletGo = Instantiate(bulletPrefab,transform.position,Quaternion.Euler(0,0,_fireAngle)) as GameObject;
//			Rigidbody2D bulletBody = bulletGo.GetComponent<Rigidbody2D>();
//			Vector2 velocity = bulletBody.velocity;
//			velocity.x = _rigidbody2D.velocity.x;
//			bulletBody.velocity = velocity;
		}
	}

	public bool IsFiring {
		get {
			return _isFiring;
		}
		set {
			_isFiring = value;
		}
	}

	public float FireAngle {
		get {
			return _fireAngle;
		}
		set {
			_fireAngle = value;
		}
	}

	public bool IsJump {
		get {
			return _isJump;
		}
		set {
			_isJump = value;
		}
	}

	public bool IsDown {
		get {
			return _isDown;
		}
		set {
			_isDown = value;
		}
	}
}
