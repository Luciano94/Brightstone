using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour{
    private static EventManager instance;

    public static EventManager Instance{
        get {
            instance = FindObjectOfType<EventManager>();
            if(instance == null){
                GameObject go = new GameObject("EventManager");
                instance = go.AddComponent<EventManager>();
            }
            return instance;
        }
    }

    private enum TutorialEvents{
        // Room 0
        Explanation = 0,
        Movement,
        Minimap,
        DoorOpen1,

        // Room 1
        SimpleAttack,
        StrongAttack,
        SimpleCombo,
        ExperienceSystem,
        DoorOpen2,

        // Room 2
        Parry, // No attacking; Wait to third parry
        DoorOpen3,

        // Room 3
        Market,
        DoorOpenLast
    }

    [SerializeField] private PauseController pauseController;

    [SerializeField] private GameObject door1;
    [SerializeField] private GameObject door2;
    [SerializeField] private GameObject door3;
    [SerializeField] private GameObject doorLast;

    private GameManager gameM;
    private UIManager uiM;

    private float timer = 0.0f;

    private TutorialEvents actualEvent = TutorialEvents.Explanation;

    void Awake(){
        gameM = GameManager.Instance;
        uiM = UIManager.Instance;

        gameM.playerMovement.enabled = false;
        gameM.playerCombat.enabled = false;
        pauseController.enabled = false;
    }

    private void Update(){
        switch(actualEvent){
            case TutorialEvents.Explanation:

            break;
            case TutorialEvents.Movement:

            break;
            case TutorialEvents.Minimap:

            break;
            case TutorialEvents.DoorOpen1:
                OpenDoor(door1);
            break;
            case TutorialEvents.SimpleAttack:

            break;
            case TutorialEvents.StrongAttack:

            break;
            case TutorialEvents.SimpleCombo:

            break;
            case TutorialEvents.ExperienceSystem:

            break;
            case TutorialEvents.DoorOpen2:
                OpenDoor(door2);
            break;
            case TutorialEvents.Parry:

            break;
            case TutorialEvents.DoorOpen3:
                OpenDoor(door3);
            break;
            case TutorialEvents.Market:

            break;
            case TutorialEvents.DoorOpenLast:
                OpenDoor(doorLast);
            break;
        }
    }

    private void OpenDoor(GameObject door){
        // Activar la puerta

    }

    private void PassThroughDoor(){
        actualEvent++;
    }
}
