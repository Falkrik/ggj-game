using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemController : MonoBehaviour
{
    [Header("Particle Prefabs")]
    public GameObject deathParticlePrefab;

    public static ParticleSystemController particleContoller;


    public void SpawnParticleSystem(ParticleType type, Vector3 position)
    {
        switch(type)
        {
            case ParticleType.DEATH:
                //
                break;

            case ParticleType.DUALITY:
                //
                break;

            case ParticleType.JUMP:
                //
                break;

            case ParticleType.DOUBLEJUMP:
                //
                break;

            case ParticleType.HITGROUND:
                //
                break;

            case ParticleType.PHASEPLATFORM:
                //
                break;
        }
    }

}
