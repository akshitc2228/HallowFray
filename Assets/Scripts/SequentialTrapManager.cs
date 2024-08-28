using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequentialTrapManager : MonoBehaviour
{
    [SerializeField]
    public Transform[] startPositions;

    private int noOfTraps;
    private int countLandedTraps = 0;
    private int countAscendedTraps = 0;

    private float descendSpeed;
    private float returnSpeed;

    private bool[] isTrapMoving; // Array to track if traps are moving

    // Start is called before the first frame update
    void Start()
    {
        noOfTraps = startPositions.Length;
        descendSpeed = 30f;
        returnSpeed = 5.0f;

        isTrapMoving = new bool[noOfTraps]; // Initialize the array
        for(int i=0; i<isTrapMoving.Length; i++)
        {
            isTrapMoving[i] = true;
        }
    }

    void FixedUpdate()
    {
        StartCoroutine(trapsDescent());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        for (int i = 0; i < noOfTraps; i++)
        {
            if (collision.collider.CompareTag("Ground") || collision.collider.CompareTag("trapsUpperBound"))
            {
                // Stop moving the trapdoor when it hits the ground or upper bound
                isTrapMoving[i] = false;
            }
        }
    }

    IEnumerator trapsDescent()
    {
        for (int i = 0; i < noOfTraps; i++)
        {
            if (!isTrapMoving[i]) continue; // Skip this trap if it's not moving

            startPositions[i].Translate(Vector3.left * descendSpeed * Time.deltaTime); //left cause I changed the axis by rotating

            if (countLandedTraps == noOfTraps)
            {
                countLandedTraps = 0;
                StartCoroutine(trapsAscent());
                break;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator trapsAscent()
    {
        yield return new WaitForSeconds(1.3f);

        for (int i = 0; i < noOfTraps; i++)
        {
            if (!isTrapMoving[i]) continue; // Skip this trap if it's not moving

            startPositions[i].Translate(Vector3.right * returnSpeed * Time.deltaTime);

            if (countAscendedTraps == noOfTraps)
            {
                countAscendedTraps = 0;
                StartCoroutine(trapsDescent());
                break;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
