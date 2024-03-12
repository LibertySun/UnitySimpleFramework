using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingBehaviour : MonoBehaviour 
{
    private float PercentVal;
    SceneType _currentScene;
    private float loadingTime;
    void Start () 
    {
        _currentScene = GameSceneManager.Instance.NextScene;
        ViewManager.Instance.OpenView(ViewType.LoadingView);
        StartCoroutine(StartLoading(_currentScene.ToString()));
    }
    IEnumerator StartLoading(string sceneName)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;
        while (!op.isDone)
        {
            float progress = Mathf.Clamp01(op.progress / 0.9f);
            PercentVal = progress;

            if (op.progress >= 0.9f && op.isDone == false && op.allowSceneActivation == false)
            {
                PercentVal = 1;
                if (loadingTime < 1)
                {
                    yield return new WaitForSeconds(1f - loadingTime);
                }
                break;
            }
            yield return new WaitForEndOfFrame();
        }
        op.allowSceneActivation = true;
        GameSceneManager.Instance.OpenNextView();
    }

    private void Update()
    {
        if (ViewManager.Instance.GetCurrentView<LoadingView>() != null)
        {
            loadingTime += Time.deltaTime;
            ViewManager.Instance.GetCurrentView<LoadingView>().UpdateTime(PercentVal, loadingTime);
        }
    }
}
