using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropGenerator : MonoBehaviour
{
    [SerializeField] private int minProps = 0;
    [SerializeField] private int maxProps = 1;
    [SerializeField] private float propZDepth = 4;
    [SerializeField] private List<GameObject> Props = new List<GameObject>();
    [SerializeField] private List<Transform> Positions = new List<Transform>();

    
    void Start()
    {
        if (minProps < 0){ minProps = 0;}
        if(maxProps > Props.Count){ maxProps = Props.Count;}
        int propAmmount = Random.Range(minProps, maxProps + 1);

        List<GameObject> pickedProps = RandomListFromOriginal<GameObject>(Props, propAmmount);
        List<Transform> pickedPositions = RandomListFromOriginal<Transform>(Positions, Positions.Count);

        if(propAmmount > 0){
            int index = 0;
            foreach (GameObject p in Props){
                GameObject go = Instantiate(p);
                go.transform.parent = transform.parent;
                Vector3 position = pickedPositions[index].position;
                position.z = propZDepth;
                go.transform.position = position;
                index++;
                if (index >= pickedPositions.Count){ index = 0;}
            }
        }
    }

    private List<T> RandomListFromOriginal<T>(List<T> originalList, int randomAmmount){
        List<T> randomList = new List<T>();
        if (originalList.Count > 0 && randomAmmount > 0 && randomAmmount <= originalList.Count){

            //Copy original list into new list
            foreach(T element in originalList){
                randomList.Add(element);
            }

            //Remove random elements until new list is of desired range
            int difference = originalList.Count - randomAmmount;

            for(int i = 0; i < difference; i++){
                randomList.Remove(randomList[Random.Range(0,randomList.Count)]);
            }

            //Fisher-Yates shuffle to randomize the new list
            for (int i = randomList.Count -1; i > 0 ; i--){
                int rnd = Random.Range(0,i +1);
                T element = randomList[i];
                randomList[i] = randomList[rnd];
                randomList[rnd] = element;
            }
        }
        return randomList;
    } 
}
