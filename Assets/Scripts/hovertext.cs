using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class hovertext : MonoBehaviour
{
    [SerializeField] Canvas textcanvas;
    [SerializeField] ObjectPool objectpool;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {




        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject go = objectpool.GetObject(textcanvas.gameObject);
                go.transform.position = hit.point;
                StartCoroutine(Up(go));
            }
        }
    }

    IEnumerator Up(GameObject go)
    {
        Debug.Log("anan");
        float timer = 0f;
        while (timer < 1f)
        {
            timer += Time.deltaTime;
            go.transform.Translate(Vector3.up * Time.deltaTime * 20f);
            yield return null;
        }
        Destroy(go);
    }
}
