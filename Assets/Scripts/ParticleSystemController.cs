using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemController : MonoBehaviour
{
    [Header("Particle Prefabs")]
    public GameObject deathParticlePrefab;
    [Space]
    public GameObject dualityParticlePrefab;
    [Space]
    public GameObject jumpParticlePrefab;
    [Space]
    public GameObject doublejumpParticlePrefab;
    [Space]
    public GameObject hitgroundParticlePrefab;
    [Space]
    public GameObject phaseplatformParticlePrefab;


    public static ParticleSystemController particleContoller;

    private void Awake() { particleContoller = this; }


    public void SpawnParticleSystem(ParticleType type, Vector3 position)
    {
        GameObject particle = null;

        switch(type)
        {
            case ParticleType.DEATH:
                particle = deathParticlePrefab;
                break;

            case ParticleType.DUALITY:
                particle = dualityParticlePrefab;
                break;

            case ParticleType.JUMP:
                particle = jumpParticlePrefab;
                break;

            case ParticleType.DOUBLEJUMP:
                particle = doublejumpParticlePrefab;
                break;

            case ParticleType.HITGROUND:
                particle = hitgroundParticlePrefab;
                break;

            case ParticleType.PHASEPLATFORM:
                particle = phaseplatformParticlePrefab;
                break;

            default:
                return;
        }

        if(particle == null)
        {
            Debug.LogError("ParticleSystemController: Particle Prefab was not added to ParticleSystemController");
            return;
        }

        Instantiate(particle, position, Quaternion.identity);
    }

}
