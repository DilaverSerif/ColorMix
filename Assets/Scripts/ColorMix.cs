using System;
using System.Collections;
using System.Collections.Generic;
using Obi;
using UnityEngine;

public class ColorMix : MonoBehaviour
{
    private ObiEmitter _emitter;
    private ObiSolver _solver;

    private void Awake()
    {
        _solver = FindObjectOfType<ObiSolver>();
        _emitter = GetComponent<ObiEmitter>();
    }
    
    void LateUpdate()
    {
        for (int i = 0; i < _solver.userData.count; ++i)
            _solver.colors[i] = _solver.userData[i];
    }

    // void Start()
    // {
    //     _solver.OnCollision += Solver_OnCollision;
    //     _emitter.OnEmitParticle += Emitter_OnEmitParticle;
    // }
    //
    // private void OnDestroy()
    // {
    //     _solver.OnCollision -= Solver_OnCollision;
    //     _emitter.OnEmitParticle -= Emitter_OnEmitParticle;
    // }
    //
    // void Emitter_OnEmitParticle(ObiEmitter em, int particleIndex)
    // {
    //     int k = _emitter.solverIndices[particleIndex];
    //     _solver.userData[k] = _solver.colors[k];
    // }
    //
    // private void Solver_OnCollision(ObiSolver s, ObiSolver.ObiCollisionEventArgs e)
    // {
    //     var world = ObiColliderWorld.GetInstance();
    //     foreach (Oni.Contact contact in e.contacts)
    //     {
    //         // look for actual contacts only:
    //         if (contact.distance < 0.01f)
    //         {
    //             var col = world.colliderHandles[contact.bodyB].owner;
    //             if (colorizers[0].collider == col)
    //             {
    //                 solver.userData[contact.bodyA] = colorizers[0].color;
    //                 if (coloredParticles.Add(contact.bodyA))
    //                     UpdateScore(finishedParticles.Count, coloredParticles.Count);
    //             }
    //         }
    //     }
    // }
}