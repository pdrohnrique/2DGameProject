using UnityEngine;
using UnityEngine.SceneManagement;

public class Texts : MonoBehaviour
{
    public GameObject TelaIni; // Tela inicial (menu)
    public GameObject Final;   // Tela de fim de jogo
    public Player player; // Referência ao jogador

    public static Texts instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        TelaIni.SetActive(true); // Mostra a tela inicial
        Final.SetActive(false);  // Esconde a tela de fim
        player.enabled = false;  // Desativa o jogador inicialmente
    }

    // Chamado pelo botão "Iniciar" da tela inicial
    public void StartGame()
    {
        TelaIni.SetActive(false);
        player.enabled = true; // Ativa o jogador
    }

    // Chamado quando o jogador toca na plataforma
    public void ShowGameOver()
    {
        Final.SetActive(true);
        player.enabled = false; // Desativa o jogador
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero; // Para o movimento
    }

    // Chamado pelo botão "Reiniciar"
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1; // Garante que o tempo volte ao normal
    }
}