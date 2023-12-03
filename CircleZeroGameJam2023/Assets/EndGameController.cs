using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class EndGameController : MonoBehaviour
{
    public Image leftHand;
    public TextMeshProUGUI leftText;
    public Image rightHand;
    public TextMeshProUGUI rightText;
    public GameObject _tauntText;

    public MouseFadeUI _mouseFadeBlue;

    public void OnEnable()
    {
        _mouseFadeBlue.enabled = false;
        leftHand.DOFade(0, 0);
        rightHand.DOFade(0, 0);
        leftText.DOFade(0, 0);
        rightText.DOFade(0, 0);

        Initialise();
    }

    public void Initialise()
    {
        StartCoroutine(ShowRoutine());
    }

    public IEnumerator ShowRoutine()
    {
        
        yield return new WaitForSeconds(1);
        yield return ShowLeft();
        yield return new WaitForSeconds(1);
        yield return ShowRight();

        yield return new WaitForSeconds(3f);
        leftText.gameObject.SetActive(false);
        rightText.gameObject.SetActive(false);
        _tauntText.SetActive(true);
    }

    public IEnumerator ShowLeft()
    {
        leftHand.DOFade(1, 2f);
        yield return new WaitForSeconds(2);
        _mouseFadeBlue.enabled = true;
        leftText.DOFade(1, 0.5f);

    }
    public IEnumerator ShowRight()
    {
        rightHand.DOFade(1, 2f);
        yield return new WaitForSeconds(2);
        rightText.DOFade(1, 0.5f);
    }
}
