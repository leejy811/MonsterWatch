using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : Enemy
{
    public int shootTimes;
    public float shootCool;
    public float fallCool;
    public Transform cannonParent;
    public GameObject cannonPrefab;

    public override void Attack()
    {
        StartCoroutine(PlayAttack());
    }

    protected override IEnumerator PlayAttack()
    {
        List<Cannon> cannons = new List<Cannon>();

        for (int i = 0;i < shootTimes;i++)
        {
            cannons.Add(Shoot());
            animator.SetTrigger("DoAttack");
            yield return new WaitForSeconds(shootCool);
        }

        for (int i = 0; i < shootTimes; i++)
        {
            cannons[i].Fall();
            yield return new WaitForSeconds(shootCool);
        }
    }

    private Cannon Shoot()
    {
        GameObject cannonObject = Instantiate(cannonPrefab, cannonParent.position, Quaternion.identity, cannonParent);
        Cannon cannon = cannonObject.GetComponent<Cannon>();
        cannon.Shoot(damage, target);
        return cannon;
    }
}