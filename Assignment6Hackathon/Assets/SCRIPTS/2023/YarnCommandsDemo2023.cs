using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn;
using Yarn.Unity;
using static UnityEngine.Rendering.DebugUI.Table;

public class YarnCommandsDemo2023 : MonoBehaviour
{
    public InMemoryVariableStorage yarnInMemoryVariableStorage;

    public GameObject[] NPCGameObjects;

    public Animator npcAnimatorMale;
    public Animator npcAnimatorFemale;
    public GameObject spawner;


    // Start is called before the first frame update
    void Start()
    {
        yarnInMemoryVariableStorage.SetValue("$numberOfNPCs", NPCGameObjects.Length);
        Debug.Log(NPCGameObjects.Length);
    }

    // Function to handle the custom command
    [YarnCommand("TurnOnPlasticBottleSpawner")]
    public void ActivateSpawnerOfBottles()
    {
        spawner.SetActive(true);
    }

    [YarnCommand("TapOnTable")]
    public void TapTheTable()
    {
        npcAnimatorMale.SetTrigger("StandingTapTable");
    }

    [YarnCommand("PointToTheRight")]
    public void ArmPointRight()
    {
        npcAnimatorMale.SetTrigger("StandingArmPointRight");
    }
}
