using UnityEngine;
using System.Collections;
using System.Collections.Generic;       

public class PlatformsController : MonoBehaviour {

    public GameObject platformPrefab;
	public float percentForPlatformSpawn=100;

    float _numOfSegments=8;
	float _segmentsSize=2.5f;

	LinkedList<PlatformSegment> _floor = new LinkedList<PlatformSegment>();

    void Start () {
        Init ();
    }
	 
    void Init(){
        Vector3 pos = new Vector3(-9,transform.position.y,0);
		for (int i = 0; i < _numOfSegments; i++) {
			GameObject instance = Instantiate(platformPrefab);
			instance.transform.position = pos;
            instance.transform.parent = gameObject.transform;
			pos.x+=_segmentsSize;
			_floor.AddLast(instance.GetComponent<PlatformSegment>());
        }
    }
	
	void Update () {
        UpdateRoad();
	}

    void UpdateRoad(){

		PlatformSegment firstSegment = _floor.First.Value;

		Vector3 leftScreenEdge = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0));

		if(firstSegment.rightSprite.bounds.max.x<leftScreenEdge.x){

			_floor.RemoveFirst();
          	
			PlatformSegment lastSegment = _floor.Last.Value;

			Vector3 pos = lastSegment.gameObject.transform.position;
			pos.x+=_segmentsSize;

			firstSegment.gameObject.transform.position = pos;
			_floor.AddLast(firstSegment);

			firstSegment.Reset();

			if(Random.Range(0,100)>percentForPlatformSpawn)
				firstSegment.gameObject.SetActive(false);
			else
				firstSegment.gameObject.SetActive(true);

        }
    }   

}
