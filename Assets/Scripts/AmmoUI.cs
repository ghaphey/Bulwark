using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    [SerializeField] private Slider reloadBar = null;
    [SerializeField] private Image reloadBarImage = null;
    [SerializeField] private Text ammoText = null;

    private GameObject currAmmoImage = null;
    private List<GameObject> ammoList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        ammoText.text = "Inf";
        reloadBarImage.enabled = false;
    }

    public void SetAmmoImage(GameObject image)
    {
        currAmmoImage = image;
    }

    public void ResetAmmoPanel(GameObject ammoImage, int newMagSize, float widthOffset)
    {
        if (ammoList.Count > 0)
        {
            foreach (GameObject a in ammoList)
            {
                Destroy(a);
            }
            ammoList.Clear();
        }
        currAmmoImage = ammoImage;
        for (int i = 0; i < newMagSize; i++)
        {
            ammoList.Add(Instantiate(ammoImage, transform));
            RectTransform nAmmo = ammoList[i].GetComponent<RectTransform>();
            nAmmo.Translate(new Vector3(widthOffset * -i * 1.25f, 0f));
        }
    }

    public void ReloadAmmo( int numReloaded)
    {
        for(int i = 0; i < numReloaded; i++)
        {
            ammoList[i].SetActive(true);
        }
        reloadBarImage.enabled = false;
    }

    public void UsedAmmo(int usedIndex)
    {
        if(usedIndex >= 0)
            ammoList[usedIndex].SetActive(false);
    }

    public void SetText(string newText)
    {
        ammoText.text = newText;
    }

    public void AdjustReloadSlider(float newValue)
    {
        reloadBarImage.enabled = true;
        reloadBar.value = newValue;
    }
}
