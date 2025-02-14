using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Movimento")]
    [SerializeField] private float maxSpeed = 10f;    // Velocidade máxima
    [SerializeField] private float acceleration = 15f; // Taxa de aceleração
    [SerializeField] private float deceleration = 20f; // Taxa de desaceleração
    [SerializeField] private float friction = 5f;      // Atrito quando parado
    
    [Header("Pulo")]
    [SerializeField] private float jumpSpeed = 10f;
    [SerializeField] private float jumpShortSpeed = 5f;
    
    [Header("Configurações")]
    [SerializeField] private LayerMask chaoLayer;
    [SerializeField] private Transform peCheck;
    [SerializeField] private float raioChecagem = 0.2f;

    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer spriteRenderer;
    private bool noChao;
    private bool jump;
    private bool jumpCancel;
    private float movimentoX;
    private bool facingRight = true; // Direção atual do personagem

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Input e flip do sprite
        movimentoX = Input.GetAxisRaw("Horizontal");
        UpdateSpriteDirection();

        // Verificação de chão
        noChao = Physics2D.OverlapCircle(peCheck.position, raioChecagem, chaoLayer);

        // Lógica do pulo
        if (Input.GetButtonDown("Jump") && noChao) jump = true;
        if (Input.GetButtonUp("Jump") && !noChao) jumpCancel = true;
    }

    void FixedUpdate()
    {
        // Movimento horizontal com aceleração
        ApplyHorizontalMovement();

        // Lógica do pulo
        HandleJump();
    }

    void ApplyHorizontalMovement()
    {
        float targetSpeed = movimentoX * maxSpeed;
        float speedDiff = targetSpeed - _rigidbody2D.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deceleration;

        // Aplica força para atingir a velocidade alvo
        float movement = Mathf.Pow(Mathf.Abs(speedDiff) * accelRate, 0.96f) * Mathf.Sign(speedDiff);
        _rigidbody2D.AddForce(movement * Vector2.right);

        // Aplica atrito quando parado
        if (Mathf.Abs(movimentoX) < 0.01f)
        {
            float frictionAmount = Mathf.Min(Mathf.Abs(_rigidbody2D.velocity.x), friction);
            frictionAmount *= Mathf.Sign(_rigidbody2D.velocity.x);
            _rigidbody2D.AddForce(-frictionAmount * Vector2.right, ForceMode2D.Impulse);
        }
    }

    void UpdateSpriteDirection()
    {
        // Mantém a direção mesmo quando parado
        if (movimentoX > 0) facingRight = true;
        if (movimentoX < 0) facingRight = false;
        
        spriteRenderer.flipX = !facingRight;
    }

    void HandleJump()
    {
        if (jump)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpSpeed);
            jump = false;
        }

        if (jumpCancel)
        {
            if (_rigidbody2D.velocity.y > jumpShortSpeed)
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpShortSpeed);
            jumpCancel = false;
        }
    }

    // Método para reiniciar do checkpoint
    public void ReiniciarDoCheckpoint()
    {
        if (GameManager.Instance.UltimoCheckpoint != Vector2.zero)
        {
            // Reposiciona o jogador no último checkpoint
            transform.position = GameManager.Instance.UltimoCheckpoint;
        }
        else
        {
            // Se não houver checkpoint, reinicia a cena
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (peCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(peCheck.position, raioChecagem);
        }
    }
}