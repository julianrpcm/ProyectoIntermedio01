using UnityEngine;

public class AmmoPack : MonoBehaviour
{
    private enum typeOfAmmo
    {
        smgAmmo,
        sgAmmo,
        glAmmo,
        deAmmo
    }

    [SerializeField] private typeOfAmmo ammo;
    private WeaponManager wm;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && other.GetComponent<WeaponManager>() != null)
        {
            wm = other.GetComponent<WeaponManager>();

            switch (ammo)
            {
                case typeOfAmmo.smgAmmo:
                    wm.subMachineGunTotalAmmo += wm.subMachineGunCharger;
                    break;
                case typeOfAmmo.sgAmmo:
                    wm.shotgunTotalAmmo += wm.shotgunCharger;
                    break;
                case typeOfAmmo.glAmmo:
                    wm.grenadeLauncherTotalAmmo += wm.grenadeLauncherCharger;
                    break;
                case typeOfAmmo.deAmmo:
                    wm.desertEagleTotalAmmo += wm.desertEagleCharger;
                    break;
                default:
                    break;
            }

            Destroy(gameObject);
        }
    }
}