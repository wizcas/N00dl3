using UnityEngine;
using System.Collections;
using Pathfinding;

public class AStarAI : MonoBehaviour {
	public Vector3 targetPosition;
	public Path path;
	public float speed = 100f;
	public float nextWaypointDistance = 3f;

	private Seeker seeker;
	private CharacterController characterController;
	private GameController gameController;
	private int currentWaypoint = 0;

	// Use this for initialization
	void Start () {
		seeker = GetComponent<Seeker> ();
		characterController = GetComponent<CharacterController> ();
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		gameController.onObstaclePut += Scan;
		Scan ();
	}

	void Scan ()
	{
		seeker.StartPath (transform.position, targetPosition, OnPathComplete);
	}
	
	void OnPathComplete(Path p)
	{
		Debug.Log ("Path back successfully proceeded.");
		Debug.Log ("Errors? " + p.error);
		if (!p.error) {
			path = p;
			currentWaypoint = 0;
		}
	}

	void FixedUpdate()
	{
		if (path == null)
			return;

		if (currentWaypoint >= path.vectorPath.Count) {
			Debug.Log ("End of path is reached");
			return;
		}

		Vector3 dir = (path.vectorPath [currentWaypoint] - transform.position).normalized;
		dir *= speed * Time.fixedDeltaTime;
		characterController.SimpleMove (dir);

		if (Vector3.Distance (transform.position, path.vectorPath [currentWaypoint]) < nextWaypointDistance) {
			currentWaypoint++;
			return;
		}
	}
}
