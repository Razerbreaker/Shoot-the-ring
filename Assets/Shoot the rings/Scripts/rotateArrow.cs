using UnityEngine;
using System.Collections;

// this class steers the arrow and its behaviour


public class rotateArrow : MonoBehaviour {

	// register collision
	bool collisionOccurred;

	// References to GameObjects gset in the inspector
	public GameObject arrowHead;
	public GameObject risingText;
	public GameObject bow;

	// Reference to audioclip when target is hit
	public AudioClip targetHit;

	// the vars realize the fading out of the arrow when target is hit
	float alpha;
	float   life_loss;
	public Color color = Color.white;

	// Use this for initialization
	void Start () {
		// set the initialization values for fading out
		float duration = 2f;
		life_loss = 1f / duration;
		alpha = 1f;
	}



	// Update is called once per frame
	void Update () {
		//this part of update is only executed, if a rigidbody is present
		// the rigidbody is added when the arrow is shot (released from the bowstring)
		if (transform.GetComponent<Rigidbody>() != null) {
			// do we fly actually?
			if (GetComponent<Rigidbody>().velocity != Vector3.zero) {
				// get the actual velocity
				Vector3 vel = GetComponent<Rigidbody>().velocity;
				// calc the rotation from x and y velocity via a simple atan2
				float angleZ = Mathf.Atan2(vel.y,vel.x)*Mathf.Rad2Deg;
				float angleY = Mathf.Atan2(vel.z,vel.x)*Mathf.Rad2Deg;
				// rotate the arrow according to the trajectory
				transform.eulerAngles = new Vector3(0,-angleY,angleZ);
			}
		}

		// if the arrow hit something...
		if (collisionOccurred) {
			// fade the arrow out
			alpha -= Time.deltaTime * life_loss;
			GetComponent<Renderer>().material.color = new Color(color.r,color.g,color.b,alpha);
			
			// if completely faded out, die:
			if (alpha <= 0f) {
				// create new arrow
				bow.GetComponent<ShootTheRing>().CreateArrow();
				// and destroy the current one
				Destroy(gameObject);
			}
		}
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "ring")
        {
            //bow.GetComponent<ShootTheRing>().setPoints(10);
        }
    }
    //
    // void OnCollisionEnter(Collision other)
    //
    // other: the other object the arrow collided with
    //


    void OnCollisionEnter(Collision other) {
		// I installed cubes as border collider outside the screen
		// If the arrow hits these objects, the player lost an arrow
		if (other.transform.name == "Cube") {
			bow.GetComponent<ShootTheRing>().CreateArrow();
			Destroy(gameObject);
		}



	}


	//
	// public void setBow
	//
	// set a reference to the main game object 

	public void setBow(GameObject _bow) {
		bow = _bow;
	}
}
