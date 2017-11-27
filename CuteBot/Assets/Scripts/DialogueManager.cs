using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    bool endConversation;

    int dialogueNumber;
    int placeNumber;
    int maxAlternatives;

    string branch;
    string alternatives;
    string place;
    string firstPath;
    string playerPath;
    string aiPath;

    XmlReader read_FirstXMLPath;
    XmlReader read_PlayerXMLPath;
    XmlReader read_aiXMLPath;
    FileStream aiPhrases_Stream;
    FileStream firstPhrase_Stream;
    FileStream playerPhrases_Stream;

    Text uiText;
    GameObject dialogueFolder;
    GameObject text;
    GameObject canvas;
    GameObject buttons;
    GameObject dialogueBox;
    Text dialogueBox_Text;

    [SerializeField]
    List<string> altList;
    List<string> nameList;
    [SerializeField]
    public List<string> everythingSaid;
    List<Text> buttonsText;
    List<GameObject> allButtons;


    void Start()
    {
        endConversation = false;

        //Om något går fel i koden är det med stor sannolikhet någonting här som inte stämmer ihop med Unity:
        canvas = GameObject.Find("Canvas");
        dialogueFolder = canvas.transform.GetChild(1).gameObject;

        dialogueBox = dialogueFolder.transform.GetChild(2).gameObject;
        dialogueBox_Text = dialogueBox.transform.GetChild(0).transform.GetChild(0).GetComponentInChildren<Text>();

        dialogueBox.SetActive(false);

        text = dialogueFolder.transform.GetChild(0).gameObject;
        uiText = text.GetComponent<Text>();

        buttons = dialogueFolder.transform.GetChild(1).gameObject;
        nameList = new List<string>();
        allButtons = new List<GameObject>() { buttons.transform.GetChild(0).gameObject,
                                              buttons.transform.GetChild(1).gameObject,
                                              buttons.transform.GetChild(2).gameObject };
        place = "Place";
        branch = "0";

        maxAlternatives = 3;
        dialogueNumber = 0;

        firstPath = Application.dataPath + @"/XML/FirstPhrases.xml";
        playerPath = Application.dataPath + @"/XML/playerPhrases.xml";
        aiPath = Application.dataPath + @"/XML/aiPhrases.xml";

        altList = new List<string>();
        everythingSaid = new List<string>();

        for (int i = 0; i < maxAlternatives; i++)
        {
            allButtons[i].SetActive(false);
        }

        //Ändra följande värde och ta bort kommentar för att kunna testa dialogsystemet utan att gå runt i spelet.
        placeNumber = 0;

        //Sätt även denna aktiv för att testa dialog-systemet utan att faktiskt gå runt i spelet.
        StartPhrase();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            dialogueBox.SetActive(!dialogueBox.activeSelf);
        }
    }


    void StartPhrase()
    {
        firstPhrase_Stream = new FileStream(firstPath, FileMode.Open);
        read_FirstXMLPath = XmlReader.Create(firstPhrase_Stream);
        while (read_FirstXMLPath.Read())
        {
            if (read_FirstXMLPath.Name == place + placeNumber)
            {
                uiText.text = read_FirstXMLPath.GetAttribute("phrase");
                string tempString = uiText.text;
                dialogueBox_Text.text = dialogueBox_Text.text + tempString;


                everythingSaid.Add(uiText.text);
            }
        }
        firstPhrase_Stream.Close();
        StartCoroutine(PlayerTalk());
    }




    IEnumerator PlayerTalk()
    {
        altList.Clear();
        nameList.Clear();
        playerPhrases_Stream = new FileStream(playerPath, FileMode.Open);
        read_PlayerXMLPath = XmlReader.Create(playerPhrases_Stream);

        yield return new WaitForSeconds(2);
        while (read_PlayerXMLPath.Read())
        {
            for (int i = 0; i < maxAlternatives; i++)
            {
                if (read_PlayerXMLPath.Name == place + placeNumber + "_Dialogue" + dialogueNumber + "_Alternative" + i)
                {
                    nameList.Add(read_PlayerXMLPath.Name);
                    if ((read_PlayerXMLPath.GetAttribute("branch")) == branch)
                    {
                        alternatives = read_PlayerXMLPath.GetAttribute("phrase");
                        altList.Add(alternatives);
                        for (int y = 0; y < altList.Count; y++)
                        {
                            allButtons[i].SetActive(true);
                            allButtons[i].GetComponentInChildren<Text>().text = altList[y];
                        }
                    }
                }
            }
        }
        playerPhrases_Stream.Close();
    }
    public void ChangeBranch(int alternative)
    {


        for (int i = 0; i < allButtons.Count; i++)
        {
            allButtons[i].SetActive(false);
        }

        playerPhrases_Stream = new FileStream(playerPath, FileMode.Open);
        read_PlayerXMLPath = XmlReader.Create(playerPhrases_Stream);
        while (read_PlayerXMLPath.Read())
        {
            for (int i = 0; i < nameList.Count; i++)
            {
                if (nameList[i] == place + placeNumber + "_Dialogue" + dialogueNumber + "_Alternative" + alternative)
                {
                    if (read_PlayerXMLPath.Name == place + placeNumber + "_Dialogue" + dialogueNumber + "_Alternative" + alternative)
                    {
                        if (read_PlayerXMLPath.GetAttribute("newBranch") != null)
                        {
                            branch = read_PlayerXMLPath.GetAttribute("newBranch");
                        }
                    }
                }
            }
        }
        uiText.text = allButtons[alternative].GetComponentInChildren<Text>().text;
        string tempString = uiText.text;
        dialogueBox_Text.text = dialogueBox_Text.text + "\n" + tempString;
        everythingSaid.Add(uiText.text);
        playerPhrases_Stream.Close();
        if (endConversation)
        {
            StartCoroutine(EndConversation());
        }
        else
        {
            StartCoroutine(RobotTalks(alternative));
        }
    }

    IEnumerator RobotTalks(int caseValue)
    {
        aiPhrases_Stream = new FileStream(aiPath, FileMode.Open);
        read_aiXMLPath = XmlReader.Create(aiPhrases_Stream);

        yield return new WaitForSeconds(2);

        while (read_aiXMLPath.Read())
        {
            if (read_aiXMLPath.Name == place + placeNumber + "_Dialogue" + dialogueNumber + "_Case" + caseValue)
            {
                if (read_aiXMLPath.GetAttribute("branch") != null)
                {
                    if (read_aiXMLPath.GetAttribute("branch") == branch)
                    {
                        uiText.text = read_aiXMLPath.GetAttribute("phrase");
                        string tempString = uiText.text;
                        dialogueBox_Text.text = dialogueBox_Text.text + "\n" + tempString;
                        everythingSaid.Add(uiText.text);
                    }
                }
                else
                {
                    if (read_aiXMLPath.GetAttribute("branch") == branch)
                    {
                        uiText.text = read_aiXMLPath.GetAttribute("phrase");
                        string tempString = uiText.text;
                        dialogueBox_Text.text = dialogueBox_Text.text + "\n" + tempString;
                        everythingSaid.Add(uiText.text);
                    }
                }
                if (read_aiXMLPath.GetAttribute("end") == "true")
                {
                    endConversation = true;
                }

            }
        }
        aiPhrases_Stream.Close();
        dialogueNumber++;
        if (endConversation)
        {
            StartCoroutine(EndConversation());
        }
        else
        {
            StartCoroutine(PlayerTalk());
        }
    }

    IEnumerator EndConversation()
    {
        for (int i = 0; i < maxAlternatives; i++)
        {
            allButtons[i].SetActive(false);
        }
        yield return new WaitForSeconds(2);
        text.SetActive(false);
        branch = "0";
    }
}
