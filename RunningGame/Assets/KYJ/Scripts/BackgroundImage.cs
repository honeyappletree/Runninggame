using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundImage : MonoBehaviour
{
    public float speed = 20f;
    float repeatDistance; //배경이 반복되는 거리
    Vector2 startPosition; //배경의 초기 위치
    float newPosition; //배경의 새로운 위치 계산

    void Start()
    {
        startPosition = transform.position;
        repeatDistance = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        newPosition = Mathf.Repeat(Time.time * speed, repeatDistance);
        transform.position = startPosition + Vector2.left * newPosition;
    }
}
