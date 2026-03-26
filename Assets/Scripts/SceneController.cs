using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;

    public enum SceneState { Ready, Loading, Finalizing }
    public SceneState CurrentState { get; private set; } = SceneState.Ready;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Load("MainMenuScene");
    }

    public void Load(params string[] sceneNames)
    {
        if (CurrentState != SceneState.Ready)
        {
            Debug.LogWarning("SceneController is busy. Ignoring load request.");
            return;
        }

        StartCoroutine(LoadMultipleAsync(sceneNames));
    }

    private IEnumerator LoadMultipleAsync(string[] scenes)
    {
        CurrentState = SceneState.Loading;

        for (int i = 0; i < scenes.Length; i++)
        {
            LoadSceneMode mode = (i == 0) ? LoadSceneMode.Single : LoadSceneMode.Additive;
            AsyncOperation op = SceneManager.LoadSceneAsync(scenes[i], mode);

            while (!op.isDone)
            {
                float normalizedProgress = Mathf.Clamp01(op.progress / 0.9f);
                yield return null;
            }
        }

        yield return null;

        Scene targetScene = SceneManager.GetSceneByName(scenes[0]);

        if (targetScene.IsValid())
        {
            SceneManager.SetActiveScene(targetScene);
        }
        else
        {
            Debug.LogError($"Scene {scenes[0]} is not valid! Was it added to Build Settings?");
        }

        CurrentState = SceneState.Finalizing;
        yield return null;

        CurrentState = SceneState.Ready;
    }
}
