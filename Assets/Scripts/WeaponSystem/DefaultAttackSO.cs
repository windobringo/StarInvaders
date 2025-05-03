using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PA.WeaponSystem;

[CreateAssetMenu(menuName = "Attacks/DefaultAttack")]
public class DefaultAttackSO : AttackPatternSO
{
    private GameObject projectile2;

    public override void Perform(Transform shootingStartPoint)
	{
        projectile2 = Instantiate(projectile, shootingStartPoint.position, shootingStartPoint.rotation);
		Player controller = FindObjectOfType<Player>();

		if (controller != null && controller.isFast == true)
		{
			projectile2.GetComponent<Projectile>().speed = 30f;
			controller.isFast = false;
		}

	}

  

}
