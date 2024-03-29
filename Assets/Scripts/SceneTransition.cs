using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    [Header("Attributes")]
    
    [SerializeField] private Text loadingProgress;
    
    private static bool shouldPlayAnim = false;
    private static SceneTransition _instance;
    private AsyncOperation asyncSceneLoading;
    private Animator animator;
    
    private void Start()
    {
        Time.timeScale = 1;
        animator = GetComponent<Animator>();
        _instance = this;
        if (shouldPlayAnim) _instance.animator.SetTrigger("Out");
    }

    public static void SwitchScene(int sceneIndex)
    {
        _instance.animator.SetTrigger("In");
        _instance.asyncSceneLoading = SceneManager.LoadSceneAsync(sceneIndex);
        _instance.asyncSceneLoading.allowSceneActivation = false;
    }
    
    public void OnAnimationOver()
    {
        shouldPlayAnim = true;
        _instance.asyncSceneLoading.allowSceneActivation = true;
    }
}
