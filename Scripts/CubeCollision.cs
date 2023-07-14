using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CubeCollision : MonoBehaviour
{
    Cube cube;

    private void Awake()
    {
        cube = GetComponent<Cube>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Cube otherCube = collision.gameObject.GetComponent<Cube>();

        //Diðer küplerle temasa geçmesini kontrol et
        if (otherCube != null && cube.cubeID > otherCube.cubeID)
        {
            // Küplerin ayný numaralý olmasýný kontrol et
            if (cube.cubeNumber == otherCube.cubeNumber)
            {
                Debug.Log("Hit: " + cube.cubeNumber);
                Vector3 contactPoint = collision.contacts[0].point;

                 // check if cubes number less than max number in CubeSpawner
                if (otherCube.cubeNumber < CubeSpawner.Instance.maxCubeNumber)
                {
                    //spawn a new cube as a result
                    Cube newCube = CubeSpawner.Instance.Spawn(cube.cubeNumber * 2, contactPoint + Vector3.up * 1.6f);

                    //push the new cube up and forward
                    float pushForce = 2.5f;
                    newCube.cubeRigidbody.AddForce(new Vector3(0, 0.3f, 1f) * pushForce, ForceMode.Impulse);

                    // add some torque
                    float randomValue = Random.Range(-20f, 20f);
                    Vector3 randomDirection = Vector3.one * randomValue;
                    newCube.cubeRigidbody.AddTorque(randomDirection);

                }

                // the explosion should affect surrounded cubes too
                Collider[] surroundedCubes = Physics.OverlapSphere(contactPoint, 2f); //Tespit edilen çarpýþmalarý bir collider dizisi içinde tutuyoruz. contactPoint çarpýþmanýn gerçekleþtiði nokta.
                float explosionForce = 400f;
                float explosionRadius = 1.5f;

                foreach (Collider coll in surroundedCubes)
                {
                    if (coll.attachedRigidbody != null )
                    {
                        coll.attachedRigidbody.AddExplosionForce(explosionForce, contactPoint, explosionRadius);
                        // Rigidbody si olan bir cisme patlama efektlerini simule eden bir kuvvet uygular.
                    }
                }

                //explosion FX
                FX.Instance.PlayCubeExplosionFX(contactPoint, cube.cubeColor);





                //Destroy the two cubes
                CubeSpawner.Instance.DestroyCube(cube);
                CubeSpawner.Instance.DestroyCube(otherCube);





            }
        }

        
    }





}
