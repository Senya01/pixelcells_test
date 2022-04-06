using UnityEngine;
using System.Collections.Generic;

public class Bee : MonoBehaviour
{
    public Vector2 _target;
    [SerializeField] public float radius = 5;
    [SerializeField] public float speed = 1.5f;

    public Hive hive;
}