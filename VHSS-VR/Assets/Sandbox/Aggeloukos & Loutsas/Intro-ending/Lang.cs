using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lang : MonoBehaviour {
    private LayerMask Anchors;
    public GameObject[] Auxes;
    public Scenario Scenario;
    public Playthings Playthings;
    public GameObject player;
    public GameObject Area;
    public GameObject gun;
    public GameObject EnglishFlag;
    public GameObject GreekFlag;
    public GameObject ShipMesh;
    public Exploration exploration;
    public GameObject Holster1;
    public GameObject Holster2;
    public bool Gre;
    // Start is called before the first frame update
    void Start() {
        //ShipMesh.SetActive(false);
        Anchors = LayerMask.GetMask("Anchors");
        player.transform.position = Area.transform.position;
        Playthings.BareHands();
        //ShipMesh.SetActive(false);
        gun.SetActive(false);
        Playthings.PistolRight.SetActive(true);
        exploration.TeleportR.TeleportToPoint.Enable();
        exploration.TeleportL.TeleportToPoint.Enable();
        Holster1.SetActive(false);
        Holster2.SetActive(false);

    }

    // Update is called once per frame
    void Update() {

    }

    public void Greek() {
        Scenario.english = false;
        Scenario.LangSelection();
        Scenario.EnterScene("Intro", Scenario.Dialogue);
        ShipMesh.SetActive(true);
        Auxes[0].SetActive(false);
        Auxes[1].SetActive(false);
        exploration.TeleportR.TeleportToPoint.Disable();
        exploration.TeleportL.TeleportToPoint.Disable();
        Holster1.SetActive(true);
        Holster2.SetActive(true);
    }

    public void English() {
        Scenario.english = true;
        Scenario.LangSelection();
        Scenario.EnterScene("Intro", Scenario.Dialogue);
        ShipMesh.SetActive(true);
        Auxes[0].SetActive(false);
        Auxes[1].SetActive(false);
        exploration.TeleportR.TeleportToPoint.Disable();
        exploration.TeleportL.TeleportToPoint.Disable();
        Holster1.SetActive(true);
        Holster2.SetActive(true);
    }
    public void GreekOutline() {
        GreekFlag.GetComponent<Outline>().enabled = true;
    }

    public void EnglishOutline() {
        EnglishFlag.GetComponent<Outline>().enabled = true;
    }

    public void GreekOutlineOFF() {
        GreekFlag.GetComponent<Outline>().enabled = false;
    }

    public void EnglishOutlineOFF() {
        EnglishFlag.GetComponent<Outline>().enabled = false;
    }

}
