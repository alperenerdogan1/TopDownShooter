﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using TopDownShooer.Network;
using TMPro;
using UnityEngine.UI;

namespace TopDownShooter.UI
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _currentState;
        [SerializeField] private Button[] _networkButtons;
        [SerializeField] private TMP_InputField _inputField;

        private void Awake()
        {
            UpdateUIWithNetworkState(MatchmakingController.Instance.CurrentNetworkState);
            MessageBroker.Default.Receive<EventPlayerNetworkStateChange>().Subscribe(OnPlayerNetworkState).AddTo(gameObject);

            _inputField.onEndEdit.AddListener(OnEditEnd);
        }
        private void OnEditEnd(string arg0)
        {
            Debug.Log("qwe:" + arg0);
            PhotonNetwork.playerName = arg0;
        }
        public void OnPlayerNetworkState(EventPlayerNetworkStateChange obj)
        {
            var networkState = obj.PlayerNetworkState;
            UpdateUIWithNetworkState(networkState);
            //_currentState.color = Color.green;
        }

        private void UpdateUIWithNetworkState(PlayerNetworkState networkState)
        {
            _currentState.text = "Connection Stat: " + networkState.ToString();
            for (int i = 0; i < _networkButtons.Length; i++)
            {
                _networkButtons[i].interactable = networkState == PlayerNetworkState.Connected;
            }
        }

        public void _CreateRoomClick()
        {
            MatchmakingController.Instance.CreateRoom();
        }
        public void _JoinRandomRoomClick()
        {
            MatchmakingController.Instance.JoinRandomRoom();

        }
        public void _SettingsClick()
        {
            Debug.Log("not ready yet");
        }
    }
}