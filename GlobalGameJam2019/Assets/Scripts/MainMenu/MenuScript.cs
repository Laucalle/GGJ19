using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {
	
	public void LoadSceneAsync(string sceneName){

		StartCoroutine(LoadYourAsyncScene(sceneName));
	}

	IEnumerator LoadYourAsyncScene(string sceneName){
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync (sceneName);

		while (!asyncLoad.isDone)
			yield return null;
	}

	public void Quit(){
		Application.Quit ();
	}

}
