using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public GameObject player;
    private Damageable damageable;
    public Image foreground;
    // Start is called before the first frame update
    void Start()
    {
        damageable = player.GetComponent<Damageable>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 scale = foreground.transform.localScale;
        scale.x = damageable.CalHealthRatio();
        foreground.transform.localScale = scale;
    }
}
