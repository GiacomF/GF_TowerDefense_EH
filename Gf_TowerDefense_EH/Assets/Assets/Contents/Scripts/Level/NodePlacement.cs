using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class NodePlacement : MonoBehaviour
{
    public GameObject ClickToBuildUI;
    public GameObject AlreadyBuiltUI;
    public GameObject TooExpansive;
    public GameObject turret;
    private GameObject spawnedTurret = null;
    public Color hoverColor;
    private Renderer rend;
    private Color startColor;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    void OnMouseDown()
    {
        ClickToBuildUI.SetActive(false);

        if(LevelController.instance.CoinsAvailable < 10 && spawnedTurret == null)
        {
            TooExpansive.SetActive(true);
        }
        else if(spawnedTurret == null)
        {
            spawnedTurret = GameObject.Instantiate(turret, transform.position, transform.rotation);
        spawnedTurret.transform.SetParent(gameObject.transform);
            LevelController.instance.CoinsAvailable -= 10;
        }
        else if(spawnedTurret != null)
        {
            AlreadyBuiltUI.SetActive(true);
        }
    }
    void OnMouseEnter()
    {
        if(turret.activeSelf == true && spawnedTurret == null)
        {
            ClickToBuildUI.SetActive(true);
            rend.material.color = hoverColor;
        }
        else
        {
            
            ClickToBuildUI.SetActive(false);
        }
    }

    void OnMouseExit()
    {
        ClickToBuildUI.SetActive(false);
        AlreadyBuiltUI.SetActive(false);
        TooExpansive.SetActive(false);
        rend.material.color = startColor;
    }
}
