using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicChat : MonoBehaviour
{
    public string message;

    public ChatView history;

    public PhotonView view;


    private void Start()
    {
        history = GameObject.FindObjectOfType<ChatView>();

        view = gameObject.GetComponent<PhotonView>();
    }

    private void OnGUI()
    {
        message = GUI.TextField(new Rect(Screen.width - 260, 10, 250, 30), message);

        if (GUI.Button(new Rect(Screen.width - 110, 45, 100, 30), "Send"))
        {
            view.RPC("sendMessage", RpcTarget.AllBuffered, message);
        }
    }

    [PunRPC]
    public void sendMessage(string msg)
    {
        history.messages.Add(msg);
    }
}
