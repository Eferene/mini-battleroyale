using UnityEngine;
using Fusion;

public class LobbyManager : SimulationBehaviour, IPlayerJoined
{
    public static LobbyManager Instance;
    [SerializeField] private NetworkPrefabRef requesterPrefab;
    [SerializeField] private NetworkPrefabRef[] characterPrefabs;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayerJoined(PlayerRef player)
    {
        Debug.Log("PlayerJoined çağrıldı. IsServer: " + Runner.IsServer);
        if(!Runner.IsServer) return;

        Runner.Spawn(requesterPrefab, Vector3.zero, Quaternion.identity, player);
    }

    public void SpawnPlayer(PlayerRef player, int characterIndex)
    {
        if(!Runner.IsServer) return;
        Debug.Log("Spawning player " + player + " with character index " + characterIndex);

        int safeIndex = Mathf.Clamp(characterIndex, 0, characterPrefabs.Length - 1);
        Runner.Spawn(characterPrefabs[safeIndex], Vector3.zero, Quaternion.identity, player);
    }
}
