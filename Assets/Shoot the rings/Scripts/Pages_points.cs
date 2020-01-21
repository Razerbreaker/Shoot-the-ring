using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pages_points : MonoBehaviour
{
    public GameObject pagePointPrefab;
    public int totalCountOfPages;           // эти значения в start передает  LvlButtonsHandler
    public int currentPage;                 // эти значения в start передает  LvlButtonsHandler

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        //while (transform.childCount > 0)
        //{

        //    Destroy(GetChild(transform.childCount - 1).gameObject);
        //}
        transform.DetachChildren();

        for (int i = 0; i <= totalCountOfPages; i++)
        {
            Instantiate(pagePointPrefab, transform).name = i.ToString();
        }
        transform.GetChild(currentPage).GetComponent<Animator>().SetTrigger("to active");
    }

    public void NextPage()
    {
        transform.GetChild(currentPage).GetComponent<Animator>().SetTrigger("to not active");
        currentPage++;
        transform.GetChild(currentPage).GetComponent<Animator>().SetTrigger("to active");

    }
    public void PreviousPage()
    {
        transform.GetChild(currentPage).GetComponent<Animator>().SetTrigger("to not active");
        currentPage--;
        transform.GetChild(currentPage).GetComponent<Animator>().SetTrigger("to active");
    }
}
