using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private GameObject uiParent;
    [SerializeField]private Button tutorialButton;
    // Start is called before the first frame update
    void Start()
    {

       GameInput.Instance.OnInteractAction += Instance_OnInteractAction;
        tutorialButton.onClick.AddListener(() =>
        {
            Show();
        });

    }

    private void Instance_OnInteractAction(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void Show()
    {
        uiParent.SetActive(true);
    }
    private void Hide()
    {
        uiParent.SetActive(false);
    }

}
