using OTBG.Gameplay.Player.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderApplication : MonoBehaviour
{
    public List<Material> materials = new List<Material>();

    // Start is called before the first frame update
    void Awake()
    {
        this.GetComponent<SpriteRenderer>().material = materials[0];
    }

    // Update is called once per frame
    void Update()
    {
        bool dyingFlag = this.GetComponentInParent<HealthController>().readyToDie;
        if (dyingFlag)
        {
            this.GetComponent<SpriteRenderer>().material = materials[1];

        }
    }
}
