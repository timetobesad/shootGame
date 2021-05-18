using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ChatView : MonoBehaviour
{
    public List<string> messages;

    public PhotonView view;

    private void Start()
    {
        view = PhotonView.Get(this);
        messages = new List<string>();
    }

    public void OnGUI()
    {
        if (!PhotonNetwork.InRoom || !view.IsMine)
            return;

        for (int i = 0; i < messages.Count; i++)
            GUI.Box(new Rect(5, 5 + (i * 30), 200, 30), messages[i]);
    }
}
