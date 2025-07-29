using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holster : MonoBehaviour
{
    public Playthings Playthings;
    public GameObject Pistol;
    public GameObject Spyglass;
    private Outline GunOutline;
    private Outline SpyglassOutline;
    // Start is called before the first frame update
    void Start()
    {
        GunOutline = Pistol.GetComponent<Outline>();
        SpyglassOutline = Spyglass.GetComponent<Outline>();
    }

    // Update is called once per frame
    void Update() 
    {
       
    }

    public void GunOutlineON() {
        GunOutline.enabled = true;
    }

    public void GunOutlineOFF() {
        GunOutline.enabled = false;
    }

    public void SpyglassOutlineON() {
        SpyglassOutline.enabled = true;
    }

    public void SpyglassOutlineOFF() {
        SpyglassOutline.enabled = false;
    }


    public bool PistolInHolster() {
        return Pistol.activeSelf;
    } 

    public bool SpyglassInHolster() {
        return Spyglass.activeSelf;
    }

    public bool HolsterIsEmpty() {
        return (!Pistol.activeSelf && !Spyglass.activeSelf);
    }

    public bool HolsterHasTool() {
        return (Pistol.activeSelf && Spyglass.activeSelf);
    }
}
