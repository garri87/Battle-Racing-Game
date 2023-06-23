using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class UIManager : MonoBehaviour
{

private Button startButton;
    private Button multiplayerButton;
    private Button optionsButton;
    private Button exitButton;
    private Button createGameButton;
    private Button joinGameButton;
    private Button startGameButton;
    private Button cancelButton;
    private Button resumeButton;
    private Button restartButton;
    private Button mainMenuButton;

    private void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        // Obtener las referencias a los elementos interactuables de la UI
        //startButton = root.Q<Button>("startButton");
        //multiplayerButton = root.Q<Button>("multiplayerButton");
        //optionsButton = root.Q<Button>("optionsButton");
        //exitButton = root.Q<Button>("exitButton");
        createGameButton = root.Q<Button>("createGameButton");
        joinGameButton = root.Q<Button>("joinGameButton");
        //startGameButton = root.Q<Button>("startGameButton");
        //cancelButton = root.Q<Button>("cancelButton");
        //resumeButton = root.Q<Button>("resumeButton");
        //restartButton = root.Q<Button>("restartButton");
        //mainMenuButton = root.Q<Button>("mainMenuButton");

        // Asignar los métodos de gestión de acciones a los elementos interactuables
       // startButton.clickable.clicked += StartGame;
       // multiplayerButton.clickable.clicked += OpenMultiplayerMenu;
       // optionsButton.clickable.clicked += OpenOptions;
       // exitButton.clickable.clicked += ExitGame;
        createGameButton.clickable.clicked += CreateGame;
        joinGameButton.clickable.clicked += JoinGame;
       // startGameButton.clickable.clicked += StartMultiplayerGame;
       // cancelButton.clickable.clicked += CancelMultiplayerGame;
       // resumeButton.clickable.clicked += ResumeGame;
       // restartButton.clickable.clicked += RestartGame;
       // mainMenuButton.clickable.clicked += ReturnToMainMenu;
    }

    private void StartGame()
    {
        // Acción del botón Iniciar Juego 1 jugador
        // Iniciar el juego para un jugador
    }

    private void OpenMultiplayerMenu()
    {
        // Acción del botón Iniciar Juego Multijugador
        // Abrir el menú de modo multijugador
    }

    private void OpenOptions()
    {
        // Acción del botón Opciones
        // Abrir el menú de opciones
    }

    private void ExitGame()
    {
        // Acción del botón Salir del juego
        // Salir del juego
        Application.Quit();
    }

    private void CreateGame()
    {
        // Acción del botón de crear partida
        // Crear una nueva partida multijugador
        //NetworkManager.Singleton.StartHost();
        //gameObject.SetActive(false);

    }

    private void JoinGame()
    {
        // Acción del botón de unirse a una partida
        // Unirse a una partida multijugador existente
          //NetworkManager.Singleton.StartClient();
         // gameObject.SetActive(false);
    }

    private void StartMultiplayerGame()
    {
        // Acción del botón Comenzar partida
        // Iniciar la partida multijugador
    }

    private void CancelMultiplayerGame()
    {
        // Acción del botón Salir del lobby
        // Cancelar la partida multijugador y volver al menú principal
    }

    private void ResumeGame()
    {
        // Acción del botón Reanudar
        // Reanudar el juego
    }

    private void RestartGame()
    {
        // Acción del botón Reiniciar
        // Reiniciar el juego
    }

    private void ReturnToMainMenu()
    {
        // Acción del botón Salir al menú principal
        // Volver al menú principal
    }

}
