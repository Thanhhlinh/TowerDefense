using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static int Money;
    public int startMoney = 400;

    public static int Lives;
    public int startLives = 20;

    public static int rounds;

    private void Start()
    {
        Money = startMoney;
        Lives = startLives;
        rounds = 0;
    }
}
