using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun Data", menuName = "Gun")]
public class GunDataSO : ScriptableObject
{
    public int damage;
    public float fireRate;
    public int magazineSize;
    public ParticleSystem muzzleFlash;
    public AudioClip shootSound;
}
