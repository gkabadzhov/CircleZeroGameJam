using System;
using UnityEngine;
using UnityEngine.UI;
namespace OTBG.Utilities.UI
{
    public class ActionButton : MonoBehaviour
    {
        private Action _onClick;
        private Button _button;
        public void Initialise(Action onClick)
        {
            _button = GetComponent<Button>();

            _onClick = onClick;

            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() => _onClick?.Invoke());
        }
    }
}