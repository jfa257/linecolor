using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum outcome
{
    undecided, victory, defeat
}
public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    public pathStorage storage;
    public Animator boxAnimator;
    public Transform boxDT;
    public CameraFollow camFollow;
    public Vector3[] pathArray;
    public bool createPathPoints = false;
    public PathType pathType = PathType.CatmullRom;
    public float tweenDuration = 1;
    public bool useDT = true;
    public Tween t;
    public bool isDestroyed = false;
    public bool isGoalReached = false;
    public outcome outcome = outcome.undecided;
    public Vector3 initBoxPosition;
    public Quaternion initRotation;
    public TrailRenderer trail;
    public Animator chest;
    public Vector3 chestInitPos;
    public Quaternion chestInitRot;

    private void Awake()
    {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        chestInitPos = chest.transform.position;
        chestInitRot = chest.transform.rotation;
        if (createPathPoints) {
            StartCoroutine(StorePathPoints());
        } else {
            initBoxPosition = boxDT.position;
            initRotation = boxDT.rotation;
            ExecutePath();
        }
    }

    private void Update()
    {
        if (!createPathPoints && !isDestroyed) {
            UIManager.instance.UpdatePercentage();
            if (Input.GetMouseButtonUp(0) && !isGoalReached) {
                PauseTween();
            } else if (Input.GetMouseButton(0) || isGoalReached) {
                PlayTween();
            }
        }
    }

    public void RestartGameState()
    {
        Debug.Log("RESTARTING GAME");
        isDestroyed = false;
        isGoalReached = false;
        camFollow.winnerOnStage = false;
        boxDT.position = initBoxPosition;
        boxDT.rotation = initRotation;
        trail.Clear();
        trail.enabled = false;
        camFollow.GetComponent<Camera>().fieldOfView = camFollow.initFOV;
        UIManager.instance.RestartGameUI();
        chest.SetInteger("openclose", 0);
        //chest.transform.position = chestInitPos;
        //chest.transform.rotation = chestInitRot;
        ResetPoles();
        RestartTween();
        outcome = outcome.undecided;
    }

    public void ResetPoles()
    {
        PoleAi[] poles = FindObjectsOfType<PoleAi>();
        foreach (PoleAi ai in poles) {
            ai.GetComponent<Rigidbody>().isKinematic = false;
            if (ai.gameObject.tag != "playerGoal") {
                ai.transform.parent.GetComponent<RotatePole>().collisioned = false;
            }
        }
    }

    public void RestartTween()
    {
        KillTween();
        ExecutePath();
    }

    public void PauseTween()
    {
        boxDT.transform.DOPause();
    }
    public void KillTween()
    {
        t.Kill();
    }

    public void PlayTween()
    {
        boxDT.transform.DOPlay();
        t.OnUpdate(PathCallback);
    }

    public void ExecutePath()
    {
        if (useDT) {
            trail.enabled = true;
            pathArray = storage.GetDTArray(true);
            t = boxDT.DOPath(pathArray, tweenDuration, pathType).SetOptions(false).SetLookAt(0.01f);
            t.SetEase(Ease.Linear).SetLoops(0);
            t.Pause();
        } else {
            ActivateBoxAnimator();
        }
    }

    public void PathCallback()
    {
        UIManager.instance.progressBar.fillAmount = t.ElapsedPercentage(false);
    }




    // to create points array based out of animated objects

    public void ActivateBoxAnimator()
    {
        boxAnimator.enabled = true;
        boxAnimator.gameObject.SetActive(true);
        boxDT.gameObject.SetActive(false);
    }


    public IEnumerator StorePathPoints()
    {
        while (boxAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1) {
            yield return new WaitForSeconds(0.03f);
            storage.StoreTarget(boxAnimator.transform.position);
            yield return null;
        }
    }
   //
}
