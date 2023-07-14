using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall : MonoBehaviour
{
    public float moveDistance = 5f;   // Duvar�n hareket edece�i mesafe
    public float moveTime = 2f;       // Duvar�n hareket etmek i�in ge�irece�i s�re
    public float resetDelay = 2f;     // Oyunun bitiminden sonra duvar�n resetlenmesi i�in ge�ecek s�re

    private Vector3 startPosition;   // Duvar�n ba�lang�� konumu
    private Vector3 endPosition;     // Duvar�n son konumu

    private float moveTimer = 0f;     // Duvar�n hareket etme zamanlay�c�s�
    private bool isMoving = false;    // Duvar�n hareket edip etmedi�ini belirten bool de�er

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

