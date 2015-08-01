using UnityEngine;
using System.Collections;

public class PlatformSegment : MonoBehaviour {
	public SpriteRenderer leftSprite;
	public SpriteRenderer rightSprite;
	public Collider2D collider2d;

	public void Reset(){
		collider2d.enabled=true;
	}

}
