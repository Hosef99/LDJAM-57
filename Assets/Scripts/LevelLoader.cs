using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    private PlayerData playerData;

    void Start()
    {
        playerData = FindAnyObjectByType<PlayerData>();
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetMouseButtonDown(0)){
        //     LoadNextLevel();
        // }
    }

    // public void LoadNextLevel(){
    //     StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));

    // }

    // IEnumerator LoadLevel(int levelIndex){
    //     transition.SetTrigger("Start");

    //     yield return new WaitForSeconds(transitionTime);
    //     SceneManager.LoadScene(levelIndex);
    // }

    public void LoadScene(string sceneName){
        StartCoroutine(LoadSceneByName(sceneName));
    }

     IEnumerator LoadSceneByName(string sceneName){
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneName);
    }

    public void ToCave(){
        playerData.ToCave();
    }

    public void ToShop(){
        playerData.ToShop();
    }
}
