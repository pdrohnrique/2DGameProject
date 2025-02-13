using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float velocidade = 10f;
    [SerializeField] private float forcaPulo = 10f;
    [SerializeField] private LayerMask chaoLayer;
    [SerializeField] private Transform peCheck;
    [SerializeField] private float raioChecagem = 0.2f;

    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer spriteRenderer;
    private bool noChao;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Movimento horizontal com suavização
        float movimentoX = Input.GetAxis("Horizontal");
        _rigidbody2D.velocity = new Vector2(movimentoX * velocidade, _rigidbody2D.velocity.y);

        // Flip do sprite
        if(movimentoX != 0)
        {
            spriteRenderer.flipX = movimentoX < 0;
        }
        
        // Verifica se está no chão
        noChao = Physics2D.OverlapCircle(peCheck.position, raioChecagem, chaoLayer);

        // Pulo
        if(Input.GetButtonDown("Jump") && noChao)
        {
            _rigidbody2D.AddForce(Vector2.up * forcaPulo, ForceMode2D.Impulse);
        }
    }

    // Visualização do ground check no editor
    void OnDrawGizmosSelected()
    {
        if(peCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(peCheck.position, raioChecagem);
        }
    }
}