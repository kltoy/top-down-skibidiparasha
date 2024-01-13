using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage;
    public float ammo;
    public float firerate;
    public float radius;

    public GameObject bullet_prefab;
    public GameObject VFX;
    public Transform SHOOTPOINT;
}
