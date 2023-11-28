//ClientController.cs
using Mirror;
using UnityEngine;

public class ClientController : NetworkBehaviour
{
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        if (UIActions.Instance != null)
        {
            UIActions.Instance.SetClientController(this);
        }
    }


    [Client]
    public void ClientAddAccount(string name, string email)
    {
        if(isLocalPlayer)
        {
            CmdAddAccount(name, email);
        }
        else
        {
            Debug.Log("Player is not the local player");
        }
    }

    [Command]
    private void CmdAddAccount(string name, string email, NetworkConnectionToClient sender = null)
    {
        bool success = Database.singleton.AddAccount(name, email);
        TargetAccountAdded(sender, success ? "Account added successfully" : "Failed to add account");
    }

    [TargetRpc]
    private void TargetAccountAdded(NetworkConnectionToClient target, string result)
    {
        Debug.Log(result);
    }
}