using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (LangManager.lang == "gr") {
            transform.GetChild(0).Find("title_gr").gameObject.SetActive(true);
            transform.GetChild(0).Find("title_en").gameObject.SetActive(false);
            transform.GetChild(0).Find("description_gr").gameObject.SetActive(true);
            transform.GetChild(0).Find("description_en").gameObject.SetActive(false);
        }
        else {
            transform.GetChild(0).Find("title_gr").gameObject.SetActive(false);
            transform.GetChild(0).Find("title_en").gameObject.SetActive(true);
            transform.GetChild(0).Find("description_gr").gameObject.SetActive(false);
            transform.GetChild(0).Find("description_en").gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseIn(float sec) {
        StartCoroutine(closing(sec));
    }

    IEnumerator closing (float sec) {
        yield return new WaitForSeconds(sec);
        gameObject.SetActive(false);
    }
}
