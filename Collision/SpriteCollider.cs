using UnityEngine;
using System.Collections;

public class SpriteCollider : MonoBehaviour {

	public Texture2D collisionMask;
	public bool pixelCollision;

	void Awake () {
		if (collisionMask == null) {
			collisionMask = GetComponent<SpriteRenderer>().sprite.texture;
		}
	}

	public bool collidesWith(SpriteCollider other) {
		if (pixelCollision) {
			return Collision2D.collides(this,other);
		}
		return getBounds().overlaps(other.getBounds());
	}

	public Bounds2D getBounds () {
		return new Bounds2D(
			(int)transform.position.x - collisionMask.width * (int)transform.localScale.x/2,
			(int)transform.position.y - collisionMask.height * (int)transform.localScale.y/2,
			collisionMask.width * (int)transform.localScale.x,
			collisionMask.height * (int)transform.localScale.y);
	}
}

