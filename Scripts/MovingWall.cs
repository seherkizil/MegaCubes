using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall : MonoBehaviour
{
    public float moveDistance = 5f;   // Duvarýn hareket edeceði mesafe
    public float moveTime = 2f;       // Duvarýn hareket etmek için geçireceði süre
    public float resetDelay = 2f;     // Oyunun bitiminden sonra duvarýn resetlenmesi için geçecek süre

    private Vector3 startPosition;   // Duvarýn baþlangýç konumu
    private Vector3 endPosition;     // Duvarýn son konumu

    private float moveTimer = 0f;     // Duvarýn hareket etme zamanlayýcýsý
    private bool isMoving = false;    // Duvarýn hareket edip etmediðini belirten bool deðer

    private void Start()
    {
        startPosition = transform.position;
        endPosition = transform.position - new Vector3(0f, 0f, moveDistance);
    }

    private void Update()
    {
        if (!isMoving)
        {
            moveTimer += Time.deltaTime;

            if (moveTimer >= 15f)
            {
                isMoving = true;
                moveTimer = 0f;
                StartCoroutine(MoveWall());
            }
        }
    }

    private IEnumerator MoveWall()
    {
        float elapsedTime = 0f;

        while (elapsedTime < moveTime)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / moveTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isMoving = false;
        yield return new WaitForSeconds(resetDelay);

        elapsedTime = 0f;

        while (elapsedTime < moveTime)
        {
            transform.position = Vector3.Lerp(endPosition, startPosition, elapsedTime / moveTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = startPosition;
    }

}

