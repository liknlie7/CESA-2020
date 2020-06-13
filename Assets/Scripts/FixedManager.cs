using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedManager : MonoBehaviour
{
    private static FixedManager instance;

    public GameObject menuCanvas;
    public RippleManager rippleManager;
    public GameOverStaging gameOverStaging;

    public ScoreManager scoreManager;
    public EnemyManager enemyManager;

    public CameraIntensity intensityManager;

    private void Awake()
    {
        instance = this;
    }

    public static FixedManager Get()
    {
        return instance;
    }
}
