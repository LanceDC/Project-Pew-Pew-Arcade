using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneManagment
{ 
    public static bool isSceneLoading = false;
    public static void LoadScene(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    public static void LoadScene(string levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    public static int GetActiveSceneInt()
    {
        Scene scene = SceneManager.GetActiveScene();
        return scene.buildIndex;
    }

    public static string GetActiveSceneString()
    {
        Scene scene = SceneManager.GetActiveScene();
        return scene.name;
    }

    public static void LoadCurrentScene()
    {
        LoadScene(GetActiveSceneInt());
    }

    public static IEnumerator TransitionScreen(Animator animator, float transitionTime, string trigger, int levelIndex)
    {
        isSceneLoading = true;
        animator.SetTrigger(trigger);

        yield return new WaitForSeconds(1f);

        isSceneLoading = false;
        LoadScene(levelIndex);
    }
}
