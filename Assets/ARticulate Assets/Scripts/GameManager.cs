using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FreeDraw;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //public GameObject canvas;
    public GameObject loginScreen;
    public GameObject dataPolicyScreen;
    public GameObject identifyAPaintingScreen;
    public TMP_Text identifyAPaintingText;
    //public GameObject paintingIdentifiedText;
    public GameObject userProfileScreen;
    public GameObject paintingInfoPanel;
    public TMP_Text[] paintingNameTexts;
    public TMP_Text[] artistNameTexts;
    public GameObject respondUIParent;
    public GameObject respondUIPartOne;
    public GameObject respondUIPartTwo;
    public GameObject viewResponsesUICollapsed;
    public GameObject viewResponsesUIExpanded;
    private int currentPaintingID;
    private int[] paintingActiveResponseIndex;

    public TMP_Text sharedResponseMainText;
    public TMP_Text sharedResponseAuthorAndDateText;

    public TMP_InputField userResponseInputField;
    public TMP_Text userResponseMainText;
    public TMP_Text userResponseAuthorAndDateText;

    

    public Drawable drawArea;
    public Image[] userAnnotationImages;
    public Image[] sharedAnnotationImages;
    public AnnotationSaver annotationSaver;
    //public AnnotationLibrary annotationLibrary;
    public FakeAnnotationDatabase fakeAnnotationDatabase;
    public FakePaintingDatabase fakePaintingDatabase;
    public ReactionButton reactionMenu;
    public ViewResponseUIManager viewResponseUIManager;

    private Sprite[] userAnnotationSprites;

    private bool loggedIn; // Used to prevent a painting from being registered while still on landing screen (not yet implemented) and to determine when to activate the "Identify a piece!" UI (implemented)
    private bool annotationMade; // TODO: Used to fix a bug for demo. Remove after recording

    private IEnumerator Start()
    {
        yield return null;
        yield return null;
        yield return null;

        loginScreen.SetActive(true);

        userAnnotationSprites = new Sprite[userAnnotationImages.Length];

        paintingActiveResponseIndex = new int[sharedAnnotationImages.Length];
        //for (int i = 0; i < paintingActiveResponseIndex.Length; i++)
        //{
        //    paintingActiveResponseIndex[i] = -1;
        //}
        paintingActiveResponseIndex[0] = -1;
        for (int i = 1; i < paintingActiveResponseIndex.Length; i++)
        {
            paintingActiveResponseIndex[i] = 0;
        }

        ViewNextResponse();
        // Hack for demo video
        //currentPaintingID = 1;
        //ViewNextResponse();
        //currentPaintingID = 0;
        //ViewNextResponse();
    }

    public void SetActivePainting(int paintingID)
    {
        currentPaintingID = paintingID;

        StartCoroutine("AnimateInCanvas");
    }

    private IEnumerator AnimateInCanvas()
    {
        //identifyAPaintingScreen.SetActive(false);
        //paintingIdentifiedText.SetActive(true);
        identifyAPaintingText.text = "Painting identified!";

        yield return new WaitForSeconds(2.5f);

        //paintingIdentifiedText.SetActive(false);
        identifyAPaintingScreen.SetActive(false);

        paintingInfoPanel.SetActive(true);

        for (int i = 0; i < paintingNameTexts.Length; i++)
        {
            paintingNameTexts[i].text = fakePaintingDatabase.paintings[currentPaintingID].paintingName;
            artistNameTexts[i].text = fakePaintingDatabase.paintings[currentPaintingID].artistName;
        }

        //if (userAnnotationSprites[currentPaintingID] != null)
        //{
        //    print("Saved annotation found");
        //    userAnnotationImages[currentPaintingID].gameObject.SetActive(true);
        //    userAnnotationImages[currentPaintingID].sprite = userAnnotationSprites[currentPaintingID];
        //}
        //else
        //{
        //    print("No saved annotation found");
        //    userAnnotationImages[currentPaintingID].gameObject.SetActive(false);
        //}

        // TODO: Remove after demo video
        if (annotationMade)
        {
            //print("Saved annotation found");
            userAnnotationImages[currentPaintingID].gameObject.SetActive(true);
            userAnnotationImages[currentPaintingID].sprite = userAnnotationSprites[currentPaintingID];
        }
        else
        {
            //print("No saved annotation found");
            userAnnotationImages[currentPaintingID].gameObject.SetActive(false);
        }
    }

    public void SetNoPaintingActive()
    {
        StopCoroutine("AnimateInCanvas");

        paintingInfoPanel.SetActive(false);
        respondUIParent.SetActive(false);
        respondUIPartOne.SetActive(false); // TODO: If actively working on a response, should be able to leave painting
        respondUIPartTwo.SetActive(false);
        viewResponsesUICollapsed.SetActive(false);
        viewResponsesUIExpanded.SetActive(false);
        reactionMenu.CloseResponseUI();
        //paintingOneAnnotationImage.gameObject.SetActive(false);

        for (int i = 0; i < userAnnotationImages.Length; i++)
        {
            userAnnotationImages[i].gameObject.SetActive(false);
            sharedAnnotationImages[i].gameObject.SetActive(false);
        }

        //canvas.SetActive(false);
        if (loggedIn)
        {
            identifyAPaintingText.text = "Identify a painting!";
            identifyAPaintingScreen.SetActive(true);
        }
    }

    public void ClickLogin()
    {
        loginScreen.SetActive(false);
        identifyAPaintingScreen.SetActive(true);

        loggedIn = true;
    }

    public void OpenDataPolicy()
    {
        loginScreen.SetActive(false);
        dataPolicyScreen.SetActive(true);
    }

    public void CloseDataPolicy()
    {
        loginScreen.SetActive(true);
        dataPolicyScreen.SetActive(false);
    }

    public void OpenUserProfile() // Could cause a UI bug if clicked while "Painting Identified!" animation is in progress
    {
        identifyAPaintingScreen.SetActive(false);
        paintingInfoPanel.SetActive(false);

        userProfileScreen.SetActive(true);
    }

    public void CloseUserProfile()
    {
        // TODO: Save profile data if modified

        userProfileScreen.SetActive(false);

        // TODO: Check if there's a painting currently active or not and pick which screen to return to based on that (currently hardcoded to return to painting info screen)
        paintingInfoPanel.SetActive(true);
    }

    public void PressRespond()
    {
        paintingInfoPanel.SetActive(false);

        respondUIParent.SetActive(true);
        respondUIPartOne.SetActive(true);

        //raycastTest.editModeActive = true;
    }

    public void SubmitResponsePartOne()
    {
        respondUIPartOne.SetActive(false);
        respondUIPartTwo.SetActive(true);

        userResponseMainText.text = userResponseInputField.text;
    }

    public void SubmitResponsePartTwo()
    {
        respondUIParent.SetActive(false);
        respondUIPartTwo.SetActive(false);

        paintingInfoPanel.SetActive(true);

        annotationSaver.SaveAnnotation(currentPaintingID);

        userAnnotationSprites[currentPaintingID] = drawArea.drawable_sprite.sprite;

        userAnnotationImages[currentPaintingID].sprite = userAnnotationSprites[currentPaintingID];
        userAnnotationImages[currentPaintingID].gameObject.SetActive(true);

        // TODO: Remove after demo video
        annotationMade = true;
    }

    public void DiscardResponse()
    {
        respondUIParent.SetActive(false);
        respondUIPartOne.SetActive(false);

        paintingInfoPanel.SetActive(true);

        //raycastTest.editModeActive = false;
        userAnnotationImages[currentPaintingID].gameObject.SetActive(false);
    }

    public void ReturnToResponsePartOne()
    {
        respondUIPartTwo.SetActive(false);
        respondUIPartOne.SetActive(true);
    }

    public void PressViewResponses()
    {
        paintingInfoPanel.SetActive(false);

        viewResponsesUICollapsed.SetActive(true);

        //// This has to be set here in case a user annotation is currently being displayed. User annotations won't overwrite the view annotation text though
        //Annotation newAnnotation = annotationLibrary.paintingOneAnnotations[paintingOneIndex];
        //paintingOneAnnotationImage.sprite = newAnnotation.visualAnnotation;

        sharedAnnotationImages[currentPaintingID].gameObject.SetActive(true);
        userAnnotationImages[currentPaintingID].gameObject.SetActive(false);

        //karenExampleResponse.SetActive(true);

        SetActiveResponse();
    }

    public void ExpandThoughtsPanel()
    {
        viewResponsesUICollapsed.SetActive(false);
        viewResponsesUIExpanded.SetActive(true);
    }

    public void CollapseThoughtsPanel()
    {
        viewResponsesUICollapsed.SetActive(true);
        viewResponsesUIExpanded.SetActive(false);
    }

    public void ReturnToHome()
    {
        viewResponsesUICollapsed.SetActive(false);
        reactionMenu.CloseResponseUI();

        paintingInfoPanel.SetActive(true);

        sharedAnnotationImages[currentPaintingID].gameObject.SetActive(false);

        // If user has a saved annotation for this painting, painting image should be active. It not, it should be disabled
        //paintingOneAnnotationImage.gameObject.SetActive(false);
        if (userAnnotationSprites[currentPaintingID] != null)
        {
            //print("Saved annotation found");
            userAnnotationImages[currentPaintingID].gameObject.SetActive(true);
            userAnnotationImages[currentPaintingID].sprite = userAnnotationSprites[currentPaintingID];
        }
        else
        {
            //print("No saved annotation found");
            userAnnotationImages[currentPaintingID].gameObject.SetActive(false);
        }

        //karenExampleResponse.SetActive(false);
    }

    public void ViewNextResponse()
    {
        paintingActiveResponseIndex[currentPaintingID]++;
        if (paintingActiveResponseIndex[currentPaintingID] >= fakeAnnotationDatabase.paintings[currentPaintingID].annotations.Length)
        {
            paintingActiveResponseIndex[currentPaintingID] = 0;
        }

        //Annotation newAnnotation = fakeAnnotationDatabase.paintings[currentPaintingID].annotations[paintingActiveResponseIndex[currentPaintingID]];

        //sharedAnnotationImages[currentPaintingID].sprite = newAnnotation.visualAnnotation;

        //sharedResponseMainText.text = newAnnotation.textAnnotation;
        //sharedResponseAuthorAndDateText.text = newAnnotation.author + " " + newAnnotation.date;

        //viewResponseUIManager.SetResponseCount(newAnnotation.reactionCounts);
        SetActiveResponse();
    }

    private void SetActiveResponse()
    {
        Annotation newAnnotation = fakeAnnotationDatabase.paintings[currentPaintingID].annotations[paintingActiveResponseIndex[currentPaintingID]];

        sharedAnnotationImages[currentPaintingID].sprite = newAnnotation.visualAnnotation;

        sharedResponseMainText.text = newAnnotation.textAnnotation;
        sharedResponseAuthorAndDateText.text = newAnnotation.author + " " + newAnnotation.date;

        viewResponseUIManager.SetResponseCount(newAnnotation.reactionCounts);
    }

}