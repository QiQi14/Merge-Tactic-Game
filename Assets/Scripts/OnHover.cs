using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnHover : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [SerializeField] private AudioSource hoverButtonSound;
    [SerializeField] private AudioSource clickButtonSound;

    public void OnPointerClick(PointerEventData eventData)
    {
        //clickButtonSound.Play();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverButtonSound.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
