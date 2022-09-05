using System;
using System.Collections;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

public class SteamInvitationsManager : MonoBehaviour
{
    [SerializeField] private RectTransform _content;
    [SerializeField] private SteamInvitation _invitationPrefab;
    private RectTransform _rectTransform;

    private float _offSetY = -25f;

    protected Callback<LobbyInvite_t> InviteLobby;
    
    private List<SteamInvitation> _invitations = new List<SteamInvitation>();
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        InviteLobby = Callback<LobbyInvite_t>.Create(OnInvitationReceived);
    }
    
    
    public void OnInvitationReceived(LobbyInvite_t lobbyInfo)
    {
        SteamInvitation invitationTemp = AddInvitation();
        invitationTemp.Initialize((CSteamID)lobbyInfo.m_ulSteamIDUser);
        _invitations.Add(invitationTemp);
    }
    
    private SteamInvitation AddInvitation()
    {
        _rectTransform = GetComponent<RectTransform>();
        SteamInvitation invitationTemp = Instantiate(_invitationPrefab, _content);

        if (_invitations.Count < 3)
        {
            Vector2 rectTransformDeltaSize = _rectTransform.sizeDelta;
            rectTransformDeltaSize = new Vector2(rectTransformDeltaSize.x, rectTransformDeltaSize.y + 125f);
            _rectTransform.sizeDelta = rectTransformDeltaSize;
        }

        Vector2 contentDeltaSize = _content.sizeDelta;
        contentDeltaSize = new Vector2(contentDeltaSize.x, contentDeltaSize.y + 125f);
        _content.sizeDelta = contentDeltaSize;
        
        Vector3 invitationPos =  invitationTemp.transform.localPosition;
        invitationPos = new Vector3(invitationPos.x, _offSetY, invitationPos.y);
        invitationTemp.transform.localPosition = invitationPos;
        
        _offSetY -= 125f;
        return invitationTemp;
    }
}
