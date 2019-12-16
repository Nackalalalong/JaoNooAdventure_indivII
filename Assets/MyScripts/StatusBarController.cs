using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBarController : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    public Image foreground;
    private PlayerStatus playerStatus;
    void Start()
    {
        playerStatus = player.GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 scale = foreground.transform.localScale;
        scale.x = playerStatus.CalStatusRatio();
        foreground.transform.localScale = scale;
    }
}
