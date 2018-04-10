using UnityEngine;
using System.Collections;

public class Scripts : MonoBehaviour
{
    protected Characters player;
    protected bool isPlayer = true;
    // Use this for initialization
    public virtual void Start()
    {
        if (isPlayer)
        {
            player = GetComponent<Characters>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
