using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItem : MonoBehaviourPunCallbacks
{
    public TMP_Text playerName;
    public GameObject arrowLeft;
    public GameObject arrowRight;

    private Hashtable playerProps = new Hashtable();

    public Image playerAvatar;
    public Sprite[] avatars;

    private Player player;

    public void SetPlayerInfo(Player _player)
    {
        playerName.text = _player.NickName;
        player = _player;
        UpdatePlayerItem(player);
    }

    public void ApplyLocalChanges()
    {
        arrowLeft.SetActive(true);
        arrowRight.SetActive(true);
    }

    public void OnLeftClickArrow()
    {
        if ((int) playerProps["playerAvatar"] == 0)
        {
            playerProps["playerAvatar"] = avatars.Length - 1;
        }
        else
        {
            playerProps["playerAvatar"] = (int) playerProps["playerAvatar"] - 1;
        }
        
        PhotonNetwork.SetPlayerCustomProperties(playerProps);
    }
    
    public void OnRightClickArrow()
    {
        if ((int) playerProps["playerAvatar"] == avatars.Length - 1)
        {
            playerProps["playerAvatar"] = 0;
        }
        else
        {
            playerProps["playerAvatar"] = (int) playerProps["playerAvatar"] + 1;
        }

        PhotonNetwork.SetPlayerCustomProperties(playerProps);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (player == targetPlayer)
        {
            UpdatePlayerItem(targetPlayer);
        }
    }

    private void UpdatePlayerItem(Player targetPlayer)
    {
        if (targetPlayer.CustomProperties.ContainsKey("playerAvatar"))
        {
            playerAvatar.sprite = avatars[(int) targetPlayer.CustomProperties["playerAvatar"]];
            playerProps["playerAvatar"] = (int) targetPlayer.CustomProperties["playerAvatar"];
        }
        else
        {
            playerProps["playerAvatar"] = 0;
        }
    }
}
