using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject startWeapon;

    public PlayerController playerController;
    public Transform weaponSlot;
    

    void Start()
    {
        TakeWeapon(startWeapon);
    }

    public void TakeWeapon(GameObject weaponObjectPrefab)
    {
        GameObject cloneWeaponObject = Instantiate(weaponObjectPrefab, weaponSlot);
        Weapon weapon = cloneWeaponObject.GetComponent<Weapon>();
        playerController.EquipWeapon(weapon);
    }
}
