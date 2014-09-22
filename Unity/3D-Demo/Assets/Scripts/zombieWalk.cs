using UnityEngine;
using System.Collections;

public class zombieWalk : MonoBehaviour {

	private Vector3 moveDirection = Vector3.zero;
	private Animator anim;

	public GameObject attackTarget;
	public float speed;
	public float exploteRange;


	void Awake(){
		anim = GetComponent<Animator>();
	}

	// Use this for initialization
	void Start () {
		// We initialize the animation
		anim.SetBool("Explode",false);
	}
	
	// Update is called once per frame
	void Update () {
		//We ask the zombie to moove toward the player
		moveDirection = (attackTarget.transform.position - rigidbody.position).normalized;
		moveDirection *= speed;
		rigidbody.AddForce(moveDirection * Time.deltaTime);

		// We define a sphere collider around the zombie to detect collisions
		Collider[] colliders = Physics.OverlapSphere (transform.position, exploteRange);
		foreach (Collider hit in colliders){
			// If the collision is detected with the player, we start the explosion animation
			if (hit.transform.tag == "Enemies") {
				anim.SetBool("Explode",true);
				Debug.Log("The zombie entered the explosion zone");
				// We destroy the zombie when the animation ended
				StartCoroutine(destroyMe());
			}
		}
	}

	IEnumerator destroyMe (){
		yield return new WaitForSeconds(1);
		Destroy(gameObject);
	}
}
