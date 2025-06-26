using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private Transform[] points;
    private int currentPoint = 0;

    void Update()
    {
        // Verifica se há pontos definidos
        if (points.Length == 0) return;

        // Movimento
        transform.position = Vector2.MoveTowards(
            transform.position,
            points[currentPoint].position,
            speed * Time.deltaTime
        );

        // Atualiza o ponto atual
        if (Vector2.Distance(transform.position, points[currentPoint].position) < 0.1f)
        {
            currentPoint = (currentPoint + 1) % points.Length;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.transform.parent = gameObject.transform;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        collision.gameObject.transform.parent = null;
    }

    void OnDrawGizmos()
    {
        if (points == null || points.Length == 0) return;

        Gizmos.color = Color.blue;
        foreach (Transform point in points)
        {
            Gizmos.DrawWireSphere(point.position, 0.2f);
        }
    }
}