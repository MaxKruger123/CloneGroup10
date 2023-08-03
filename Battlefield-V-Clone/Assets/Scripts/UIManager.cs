using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField]
    public Text _ammoText;
    public int fullAmmo = 270;
    public GameObject player;
    public GunController gunController;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gunController = player.GetComponent<GunController>();
    }
    void Update()
    {
        
        _ammoText.text =gunController._currentAmmoInClip.ToString() +"/" + gunController._ammoInReserve.ToString();
    }

    public void UpdateAmmo(int count)
    {
        _ammoText.text = count + " /270";
    }
}
