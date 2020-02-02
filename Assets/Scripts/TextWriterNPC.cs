using UnityEngine;
using UnityEngine.UI;
using System.Collections;



public class TextWriterNPC : MonoBehaviour
{

    //Give access to the text component.
    public Text dialogueText;

    public string textToAnimate;
    public string textProximiti;
    public string textEvent;

    //The Speed the text is animated on screen. Waits 0.05 seconds before animating the next character.
    //Useful for letting the player accelerate the speed animation.
    public float speedText = 0.05f;

    private bool playerCloseEnough = false;
    private bool ProximitiEvent = false;
    private bool onEvent = false;
    private NPCStatus status;

    public int distance;

    public GameObject ob1;
    public GameObject ob2;
    public GameObject obEvent;

    void Awake()
    {
        status = this.obEvent.GetComponent<NPCStatus>();
    }
    
    void Update()
    {
        //Simple controls to accelerate the text speed.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            speedText = speedText / 100;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            speedText = 0.2f;
        }
        if ((ProximitiEvent == false) && (Vector3.Distance(ob1.transform.position, ob2.transform.position) < distance))
        {
            AnimateDialogueBox(textProximiti);
            ProximitiEvent = true;
        }
        if (true) {
            if ((onEvent == false) && (status.isRepaied() == true))
            {
                AnimateDialogueBox(textEvent);
                onEvent = true;
                Debug.Log("Noooooo");
            }
        }
    }

    //Call this public function when you want to animate text. This should be used in your other scripts.
    public void AnimateDialogueBox(string text)
    {
        StartCoroutine(AnimateTextCoroutine(text));
    }

    private IEnumerator AnimateTextCoroutine(string text)
    {
        TextMesh textMeshComponent = (TextMesh)GetComponent(typeof(TextMesh));
        textMeshComponent.text = "";
        int i = 0;

        //Loop over the string.
        while (i < text.Length)
        {

            //Add a character to the dialogue text field.
            textMeshComponent.text += text[i];

            i++;    //increment

            //Wait before animating next character in scenarioText.
            yield return new WaitForSeconds(speedText);
        }
        yield return new WaitForSeconds(5);
        textMeshComponent.text = "";
        Debug.Log("Done animating!");
    }
}
