using UnityEngine;
using Fusion;

public class NetworkPlayer : NetworkBehaviour
{
    public override void Spawned()
    {
        if(Object.HasStateAuthority)
        {
            int id = PlayerDataHolder.Instance.data.characterIndex;
            RPC_SetCharacter(id);
        }
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    private void RPC_SetCharacter(int characterIndex)
    {
        if(Object.HasStateAuthority)
        {
            LobbyManager.Instance.SpawnPlayer(Object.InputAuthority, characterIndex);
            Runner.Despawn(Object);
        }
    }
}
