using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Editor.Editor
{
    [InitializeOnLoad]
    public class EditorUtility
    {
        const string STARTER_PATH = "Assets/Scenes/Starter.unity";

        static EditorUtility()
        {
            EditorApplication.update = Update;
        }

        [MenuItem("Game/Run Starter")]
        public static void RunStarter()
        {
            if (!EditorApplication.isPlaying)
            {
                PlayerPrefs.SetString("restoreScenePath", SceneManager.GetActiveScene().path);
                EditorSceneManager.SaveOpenScenes();
                EditorSceneManager.OpenScene(STARTER_PATH);
                PlayerPrefs.SetInt("needRestore", 1);
                EditorApplication.isPlaying = true;
            }
        }

        [MenuItem("Game/Clear Data")]
        public static void ClearData()
        {
            Caching.ClearCache();
            PlayerPrefs.DeleteAll();
            FileUtil.DeleteFileOrDirectory(Application.persistentDataPath);
        }

        static void Update()
        {
            if (!EditorApplication.isPlaying &&
                !EditorApplication.isPlayingOrWillChangePlaymode &&
                PlayerPrefs.GetInt("needRestore") == 1)
            {
                PlayerPrefs.SetInt("needRestore", 0);
                EditorSceneManager.OpenScene(PlayerPrefs.GetString("restoreScenePath"));
            }
        }
    }
}
