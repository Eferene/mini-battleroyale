using UnityEngine;
using Fusion;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [Header("Player Settings")]
    public string pName = "Player";
    public int cIndex = 0;

    [Header("UI Elements")]
    public TMP_InputField nameInputField;
    
    [Header("Character Models")]
    public GameObject[] characterModels;

    private void Start()
    {
        pName = "Player" + Random.Range(1000, 9999);
        nameInputField.text = pName;
        UpdateCharacterModels();
    }

    public void ConnectButton()
    {
        if(string.IsNullOrEmpty(nameInputField.text))
        {
            Debug.LogWarning("Player name cannot be empty!");
            return;
        }

        pName = nameInputField.text;

        PlayerDataHolder.Instance.data = new PlayerData
        {
            playerName = pName,
            characterIndex = cIndex
        };

        var runner = FindFirstObjectByType<NetworkRunner>();

        runner.StartGame(new StartGameArgs
        {
            GameMode = GameMode.AutoHostOrClient,
            SessionName = "Lobby",
            Scene = SceneRef.FromIndex(1), // Lobby sahnesinin indexi
            SceneManager = runner.gameObject.AddComponent<NetworkSceneManagerDefault>()
        });
    }

    public void NextCharacter()
    {
        cIndex = (cIndex + 1) % characterModels.Length;
        UpdateCharacterModels();
    }

    public void PreviousCharacter()
    {
        cIndex = (cIndex - 1 + characterModels.Length) % characterModels.Length;
        UpdateCharacterModels();
    }

    public void UpdateCharacterModels()
    {
        for (int i = 0; i < characterModels.Length; i++)
        {
            characterModels[i].SetActive(i == cIndex);
        }
    }
}
