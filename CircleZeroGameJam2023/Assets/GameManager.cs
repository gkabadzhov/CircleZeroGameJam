using Cinemachine;
using OTBG.Gameplay.Player;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [FoldoutGroup("References"), SerializeField]
    public CinemachineVirtualCamera _playerCamera;
    [FoldoutGroup("References"), SerializeField]
    public PlayerController _player;

    private void Awake()
    {
        if(_player == null)
            _player = FindFirstObjectByType<PlayerController>(FindObjectsInactive.Exclude);
        if(_playerCamera == null)
            _playerCamera = FindFirstObjectByType<CinemachineVirtualCamera>(FindObjectsInactive.Exclude);
    }

    private void Start()
    {
        Initialise();
    }

    public void Initialise()
    {
        _player.Initialise();

        if (_playerCamera.Follow == null)
            _playerCamera.Follow = _player.transform;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
