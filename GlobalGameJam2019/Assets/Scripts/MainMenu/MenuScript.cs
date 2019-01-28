using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

	[SerializeField]
	private GameObject mainMenuObject, creditsGameObject;
	
	public void LoadSceneAsync(string sceneName){
        Time.timeScale = 1;
        StartCoroutine(LoadYourAsyncScene(sceneName));
	}

	IEnumerator LoadYourAsyncScene(string sceneName){
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync (sceneName);

		while (!asyncLoad.isDone)
			yield return null;
        Time.timeScale = 1;
    }

	public void Quit(){
		Application.Quit ();
	}

	public void ShowCredits(){
		mainMenuObject.SetActive (false);
		creditsGameObject.SetActive (true);
	}

	public void BackFromCreditsToMenu(){
		creditsGameObject.SetActive (false);
		mainMenuObject.SetActive (true);
	}

}
