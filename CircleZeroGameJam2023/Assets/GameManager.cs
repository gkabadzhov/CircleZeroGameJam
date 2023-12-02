using Cinemachine;
using OTBG.Gameplay.Player;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [FoldoutGroup("References"), SerializeField]
    public CinemachineVirtualCamera _playerCamera;
    [FoldoutGroup("References"), SerializeField]
    public PlayerController _player;

    private void Awake()
    {
        _player = FindFirstObjectByType<PlayerController>(FindObjectsInactive.Exclude);
        _playerCamera = FindFirstObjectByType<CinemachineVirtualCamera>(FindObjectsInactive.Exclude);
    }

    public void Initialise()
    {
        _player.Initialise();

        if (_playerCamera.Follow == null)
            _playerCamera.Follow = _player.transform;
    }
}
