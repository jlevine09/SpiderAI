using UnityEngine;
using System.Collections;

public class AISpider : MonoBehaviour {
	public Transform[] Waypoints;
	public int curWayPoint;
	public bool doPatrol = true;
	public Vector3 Target;
	public Vector3 MoveDirection;
	public Vector3 Velocity;
	public float Speed;
	public float fpsTargetDistance;
	public float enemyLookDistance;
	public float engageDistance;
	public float attackDistance;
	public float enemyMovementSpeed;
	public float damping;
	public Transform fpsTarget;
	Rigidbody theRigidbody;
	Renderer myRender;
	//Animator m_Animator;	//Eric's changes
	private Animation anim;


	// Use this for initialization
	void Start () {
		myRender = GetComponent<Renderer> ();
		theRigidbody = GetComponent<Rigidbody> ();
		anim = GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update () {
		// target Player 
		// Enemy Patrol
		if (curWayPoint < Waypoints.Length && doPatrol) {
			Target = Waypoints [curWayPoint].position;
			MoveDirection = Target - transform.position;
			Velocity = GetComponent<Rigidbody>().velocity;
			anim.Play ("Walk");

			if (MoveDirection.magnitude < 1) {
				curWayPoint++;
			}

			else 
			{
				Velocity = MoveDirection.normalized * Speed;
			}
		} 
		else
		{
			if (doPatrol) {
				curWayPoint = 0;
			} 
			else
			{
				transform.LookAt (new Vector3(fpsTarget.position.x, transform.position.y, fpsTarget.position.z), Vector3.up);
			}
		}
		theRigidbody.velocity = Velocity;
		transform.LookAt (Target);
		// Test patrol end

		// Enemy targeting player
		fpsTargetDistance = Vector3.Distance(fpsTarget.position, transform.position);
		if (fpsTargetDistance < enemyLookDistance) {
			doPatrol = false;
			lookAtPlayer ();
		} 

		else {
			doPatrol = true;
		}

		if (fpsTargetDistance < engageDistance) {
			attackPlayer();
			print ("attack");
		}
	}

	void lookAtPlayer(){

		//Quaternion rotation = Quaternion.LookRotation (fpsTarget.position - transform.position);
		//	transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * damping);
		transform.LookAt (new Vector3(fpsTarget.position.x, transform.position.y, fpsTarget.position.z), Vector3.up);

	}
	void attackPlayer(){


		if(fpsTargetDistance < attackDistance )
		{
			anim.Play ("Attack_Right");
		}

		else
		{
			transform.LookAt (new Vector3(fpsTarget.position.x, transform.position.y, fpsTarget.position.z), Vector3.up);
			anim.Play ("Walk");
		}
	}
}
