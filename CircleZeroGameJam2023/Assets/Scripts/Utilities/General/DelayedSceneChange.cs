using OTBG.Utilities.PropertyAttributes;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OTBG.Utilities.General
{
    public class DelayedSceneChange : MonoBehaviour 
    {
        [FoldoutGroup("Change Info"), SerializeField, SceneDropdown]
        private string sceneToGoTo;
        [FoldoutGroup("Change Info"), SerializeField]
        private float _sceneChangeDelay;
        public void TriggerSceneChange()
        {
            StartCoroutine(DelayToSceneChange());
        }

        private IEnumerator DelayToSceneChange()
        {
            yield return new WaitForSeconds(_sceneChangeDelay);
            SceneManager.LoadScene(sceneToGoTo);
        }
    }
}
