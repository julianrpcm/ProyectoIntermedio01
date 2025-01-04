using UnityEngine;

public class AmmoPack : MonoBehaviour
{
    private enum typeOfAmmo
    {
        smgAmmo,
        sgAmmo,
        glAmmo
    }

    [SerializeField] private typeOfAmmo ammo;
    private WeaponManager wm;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && other.GetComponent<WeaponManager>() != null)
        {
            wm = other.GetComponent<WeaponManager>();
            if (ammo == typeOfAmmo.smgAmmo)
                wm.subMachineGunTotalAmmo += wm.subMachineGunCharger;
            else if (ammo == typeOfAmmo.sgAmmo)
                wm.shotgunTotalAmmo += wm.shotgunCharger;
            else if (ammo == typeOfAmmo.glAmmo)
                wm.grenadeLauncherTotalAmmo += wm.grenadeLauncherCharger;

            Destroy(gameObject);
        }
    }
}