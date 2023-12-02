using OTBG.Utilities.PropertyAttributes;
using OTBG.Utilities.UI;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [FoldoutGroup("References"), SerializeField]
    private ActionButton _playGameButton;
    [FoldoutGroup("References"), SerializeField]
    private ActionButton _settingsButton;
    [FoldoutGroup("References"), SerializeField]
    private ActionButton _exitGameButton;

    [FoldoutGroup("Configuration"), SerializeField, SceneDropdown]
    private string sceneToGoTo;
    private void Awake()
    {
        _playGameButton.Initialise(() =>
        {
            SceneManager.LoadSceneAsync(sceneToGoTo);
        });

        _exitGameButton.Initialise(() =>
        {
            Application.Quit();
        });


    }

}
