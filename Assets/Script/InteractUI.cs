// InteractionUI.cs
using UnityEngine;
using UnityEngine.UI;

public class InteractUI : MonoBehaviour
{
    public static InteractUI Instance;

    [SerializeField] private Text interactionText;
    [SerializeField] private GameObject dialogue;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dialogue.SetActive(false);
        }
    }

    public void ShowText(string text)
    {
        interactionText.text = text;
        interactionText.gameObject.SetActive(true);
    }

    public void HideText()
    {
        interactionText.gameObject.SetActive(false);
    }

    public void ShowDialogue(string content)
    {
        dialogue.SetActive(true);
        Text dText = dialogue.GetComponentInChildren<Text>();
        if (dText != null)
        {
            dText.text = content;
        }
    }
}