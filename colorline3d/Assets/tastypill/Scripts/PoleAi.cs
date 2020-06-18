using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleAi : MonoBehaviour
{
    private void PlayParticles()
    {
        ParticleSystem[] particlesOnScene = FindObjectsOfType<ParticleSystem>();
        foreach (ParticleSystem ps in particlesOnScene) {
            ps.Play();
        }
    }

    private void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.tag == "playerBox") {
            if (gameObject.tag == "playerGoal") {
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

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "playerBox" && GameManager.instance.camFollow != null) {
            if (gameObject.tag == "playerGoal") {
                GameManager.instance.isGoalReached = true;
                GameManager.instance.outcome = outcome.victory;
                GameManager.instance.camFollow.winnerOnStage = true;
                GameManager.instance.chest.SetInteger("openclose", 1);
                UIManager.instance.ShowHideEndGameUI(true);
                PlayParticles();
            }
        }
    }
}
