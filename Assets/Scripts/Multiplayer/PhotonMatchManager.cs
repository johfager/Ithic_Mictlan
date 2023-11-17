using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

// TODO : 28) Timer doesn't restart counting on the next match (useSameMap = true). Fix it. DONE

public class PhotonMatchManager : MonoBehaviourPunCallbacks, IOnEventCallback
{
    public bool useSameMap;
    public enum EventsCodes : byte
    {
        NewPlayer, // 0
        ListPlayers, // 1
        ChangeStat, // 2
        NextMatch, // 3
        TimerSync
    }

    [SerializeField] private List<PhotonPlayerInfo> _playersInfo = new List<PhotonPlayerInfo>();
    
    public enum GameStates
    {
        WaitingForPlayers,
        Playing,
        EndGame
    }
    
    public int killsToWin = 5;
    public GameStates gameState = GameStates.WaitingForPlayers;
    public float waitTimeAfterEnding = 5;
        
    private float matchLength = 60f;
    public float currentMatchTime = 0;
    private float _sentTimer = 1; // Used to send the timer every second

    // Start is called before the first frame update
    void Start()
    {
        // Si el jugador no esta conectado, etonces regresamos al menu principal
        // No usamos PhotonNetwork.LoadLevel() porque queremos llamar la escena de forma local unicamente
        if (!PhotonNetwork.IsConnected)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            StartMatch();
        }
    }

    private void Update()
    {
        // Only the master client can sent time information
        if (PhotonNetwork.IsMasterClient)
        {
            if (currentMatchTime >= 0 && gameState == GameStates.Playing)
            {
                currentMatchTime -= Time.deltaTime;

                if (currentMatchTime <= 0)
                {
                    currentMatchTime = 0;
                    gameState = GameStates.EndGame;

                    ListPlayersSent();
                    EndGame();
                }

                SetMatchTime();

                _sentTimer -= Time.deltaTime;

                // We cant set timeo directly o 1 since it will be time discrepancies between players because 
                // my delta time is not the same for every player
                if (_sentTimer <= 0)
                {
                    _sentTimer += 1;
                    TimerSend();
                }
            }
        }
    }

    void StartMatch()
    {
        // Start the game
        gameState = GameStates.Playing;
        currentMatchTime = matchLength;
        SetMatchTime();
    }

    public void OnEvent(EventData photonEvent)
    {
        // Valores de eventos hasta el 200 para el usuario, mas de 200 son reservados para photon
        if (photonEvent.Code < 200)
        {
            // Castea evento y es recibido por photon
            EventsCodes eventCode = (EventsCodes) photonEvent.Code;

            Debug.Log("Evento recibido: " + eventCode);

            //Guarda la info del evento
            object[] data = (object[]) photonEvent.CustomData;

            // Hacer algo con el codigo e info recibida
            switch(eventCode)
            {
                case EventsCodes.NewPlayer:
                    NewPlayerReceived(data);
                    break;
                case EventsCodes.ListPlayers:
                    ListPlayersReceived(data);
                    break;
                case EventsCodes.ChangeStat:
                    ChangeStatReceived(data);
                    break;
                case EventsCodes.NextMatch:
                    NextMatchReceived(data);
                    break;
                case EventsCodes.TimerSync:
                    TimerReceived(data);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    #region Eventos para enviar y recibir de photon
        public void NewPlayerSent(string playerName)
        {
            // Crea un arreglo de objetos para enviar
            object[] package = new object[5];
            package[0] = playerName;
            package[1] = 0;
            package[2] = 0;
            package[3] = 0;
            package[4] = PhotonNetwork.LocalPlayer.ActorNumber;

            // Envia el paquete a todos los jugadores en la room
            PhotonNetwork.RaiseEvent(
                (byte) EventsCodes.NewPlayer, //Tipo de evento que mandaremos
                package, // Datos que mandaremos
                new RaiseEventOptions {Receivers = ReceiverGroup.MasterClient}, // A quien le mandaremos los datos
                SendOptions.SendReliable
                );
        }

        public void NewPlayerReceived(object[] dataReceived)
        {
            //Unbox de la info recibida, no olvidar castear la informacion correctamente por los tipos
            string playerName = (string)dataReceived[0];
            int playerKills = (int)dataReceived[1];
            int playerDeaths = (int)dataReceived[2];
            int playerScore = (int)dataReceived[3];
            int playerID = (int)dataReceived[4];

            // Check if the player with the same ActorNumber is already in the list
            bool playerExists = _playersInfo.Exists(player => player.PlayerID == playerID);

            if (!playerExists)
            {
                PhotonPlayerInfo newPlayer = new PhotonPlayerInfo(playerName, playerKills, playerDeaths, playerScore, playerID);
                _playersInfo.Add(newPlayer);
            }
            
            ListPlayersSent();
        }

        public void ListPlayersSent()
        {
            // Crear el paquete a enviar
            object[] package = new object[_playersInfo.Count + 1];

            Debug.Log($"Sending game state {gameState}");
            package[0] = gameState; // Send my game state to all the players
            
            // Llenar con la informacion de cada jugador
            for (int i = 0; i < _playersInfo.Count; i++)
            {
                object[] playerInfo = new object[5];
                playerInfo[0] = _playersInfo[i].PlayerName;
                playerInfo[1] = _playersInfo[i].PlayerKills;
                playerInfo[2] = _playersInfo[i].PlayerDeaths;
                playerInfo[3] = _playersInfo[i].PlayerScore;
                playerInfo[4] = _playersInfo[i].PlayerID;
                
                // Agregar jugador al paquete
                package[i + 1] = playerInfo;
            }

            // Enviar paquete a todos los jugadores
            PhotonNetwork.RaiseEvent(
                (byte) EventsCodes.ListPlayers, // Tipo de venta
                package,  // Datos
                new RaiseEventOptions {Receivers = ReceiverGroup.All}, // Quien recibe el evento
                SendOptions.SendReliable
            );
        }

        public void ListPlayersReceived(object[] dataReceived)
        {
            // Crear el paquete a enviar
            _playersInfo.Clear();

            // unbox the game state
            gameState = (GameStates) dataReceived[0];
            Debug.Log($"Receiving game state {gameState}");
            CheckGameState();
            
            // Unbox de la info recibida, no olvidar castear la informacion correctamente por los tipos
            for (int i = 1; i < dataReceived.Length; i++)
            {
                object[] playerInfo = (object[]) dataReceived[i];
                
                PhotonPlayerInfo newPlayer = new PhotonPlayerInfo(
                    (string) playerInfo[0],
                    (int) playerInfo[1],
                    (int) playerInfo[2],
                    (int) playerInfo[3],
                    (int) playerInfo[4]
                );
            
                // Agregar jugador a la lista
                _playersInfo.Add(newPlayer);
            }
        }

        public void ChangeStatSent(int sendingActor, int statToUpdate, int AmountToChange)
        {
            object[] package = new object[3];
            package[0] = sendingActor;
            package[1] = statToUpdate;
            package[2] = AmountToChange;
            
            PhotonNetwork.RaiseEvent(
                (byte) EventsCodes.ChangeStat,
                package,
                new RaiseEventOptions {Receivers = ReceiverGroup.All},
                SendOptions.SendReliable
            );
        }

        public void ChangeStatReceived(object[] dataReceived)
        {
            int sendingActor = (int) dataReceived[0];
            int statToUpdate = (int) dataReceived[1];
            int amountToChange = (int) dataReceived[2];
            
            for (int i = 0; i < _playersInfo.Count; i++)
            {
                if (_playersInfo[i].PlayerID == sendingActor)
                {
                    switch (statToUpdate)
                    {
                        case 0:
                            _playersInfo[i].PlayerKills += amountToChange;
                            Debug.Log($"Player [{_playersInfo[i].PlayerName}] has [{_playersInfo[i].PlayerKills}] kills");
                            break;
                        case 1:
                            _playersInfo[i].PlayerDeaths += amountToChange;
                            Debug.Log($"Player [{_playersInfo[i].PlayerName}] has [{_playersInfo[i].PlayerDeaths}] deaths");
                            break;
                        case 2:
                            _playersInfo[i].PlayerScore += amountToChange;
                            Debug.Log($"Player [{_playersInfo[i].PlayerName}] has [{_playersInfo[i].PlayerScore}] score");
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
             // Update the logic for the ond of game
            ScoreCheck();
        }

        private void NextMatchSend()
        {
            // Send the package to all the players in the room
            PhotonNetwork.RaiseEvent(
                (byte) EventsCodes.NextMatch, // Which type of event are we sending 
                null,  // Event date to send
                new RaiseEventOptions {Receivers = ReceiverGroup.All}, // Who will receive the event 
                SendOptions.SendReliable
            );
        }

        private void NextMatchReceived(object[] data = null)
        {
            // UIController.Instance.HideDeathScreen();
            gameState = GameStates.WaitingForPlayers;
            
            // Reset player stats
            for (int i = 0; i < _playersInfo.Count; i++)
            {
                _playersInfo[i].PlayerKills = 0;
                _playersInfo[i].PlayerDeaths = 0;
                _playersInfo[i].PlayerScore = 0;
            }
            
            currentMatchTime = matchLength;
            FindObjectOfType<PhotonSpawnPlayer>().Instance.Start();
            StartMatch();
        }

        /// <summary>
        /// Sent the current time to all players
        /// </summary>
        public void TimerSend()
        {
            object[] package = new object[] { (int)currentMatchTime, gameState };
            
            // Send the package to all the players in the room
            PhotonNetwork.RaiseEvent(
                (byte) EventsCodes.TimerSync, // Which type of event are we sending 
                package,  // Event date to send
                new RaiseEventOptions {Receivers = ReceiverGroup.All}, // Who will receive the event 
                SendOptions.SendReliable
            );
        }
        
        /// <summary>
        /// Do something with the time I received from the master server
        /// </summary>
        /// <param name="data"></param>
        public void TimerReceived(object[] data)
        {
            currentMatchTime = (int) data[0];
            gameState = (GameStates) data[1];
            SetMatchTime();
        }

        /// <summary>
        /// Triggered when the player left the room
        /// </summary>
        public override void OnLeftRoom()
        {
            SceneManager.LoadScene(0);
        }

    #endregion

    /// <summary>
        /// Function that iterates the players in room lookinf for the player with the highest kills
        /// </summary>
        private void ScoreCheck()
        {
            Debug.Log($"ScoreCheck");
            bool winnerFound = false;
            // Loop through the players in the room
            for (int i = 0; i < _playersInfo.Count; i++)
            {
                // Check if the player has the highest kills
                if (_playersInfo[i].PlayerKills >= killsToWin && killsToWin > 0)
                {
                    // End the game
                    winnerFound = true;
                    break;
                }
            }

            if(winnerFound)
                if (PhotonNetwork.IsMasterClient && gameState != GameStates.EndGame)
                {
                    gameState = GameStates.EndGame;
                    ListPlayersSent();
                }
        }
        
        /// <summary>
        /// SHecks the game state
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void CheckGameState()
        {
            if (gameState == GameStates.EndGame)
            {
                EndGame();
            }
        }

        /// <summary>
        /// Finish the current match
        /// </summary>
        private void EndGame()
        {
            // Set the game state to end game
            gameState = GameStates.EndGame;
            
            // Stops automatic player instantiation
            FindObjectOfType<PhotonSpawnPlayer>().Instance.StopAllCoroutines();
            
            // Release the cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            
            if (PhotonNetwork.IsMasterClient)
            {
                // Wait for the end of the game and then go mack to the main menu
                StartCoroutine(CorBackToMainMenu());
                
                // Destroy all the multiplayer objects
                PhotonNetwork.DestroyAll();
            }
        }

        /// <summary>
        /// Updates the match time
        /// </summary>
        private void SetMatchTime()
        {
            // UIController.Instance.SetTime(currentMatchTime);
        }

        private IEnumerator CorBackToMainMenu()
        {
            yield return new WaitForSeconds(waitTimeAfterEnding);

            if (!useSameMap)
            {
                // This will triugger our OnLeftRoom Function
                PhotonNetwork.LeaveRoom();
                // Not sync scenes anymore between players
                PhotonNetwork.AutomaticallySyncScene = false;
            }
            else
            {
                NextMatchSend();
            }
        }

    public int GetActorID(string playerNickName)
        {
            for (int i = 0; i < _playersInfo.Count; i++)
            {
                if (_playersInfo[i].PlayerName == playerNickName)
                {
                    Debug.Log($"[ Returning PlayerActor ID {_playersInfo[i].PlayerID}]");
                    return _playersInfo[i].PlayerID;
                }
            }

            return -1;
        }
}
