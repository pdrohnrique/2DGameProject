using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Vector2 posicaoCheckpoint;
    private bool checkpointAtivo = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !checkpointAtivo)
        {
            checkpointAtivo = true;
            posicaoCheckpoint = transform.position; // Salva a posição do checkpoint
            Debug.Log("Checkpoint ativado!");
            
            // Atualiza o checkpoint no GameManager
            GameManager.Instance.UltimoCheckpoint = posicaoCheckpoint;
        }
    }
}