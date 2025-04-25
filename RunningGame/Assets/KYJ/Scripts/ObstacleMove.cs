using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    [SerializeField] float speed = 20f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        //if(transform.position.x <= -10.75f) // -10.75f보다 x좌표 값이 작아지면 장애물 오브젝트 비활성화
        //{
        //    obstacle.SetActive(false);
        //}
    }
}
