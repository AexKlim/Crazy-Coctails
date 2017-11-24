using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Manager : MonoBehaviour {
	
	public void LoadScene(int id){
		Time.timeScale = 1f;
		SceneManager.LoadScene (id);
	}

}
