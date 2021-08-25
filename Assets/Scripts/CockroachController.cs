using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CockroachController : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    private Vector2 minBorder;
    private Vector2 maxBorder;

    private Vector2 direction;

    void Start()
    {
        minBorder = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        maxBorder = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        moveSpeed = Random.Range(1f, 8f);

        var dir = Random.Range(-1, 2);
        if (dir == 0)
            dir = -1;

        direction.x = dir;

        if (direction.x == -1)
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1);
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        transform.Translate(direction * moveSpeed * Time.fixedDeltaTime);

        //проверяю на столкновение с границей экрана:
        if (Mathf.Abs(transform.position.x) + transform.localScale.x / 4f > maxBorder.x
            || Mathf.Abs(transform.position.x) - transform.localScale.x / 4f > maxBorder.x)
        {
            direction = new Vector2(-direction.x, direction.y);
            var newScale = transform.localScale.x;
            newScale *= -1f;
            transform.localScale = new Vector3(newScale, transform.localScale.y, 1);
        }
        else if (Mathf.Abs(transform.position.y) + transform.localScale.y / 4f > maxBorder.y
                || Mathf.Abs(transform.position.y) - transform.localScale.y / 4f > maxBorder.y)
        {
            direction = new Vector2(-direction.x, direction.y);
            var newScale = transform.localScale.x;
            newScale *= -1f;
            transform.localScale = new Vector3(newScale, transform.localScale.y, 1);
        }
    }
}