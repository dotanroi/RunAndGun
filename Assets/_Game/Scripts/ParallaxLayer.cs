using UnityEngine;
using System.Collections;

public class ParallaxLayer : MonoBehaviour {
	public int distance = 30;
	public float movespeed = 0;
	public Transform parent = null;

	Vector2 moveVectorDelta = new Vector2(0, 0);
	Vector2 moveVector = new Vector2();



	void Start () {
		if (parent == null) {
			parent = transform;
		}	
	}
	
	// Update is called once per frame
	void Update () {

		moveVector += moveVectorDelta*movespeed*Time.deltaTime;
		Vector2 existing = GetComponent<Renderer>().material.GetTextureOffset("_MainTex");
		Vector2 target = moveVector + new Vector2 (parent.position.x / distance, 0);
		//Vector2 delta = target - existing;
		GetComponent<Renderer>().material.SetTextureOffset ("_MainTex", target);
	}
}
