using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o objeto que entrou na zona é o jogador
        if (collision.CompareTag("Player"))
        {
            // Chama a função de reiniciar do checkpoint no Player
            collision.GetComponent<Player>().ReiniciarDoCheckpoint();
        }
    }
}