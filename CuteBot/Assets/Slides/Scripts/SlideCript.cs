using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SlideCript : MonoBehaviour {

	public int ending = 0;	

    public GameObject bild;

    public void NextBild()
    {
		if (ending > 0) {

			EndingManager.instance.ActivateEnding (ending);
		}

        bild.SetActive(true);
        this.transform.parent.gameObject.SetActive(false);
    }

	public void TheEnd()
	{
		SceneManager.LoadScene (1);
	}
}
