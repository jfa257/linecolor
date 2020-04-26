using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleAi : MonoBehaviour {


		private void OnCollisionEnter(Collision collider){
			if(collider.gameObject.tag == "playerBox"){
				if(gameObject.tag == "playerGoal"){
				 GameManager.instance.isGoalReached = true;
				} else {
				 GetComponent<Rigidbody>().isKinematic = true;
				 GameManager.instance.isDestroyed = true;
				 transform.parent.GetComponent<RotatePole>().collisioned = true;
				 GameManager.instance.KillTween();
				 UIManager.instance.ShowHideEndGameUI(true);
			 }
			}
		}

		private void OnTriggerEnter(Collider collider){
			if(collider.gameObject.tag == "playerBox" && GameManager.instance.camFollow != null){
				if(gameObject.tag == "playerGoal"){
				 GameManager.instance.isGoalReached = true;
				 GameManager.instance.outcome = outcome.victory;
				 GameManager.instance.camFollow.winnerOnStage = true;
				 UIManager.instance.ShowHideEndGameUI(true);
			}
		}
	}
}
