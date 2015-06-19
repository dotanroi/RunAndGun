using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {


	public GameObject bulletPrefab;
	public float fireRate=0.25f;

	bool _isFireing=false;
	float _fireAngle=0;


	Rigidbody2D _rigidbody;

	void Awake(){
		_rigidbody=GetComponent<Rigidbody2D>();
	}

	// Use this for initialization
	void Start () {
		InvokeRepeating("DoFire",0,fireRate);
	}

	void DoFire(){
		if(_isFireing){
			GameObject bulletGo = Instantiate(bulletPrefab,transform.position,Quaternion.Euler(0,0,_fireAngle)) as GameObject;
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
