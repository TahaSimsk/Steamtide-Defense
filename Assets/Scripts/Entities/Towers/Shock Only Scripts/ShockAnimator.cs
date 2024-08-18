using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ShockAnimator : MonoBehaviour
{

    [SerializeField] Texture[] textures;
    [SerializeField] float animSpeed;
    LineRenderer lineRenderer;


    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }


    private void OnEnable()
    {
       StartCoroutine(AnimateLine());
    }


    IEnumerator AnimateLine()
    {
        while (true)
        {

            foreach (var item in textures)
            {

                lineRenderer.material.SetTexture("_MainTex", item);
                yield return new WaitForSeconds(animSpeed);
                Debug.Log("anan");
            }

        }

    }



}
