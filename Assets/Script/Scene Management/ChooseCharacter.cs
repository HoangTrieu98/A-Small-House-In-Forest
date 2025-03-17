using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseCharacter : MonoBehaviour
{
    [SerializeField] private Image[] avatarCharacters;
    [SerializeField] private Button[] selectButtons;
    [SerializeField] private int currentSelectButton;
    [SerializeField] private int currentImageIndex;

    private void Start()
    {
        currentImageIndex = 0;
        UpdateImage();
        UpdateButton();
    }

    public void NextImage()
    {
        currentSelectButton++;
        currentImageIndex++;
        if (currentImageIndex >= avatarCharacters.Length)
        {
            currentSelectButton = 0;
            currentImageIndex = 0;
        }
        UpdateImage();
        UpdateButton();
    }

    public void PreviousImage()
    {
        currentSelectButton--;
        currentImageIndex--;
        if (currentImageIndex < 0)
        {
            currentSelectButton = selectButtons.Length - 1;
            currentImageIndex = avatarCharacters.Length - 1;
        }
        UpdateImage();
        UpdateButton();
    }
    private void UpdateButton()
    {
        foreach (var button in selectButtons)
        {
            button.gameObject.SetActive(false);
        }
        if (selectButtons.Length > 0)
        {
            selectButtons[currentSelectButton].gameObject.SetActive(true);
        }
    }
    private void UpdateImage()
    {
        foreach(var image in avatarCharacters)
        {
            image.gameObject.SetActive(false);
        }

        if (avatarCharacters.Length > 0)
        {
            avatarCharacters[currentImageIndex].gameObject.SetActive(true);
            PlayerPrefs.SetInt("Select", currentImageIndex);
        }
    }
}
