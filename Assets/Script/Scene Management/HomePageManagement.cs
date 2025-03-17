using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomePageManagement : MonoBehaviour
{
    [SerializeField] private GameObject chooseCharacters;
    [SerializeField] private Button comingSoonButton;
    [SerializeField] private GameObject settingPanel;

    public void StartGame()
    {
        SceneManager.LoadScene("Main Scene");
    }

    public void CharacterSButton()
    {
        chooseCharacters.SetActive(true);
    }

    public void CloseCharacterPanel()
    {
        chooseCharacters.SetActive(false);
    }

    public void SelectCharacter()
    {
        chooseCharacters.SetActive(false);
    }
    public void ClockButton()
    {
        comingSoonButton.gameObject.SetActive(true);
    }

    public void CloseComingSoonbutton()
    {
        comingSoonButton.gameObject.SetActive(false);
    }

    public void SettingButton()
    {
        settingPanel.SetActive(true);
    }

    public void SaveSetting()
    {
        settingPanel.SetActive(false);
    }

    public void ExitButton()
    {

    }
}
