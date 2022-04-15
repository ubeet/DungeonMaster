using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] Text loadingProgress;
    
    private static SceneTransition _instance;
    private Animator animator;
    private AsyncOperation asyncSceneLoading;
    private static bool shouldPlayAnim = false;

    public static void SwitchScene(int sceneIndex)
    {
        _instance.animator.SetTrigger("In");
        _instance.asyncSceneLoading = SceneManager.LoadSceneAsync(sceneIndex);
        _instance.asyncSceneLoading.allowSceneActivation = false;
        
    }
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        _instance = this;
        if (shouldPlayAnim) _instance.animator.SetTrigger("Out");
    }

    public void OnAnimationOver()
    {
        shouldPlayAnim = true;
        _instance.asyncSceneLoading.allowSceneActivation = true;
    }

    private void Update()
    {
        if (asyncSceneLoading != null)
            loadingProgress.text = Mathf.RoundToInt(asyncSceneLoading.progress * 100) + "%";
    }
}
