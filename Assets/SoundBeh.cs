using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBeh : MonoBehaviour
{
    FieldOfView view;
    [SerializeField] float maxView;
    bool goBack;

    private void Start()
    {
        view = GetComponent<FieldOfView>();
    }

    private void Update()
    {
        if (!goBack)
        {
            view.viewRadius = Mathf.Lerp(view.viewRadius, maxView, Time.deltaTime);
            if (maxView - view.viewRadius < 1f)
            {
                goBack = true;
            }
        }
        if (goBack)
        {
            view.viewRadius = Mathf.Lerp(view.viewRadius, 0, Time.deltaTime);
            if (view.viewRadius < 1f)
            {
                goBack = false;
            }
        }

    }
}
