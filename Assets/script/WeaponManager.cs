using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject rifle;

    public PlayerController playerController;
    public Transform weaponSlot;

    void Start()
    {
        TakeWeapon(rifle);
    }

    public void TakeWeapon(GameObject weaponObject)
    {
        GameObject cloneWeaponObject = Instantiate(weaponObject, weaponSlot);
        Weapon weapon = cloneWeaponObject.GetComponent<Weapon>();
        playerController.EquipWeapon(weapon);
    }
}
