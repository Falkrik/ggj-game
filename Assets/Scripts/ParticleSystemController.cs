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
    [Space]
    public GameObject pushParticlePrefab;


    public static ParticleSystemController particleContoller;

    private void Awake() { particleContoller = this; }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
            particleContoller.SpawnParticleSystem(ParticleType.DEATH, new Vector3(0, -5, 0));
        if (Input.GetKeyUp(KeyCode.Alpha2))
            particleContoller.SpawnParticleSystem(ParticleType.DUALITY, new Vector3(0, 0, 0));
        if (Input.GetKeyUp(KeyCode.Alpha3))
            particleContoller.SpawnParticleSystem(ParticleType.JUMP, new Vector3(0, 0, 0));
        if (Input.GetKeyUp(KeyCode.Alpha4))
            particleContoller.SpawnParticleSystem(ParticleType.DOUBLEJUMP, new Vector3(0, 0, 0));
        if (Input.GetKeyUp(KeyCode.Alpha5))
            particleContoller.SpawnParticleSystem(ParticleType.HITGROUND, new Vector3(0, 0, 0));
        if (Input.GetKeyUp(KeyCode.Alpha6))
            particleContoller.SpawnParticleSystem(ParticleType.PHASEPLATFORM, new Vector3(0, 0, 0));
        if (Input.GetKeyUp(KeyCode.Alpha7))
            particleContoller.SpawnParticleSystem(ParticleType.PUSH, new Vector3(0, 0, 0));

    }

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

            case ParticleType.PUSH:
                particle = pushParticlePrefab;
                break;

            default:
                return;
        }

        if(particle == null)
        {
            Debug.LogError("ParticleSystemController: Particle Prefab was not added to ParticleSystemController");
            return;
        }

        Destroy(Instantiate(particle, position, particle.transform.rotation), 2);
    }

}
