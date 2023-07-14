using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RedZone : MonoBehaviour
{
    private bool gameOver = false;

    void OnTriggerEnter(Collider other)
    {
        Cube cube = other.GetComponent<Cube>();

        if (cube != null && other.CompareTag("cube") && !gameOver)
        {

            if (!cube.isMainCube)
            {
                // Küp çarpýþtýktan 2 saniye sonra oyunu bitir
                Invoke("GameOver", 2f);
                gameOver = true;
            }
        }
    }

    void GameOver()
    {
        // Uyarý mesajý göster
        Debug.Log("Oyun Bitti!");

        // Diðer sahneye geç
        SceneManager.LoadScene("BaskaBirSahne");
    }










   







}
