using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleMirror : MonoBehaviour
{
    [SerializeField] Image humanImage, gorillaImage;
    [SerializeField] GameObject blackPanel;
    [SerializeField] RectTransform mirror;
    [SerializeField] bool debugVal;
    [SerializeField] CharacterController human;

    [NaughtyAttributes.Button]
    public void ShowMirror() => ShowMirror(human.IsGorilla);

    public void ToggleMirror()
    {
        if (blackPanel.activeInHierarchy)
        {
            HideMirror();
        }
        else
        {
            human.ToggleForm();
            ShowMirror();
        }
    }
    private void ShowMirror(bool isGorilla)
    {
        HandleImages(isGorilla);
        blackPanel.SetActive(true);
        DOTween.To(() => mirror.anchoredPosition, (value) => mirror.anchoredPosition = value, Vector2.zero, 0.6f).SetEase(Ease.InOutQuad);
    }
    public void HideMirror()
    {
        blackPanel.SetActive(false);
        DOTween.To(() => mirror.anchoredPosition, (value) => mirror.anchoredPosition = value, Vector2.zero.ChangeVector(y: -2285), 0.6f).SetEase(Ease.InOutQuad); ;
    }


    public void HandleImages(bool isGorilla)
    {
        gorillaImage.gameObject.SetActive(false);
        humanImage.gameObject.SetActive(false);

        if (isGorilla)
            gorillaImage.gameObject.SetActive(true);
        else
            humanImage.gameObject.SetActive(true);
    }

}
