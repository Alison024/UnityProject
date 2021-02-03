using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public interface IWeapon
{
    GameObject GetStartBulletPostion();
    [Command]
    void BulletShoot(Vector2 vector);
}
