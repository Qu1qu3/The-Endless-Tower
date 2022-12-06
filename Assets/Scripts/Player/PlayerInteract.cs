using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class PlayerInteract
{   
    public float distanceInteract = 3f;
    public TextMeshProUGUI interactionText;
    public GameObject interactionUI;
    public void Initialize()
    {
        interactionUI = GameObject.Find("Mage/Einteract/InteractionUI");
        Debug.Log(interactionUI + "aaaaaaaaaaaaaaaaaaa");
        interactionText = interactionUI.transform.Find("InteractionText").gameObject.GetComponent<TextMeshProUGUI>();
    }
    // Start is called before the first frame update
    public void tryToInteract()
    {
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, distanceInteract, 1 << 11))
        {
            //Debug.Log ("E");
            Interactable inter = hit.collider.GetComponent<Interactable>();
            if(inter != null)
            {
                //Debug.Log ("Interacting!");
                inter.Interact();
            }
        }
    }


    public void UpdateRay(bool hold)
    {
        //Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward, Color.white, 3f);
        
        RaycastHit hit;

        bool hitSomething = false;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, distanceInteract, 1 << 11))
        {

            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if (interactable != null)
            {
                hitSomething = true;
                interactionText.text = interactable.GetDescription();

            }
        }
        if (hold)
            hitSomething = false;

        interactionUI.SetActive(hitSomething);
    
    }
}
