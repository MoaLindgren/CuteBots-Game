using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonSays : MonoBehaviour {
    /* Spelet startar. 
     * Spelaren får ett meddelande att observera först.
     * Spelet visar färgkombination.
     * Spelaren upprepar.
     * Vid fel, spelaren förlorar. Stäng av mini game.
     * Vid rätt. Nästa kombination.
     * upp till tre kombinationer. +1 för varje kombination.
      */

    bool win = true;
    public int myNr;

    private Renderer myRenderer;
	public enum ButtonColors
    {
        Red,
        Green,
        Blue,
        Yellow
    }

    private void Awake()
    {
        myRenderer = GetComponent<Renderer>();
        myRenderer.enabled = true;

        
    }

    private void OnMouseDown()
    {
        ClickedColor();
    }

    private void OnMouseUp()
    {
        UnClickedColor();
    }

    public void ClickedColor()
    {

    }

    public void UnClickedColor()
    {

    }

    
	

	void Update ()
    {
		
	}
}
