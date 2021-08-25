using UnityEngine;

public class PlayerController : MonoBehaviour
{
    SpriteRenderer playerSkin;

    Vector2 moveDirection = Vector2.zero;

    [SerializeField] float moveSpeed = 10f;

    private Camera _camera;

    public Vector3 StartPosition { get; set; }

    bool isMove = false;

    [SerializeField] private float moveBackSpeed = 5f;

    Vector2 maxBorder;

    [SerializeField] private bool canKill = false;

    [SerializeField] LayerMask player;

    private void Start()
    {
        maxBorder = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        _camera = Camera.main;

        StartPosition = transform.position;

        playerSkin = GetComponent<SpriteRenderer>();

        if (GameManager.Instance.BallSkin != null)
            playerSkin.sprite = GameManager.Instance.BallSkin;
    }

    private void Update()
    {
        Shoot();
        CheckOutsideBall();

        if (isMove && transform.position != StartPosition)
            transform.position = Vector3.MoveTowards(transform.position, StartPosition, Time.deltaTime * moveBackSpeed);
        else if (transform.position == StartPosition) isMove = false;
    }

    void Shoot()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            //if (touch.phase == TouchPhase.Moved)
            //{
            //    Vector2 positionChange = touch.deltaPosition;
            //    //positionChange.y = -positionChange.y;
            //    moveDirection = positionChange.normalized;
            //}

            for (int i = 0; i < Input.touchCount; i++)
            {
                Vector2 ray = new Vector2(_camera.ScreenToWorldPoint(new Vector3(Input.touches[i].position.x, Input.touches[i].position.y, 0f)).x,
                                          _camera.ScreenToWorldPoint(new Vector3(Input.touches[i].position.x, Input.touches[i].position.y, 0f)).y);

                RaycastHit2D hit2D = Physics2D.Raycast(ray, Vector2.zero, 0.5f, player);
                if (hit2D)
                {
                    Vector2 positionChange;
                    Vector2 startPos = StartPosition;
                    if (touch.phase == TouchPhase.Began)
                    {
                        startPos = touch.position;
                    }
                    if (touch.phase == TouchPhase.Moved)
                    {
                        positionChange = touch.deltaPosition - startPos;
                        //positionChange.y = -positionChange.y;
                        moveDirection = positionChange.normalized;
                    }
                }
            }
        }
        //transform.position += (Vector3)moveDirection * -moveSpeed * Time.deltaTime;
        transform.Translate((Vector3)moveDirection * -moveSpeed * Time.deltaTime);
    }

    void CheckOutsideBall()
    {
        //проверяю на столкновение с границей экрана:
        if (Mathf.Abs(transform.position.x) > maxBorder.x
            || Mathf.Abs(transform.position.x) > maxBorder.x)
        {
            print("overX");
            GameHUD.Instance.ShowLoseScreen();
            moveDirection = Vector2.zero;
            transform.position = StartPosition;
        }
        else if (Mathf.Abs(transform.position.y) > maxBorder.y
                || Mathf.Abs(transform.position.y) > maxBorder.y)
        {
            print("overY");
            GameHUD.Instance.ShowLoseScreen();
            moveDirection = Vector2.zero;
            transform.position = StartPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canKill)
        {
            canKill = false;
            return;
        }

        if (collision.GetComponent<CockroachController>())
        {
            Destroy(collision.gameObject);
            GameManager.Instance.Score++;
            GameManager.Instance.Last_Score++;
            GameManager.Instance.Money++;
            moveDirection = Vector2.zero;
            isMove = true;
            canKill = true;
        }
    }
}