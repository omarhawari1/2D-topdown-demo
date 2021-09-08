using System.Collections;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;
using Mirror;

public class SteamJoin : MonoBehaviour
{
    protected Callback<LobbyCreated_t> lobbyCreated;
    protected Callback<GameLobbyJoinRequested_t> gameLobbyJoinRequested;
    protected Callback<LobbyEnter_t> lobbyEnterd;

    private void Start() 
    {
        lobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
        gameLobbyJoinRequested = Callback<GameLobbyJoinRequested_t>.Create(OnGameLobbyJoinRequested);
        lobbyEnterd = Callback<LobbyEnter_t>.Create(OnLobbyEnterd);
    }
    public void HostLobby()
    {
        SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, 6);
    }
    private void OnLobbyCreated(LobbyCreated_t callBack)
    {
        if(callBack.m_eResult != EResult.k_EResultOK){return;}
        NetworkManager.singleton.StartHost();

        SteamMatchmaking.SetLobbyData(new CSteamID(callBack.m_ulSteamIDLobby), "HostAddress", SteamUser.GetSteamID().ToString());
    }
    private void OnGameLobbyJoinRequested(GameLobbyJoinRequested_t callBack)
    {
        SteamMatchmaking.JoinLobby(callBack.m_steamIDLobby);
    }
    private void OnLobbyEnterd(LobbyEnter_t callBack)
    {
        if(NetworkServer.active){return;}

        string hostAddress = SteamMatchmaking.GetLobbyData(new CSteamID(callBack.m_ulSteamIDLobby), "HostAddress");

        NetworkManager.singleton.networkAddress = hostAddress;
        NetworkManager.singleton.StartClient();

        
    }

}
