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
                // K�p �arp��t�ktan 2 saniye sonra oyunu bitir
                Invoke("GameOver", 2f);
                gameOver = true;
            }
        }
    }

    void GameOver()
    {
        // Uyar� mesaj� g�ster
        Debug.Log("Oyun Bitti!");

        // Di�er sahneye ge�
        SceneManager.LoadScene("BaskaBirSahne");
    }










   







}
