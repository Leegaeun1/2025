using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{

    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    //[SerializeField] [Range(0,1)]
    float movementFactor;
    [SerializeField] float period=2f;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition= transform.position;
        Debug.Log(startingPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (period <Mathf.Epsilon) {  return; }
        float cycles = Time.time / period; //���ʿ� �� ����Ŭ.

        const float tau=Mathf.PI *2; //������*2
        float rawSinWave=Mathf.Sin(cycles*tau); //-1~1 (������ 1�� ���� �����ı׸���)

        movementFactor=(rawSinWave+1f)/2f; //0~1

        Vector3 offset = movementVector * movementFactor; 
        transform.position = startingPosition+offset;
    }
}
