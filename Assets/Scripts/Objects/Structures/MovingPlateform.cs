using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlateform : MonoBehaviour
{


    float _speed;
    public Transform plateform;
    public Transform parentPath;
    Transform[] path;
    int positionSelected;


    // Use this for initialization
    void Awake()
    {
        _speed = 2f;
        path = parentPath.GetComponentsInChildren<Transform>();
        positionSelected = 2;
    }

    // Update is called once per frame
    void Update()
    {

        plateform.position = Vector3.MoveTowards(plateform.position, path[positionSelected].position, Time.deltaTime * _speed);
        if(plateform.position == path[positionSelected].position)
        {
            if(positionSelected == path.Length-1)
            {
                positionSelected = 1;
            }
            else
            {
                positionSelected += 1;
            }
        }
    }


}
