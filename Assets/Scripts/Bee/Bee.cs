﻿using UnityEngine;

public class Bee : MonoBehaviour
{
    [SerializeField,Tooltip("Скорость пчелы")] public float speed = 1.5f;

    [HideInInspector] public Vector2 target;
    [HideInInspector] public Hive hive;
    
    protected void MoveTo()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    protected void Rotation()
    {
        transform.localScale = target.x > transform.position.x
            ? new Vector3(-1.0f, 1.0f, 1.0f)
            : new Vector3(1.0f, 1.0f, 1.0f);
    }
}