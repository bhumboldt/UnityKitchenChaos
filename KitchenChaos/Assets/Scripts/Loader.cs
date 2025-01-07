using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene
    {
        MainMenuScene,
        GameScene,
        LoadingScene
    }
    private static Scene TargetScene;

    public static void Load(Scene targetSceneName)
    {
        Loader.TargetScene = targetSceneName;

        SceneManager.LoadScene(Loader.Scene.LoadingScene.ToString());
    }

    public static void LoaderCallback()
    {
        SceneManager.LoadScene(TargetScene.ToString());
    }
}
