using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class Player : MonoBehaviour {


	public GameObject bulletPrefab;
	public float fireRate=0.25f;

	PlatformerCharacter2D _character2D;
	Rigidbody2D _rigidbody2D;

	bool _isFireing=false;
	float _fireAngle=0;

	void Awake(){
		_character2D=GetComponent<PlatformerCharacter2D>();
		_rigidbody2D = GetComponent<Rigidbody2D>();
	}

	void Start () {
		InvokeRepeating("DoFire",0,fireRate);
	}

//	void FixedUpdate(){
//		_character2D.Move(1,false,false);
//	}

	void DoFire(){
		if(_isFireing){
			GameObject bulletGo = Instantiate(bulletPrefab,transform.position,Quaternion.Euler(0,0,_fireAngle)) as GameObject;
			Rigidbody2D bulletBody = bulletGo.GetComponent<Rigidbody2D>();
			Vector2 velocity = bulletBody.velocity;
			velocity.x = _rigidbody2D.velocity.x;
			bulletBody.velocity = velocity;
		}
	}

	public bool IsFireing {
		get {
			return _isFireing;
		}
		set {
			_isFireing = value;
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
}
