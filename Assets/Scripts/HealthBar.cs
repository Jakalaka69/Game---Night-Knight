using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.GridLayoutGroup;
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
   
    [SerializeField] private GameObject target;
   

    // Update is called once per frame

    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
    }
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {


        transform.parent.LookAt(target.transform.position);

    }
}
