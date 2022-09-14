using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Obi;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ObiSolver solver;

    HashSet<int> finishedParticles = new HashSet<int>();

    public ObiCollider finishLine;

    public Color MatchColor;
    public List<Color> WaterColors = new List<Color>();
    public Color AvargeColor;
    private bool finish;

    public static GameManager Instance;
    public float MatchPercentage;

    private void Awake()
    {
        Instance = this;
        solver = GetComponent<ObiSolver>();
    }

    private void OnDisable()
    {
        Instance = null;
    }

    void Start()
    {
        solver.OnCollision += Solver_OnCollision;
        
        FindObjectOfType<Kab>().SetColor(MatchColor);
    }

    private void Solver_OnCollision(ObiSolver obiSolver, ObiSolver.ObiCollisionEventArgs contacts)
    {
        var world = ObiColliderWorld.GetInstance();
        foreach (Oni.Contact contact in contacts.contacts)
        {
            // look for actual contacts only:
            if (contact.distance < 0.01f)
            {
                var col = world.colliderHandles[contact.bodyB].owner;

                // if (!finish)
                // {
                //     
                // }

                if (finishLine == col)
                {
                    if (finishedParticles.Add(contact.bodyA))
                    {
                        WaterColors.Add(solver.colors[contact.bodyA]);
                        if (finishedParticles.Count > solver.allocParticleCount / 5)
                        {
                            Debug.Log("OKAY");

                            if (!finish)
                            {
                                finish = true;

                                DOVirtual.DelayedCall(3, () =>
                                {
                                    AvargeColor /= finishedParticles.Count;
                                    finish = true;
                                    CheckColor();
                                });
                            }
                        }
                    }
                }
            }
        }
    }


    private void CheckColor()
    {
        // // var a = SetAvargeColor - AvargeColor;
        // Debug.Log(AvargeColor /= finishedParticles.Count);
        // Debug.Log(AvargeColor);

        AvargeColor = SetAvargeColor(WaterColors.ToArray());
        Debug.Log(AvargeColor);
        MatchPercentage = GetColorDistance(MatchColor, AvargeColor);

        if (MatchPercentage > 0.5f)
            Base.FinisGame(GameStat.Lose, 1.5f);
        else Base.FinisGame(GameStat.Win, 1.5f);
    }


    public Color SetAvargeColor(Color[] colors)
    {
        float r = 0;
        float g = 0;
        float b = 0;
        float a = 0;
        for (int i = 0; i < colors.Length; i++)
        {
            r += colors[i].r;
            g += colors[i].g;
            b += colors[i].b;
            a += colors[i].a;
        }

        r /= colors.Length;
        g /= colors.Length;
        b /= colors.Length;
        a /= colors.Length;

        return new Color(r, g, b, a);
    }
    

    [Button]
    private float GetColorDistance(Color a, Color b)
    {
        return Mathf.Sqrt(Mathf.Pow(a.r - b.r, 2) + Mathf.Pow(a.g - b.g, 2) + Mathf.Pow(a.b - b.b, 2));
    }

    private void OnDestroy()
    {
        solver.OnCollision -= Solver_OnCollision;
    }
}