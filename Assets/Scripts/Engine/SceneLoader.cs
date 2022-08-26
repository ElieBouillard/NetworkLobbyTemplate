using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Scenes[] _scenesToLoad;
    
    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoad;

        for (int i = 0; i < _scenesToLoad.Length; i++)
        {
            SceneManager.LoadScene((int) _scenesToLoad[i], i == 0 ? LoadSceneMode.Single : LoadSceneMode.Additive);
        }
    }
    
    private void OnSceneLoad(Scene scene, LoadSceneMode loadSceneMode)
    {
        
    }

    [MenuItem("SceneLoader/PlayBuild")]
    private static void PlayBuild()
    {
        EditorSceneManager.SaveOpenScenes();

        EditorSceneManager.OpenScene("Assets/Scenes/LaunchScene.unity", OpenSceneMode.Single);
        
        EditorApplication.EnterPlaymode();
    }
}

public enum Scenes
{
    Launch = 0,
    StartMenu,
    Gameplay,
}