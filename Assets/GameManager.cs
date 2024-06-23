using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Bullet bullet;
    void Start()
    {
        ObjectPooling.Init(bullet);
    }
}
