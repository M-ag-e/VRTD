using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
public class Crossbow : MonoBehaviour
{
    public GameObject boltVisual;
    public TowerProjectile boltProjectile;
    public GameObject shootPoint;
    public Hand hand;
    public bool loaded = true;
    private void FixedUpdate()
    {
        if (hand.grabGripAction.state)
        {
            if (loaded)
            {
                var bolt = Instantiate(boltProjectile);
                bolt.transform.position = boltVisual.transform.position;
                bolt.transform.LookAt(shootPoint.transform);
                loaded = false;
                boltVisual.SetActive(false);
                StartCoroutine(Reload());
            }
            
        }
    }
    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(2f);
        loaded = true;
        boltVisual.SetActive(true);
    }

}
