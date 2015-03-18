using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public GameObject obstaclePrefab;
	public delegate void OnObstablePutDelegate();
	public event OnObstablePutDelegate onObstaclePut;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			PutObstacle();
		}
	}

	void PutObstacle()
	{
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 50f, LayerMask.GetMask ("Ground"))) {
			Vector3 pos = hit.point + new Vector3(0f, 2f, 0f);
			GameObject obj = Instantiate(obstaclePrefab, pos, Quaternion.identity) as GameObject;
			AstarPath.active.UpdateGraphs (obj.collider.bounds);
			if(onObstaclePut != null)
				onObstaclePut();
		}
	}
}
