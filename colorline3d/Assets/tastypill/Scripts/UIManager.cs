using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

public static UIManager instance = null;

	public Text textPerc;
	public Text textWin;
	public Image progressBar;
	public Image blockPanel;
	public GameObject restartBtn;
  private string dumpValue;

	private void Awake()
	 {
			 if (instance == null)
			 {
					 instance = this;
			 }
			 else
			 {
					 Destroy(gameObject);
			 }
	 }

	 public void RestartGameOnAppRuntime()
	 {
	 	Scene scene = SceneManager.GetActiveScene();
	 	SceneManager.LoadScene(scene.name);
	 }

  	public void ShowHideEndGameUI(bool show)
 	 {
		 if(GameManager.instance.outcome == outcome.victory){
		  restartBtn.SetActive(show);
      blockPanel.enabled = show;
			textWin.gameObject.SetActive(show);
			textWin.text = "YOU WIN!!";
			textWin.GetComponent<RandomTextColor>().AnimateColor();
			textPerc.gameObject.SetActive(show);
			textPerc.text = "COMPLETED 100.0%";
		} else {
			restartBtn.SetActive(show);
			blockPanel.enabled = show;
			textWin.gameObject.SetActive(show);
			textWin.text = "YOU LOSE";
			textWin.GetComponent<RandomTextColor>().ApplyLosingColor();
			textPerc.gameObject.SetActive(show);
			textPerc.text = dumpValue == "NaN" ? "COMPLETED " + "100.0%" : "COMPLETED " + dumpValue;
		}
 	 }

   public void RestartGameUI()
	 {
		  progressBar.fillAmount = 0;
      ShowHideEndGameUI(false);
	 }

   public void RestartState()
	 {
		 GameManager.instance.RestartGameState();
	 }

   public void UpdatePercentage ()
	 {
		dumpValue = (GameManager.instance.t.Elapsed(false) / GameManager.instance.t.Duration(false)).ToString("0.0%");
	 }
}
