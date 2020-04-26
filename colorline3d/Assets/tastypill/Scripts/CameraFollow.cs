using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

  public Transform target;
	public Vector3 initPosition;
	public float zOffset;
	public float xOffset;
	public float zoomPower = 5f;
	public float initFOV;
	public bool winnerOnStage = false;

public Vector3 GetUpdatedTrackPosition()
{
	return new Vector3 (target.position.x - xOffset,transform.position.y,target.position.z - zOffset);
}

 private void Start()
 {
	 initFOV = GetComponent<Camera>().fieldOfView;
 }

	void Update () {
      transform.position = GetUpdatedTrackPosition();
			if(winnerOnStage &&  GetComponent<Camera>().fieldOfView < 99){
			  GetComponent<Camera>().fieldOfView += Time.deltaTime + zoomPower;
		  }
	}

  public void StartWinAnimation()
	{
			// GetComponent<Animator>().enabled = true;
			// GetComponent<Animator>().Play("zoomOut");
	}

	public void RestartGameCamera()
	{
		 // GetComponent<Animator>().enabled = false;
	}
}
