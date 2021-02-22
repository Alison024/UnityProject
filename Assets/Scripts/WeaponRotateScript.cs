using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class WeaponRotateScript : NetworkBehaviour
{
    // Start is called before the first frame update
    private GameObject weapon;
    void Start()
    {
        Transform transform = this.transform.Find("Weapon");
        if(transform != null)
        {
            weapon = transform.gameObject;
        }
        else
        {
            weapon = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        //Get the Screen positions of the object
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(weapon.transform.position);
        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
        //Get the angle between the points
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
        //Ta Daaa
        weapon.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

    }
    private float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(b.y - a.y, b.x - a.x) * Mathf.Rad2Deg;
    }
}
