using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpiritBond : MonoBehaviour
{
    // Get references to all 4 players
    [SerializeField] private GameObject Player1;
    [SerializeField] private GameObject Player2;
    [SerializeField] private GameObject Player3;
    [SerializeField] private GameObject Player4;
    
    // Initialize an array that will contain all the players' positions
    public Transform[] playerPositions;

    private Transform[] PlayerPositionsDelta;

    // Total distance between players, calculated as the sum of the distance between every player. Damage Multiplier scales directly with this.
    [SerializeField] private float TotalDistance;
    // Damage Multiplier. This is the variable that we are trying to calculate
    [SerializeField] private double bonusMultiplier;

    // Public variable that references the calculated value. Intended for use on other scripts
    public float BonusMultiplier => (float)bonusMultiplier;

    // Rate at which the bonus multiplier decreases inversely proportional to distance. The lower it is, the slower the damage will decrease whenever distance increases
    private float decreaseRate = 1.2f;
    // Controls the distance that has to exist between players before damage starts to decrease. The higher it is, the further apart players must be for falloff to occur
    private float distanceFalloff = 180.0f;

    




    // Initializes player positions as an array of 4 elements, each containing the Transform of its respective player; TotalDistance as 0.0f
    void Start(){
        playerPositions = new Transform[4] 
            {
            Player1.transform, 
            Player2.transform, 
            Player3.transform, 
            Player4.transform
            };

        TotalDistance = 0.0f;

        PlayerPositionsDelta = playerPositions;

        
        
    }

    void Update()
    {
        // Reset the value of total distance to 0
        // It will increase exponentially if we don't
        TotalDistance = 0.0f;


        // Calculates the total distance between adjacent players contained in the playerPositions Array.
        // This method ensures that the resulting distance will be consistent regardless of how players are arranged. 
        // Determines the distance between each adjacent pair of players, then adds the result to TotalDistance
        for(int i = 0; i < playerPositions.Length; i++) {
            int nextIndex = (i + 1) % playerPositions.Length;
            float distance = Vector3.Distance(playerPositions[i].position, playerPositions[nextIndex].position);

            TotalDistance += distance;

        }

        // A mathematical formula that models the exponential decrease of bonusMultiplier inversely proportional to TotalDistance as:
        // f(t) = -dR^(t-dF/2)+4
        // Players may stray away from each other up to an effective range of about 65 meters before damage starts to decrease.
        // The further away players are from each other, the sharper the decline in damage. This decline is hardcapped to 0.5f to avoid leaving players with negative damage
        // Due to how the formula is written, f(t) cannot be higher than 4.
        bonusMultiplier = -Math.Pow(decreaseRate, (TotalDistance - distanceFalloff / 2)) + 4.0f;
        bonusMultiplier = bonusMultiplier < 0.5 ? 0.5 : bonusMultiplier;

        if(bonusMultiplier > 3.8){
            ActivateParticleSystems();
        } else {
            DeactivateParticleSystems();
        }

        CalculatePlayerOrder();

    }

    void BubbleSort(Transform[] playerPos, Vector3 front, Vector3 center){
        Transform temp;
        bool swapped;
        for (int i = 0; i < playerPos.Length; i++){
            swapped = false;
            for (int j = 0; j < playerPos.Length; j++){
                if (GetAngle(center, front, playerPos[j].position) > GetAngle(center, front, playerPos[j + 1].position)){
                    temp = playerPos[j];
                    playerPos[j] = playerPos[j + 1];
                    playerPos[j + 1] = temp;
                    swapped = true;
                }
            }
            if (swapped == false)
                break;
        }
    }

    float GetAngle(Vector3 center, Vector3 front, Vector3 angle){
        float calculatedAngle = Vector3.SignedAngle(angle - center, front, center);

        float FinalAngle = (calculatedAngle > 0) ? calculatedAngle : calculatedAngle + 360;
    
        return FinalAngle;
        
    }

    void CalculatePlayerOrder(){
        // find the vertex of the angle. this is used in every calculation
        Vector3 centroid = FindCentroid();
        // find the point directly in front of the centroid. this is the control vector to make all comparisons from
        Vector3 front = centroid + new Vector3(0, 0, 20);
        Debug.DrawRay(centroid, front, new Color(255, 0, 0, 255));
        

        BubbleSort(PlayerPositionsDelta, front, centroid);

    }

    Vector3 FindCentroid(){
        float centroidX = 0, centroidZ = 0;
        foreach (Transform vertex in playerPositions){
            centroidX += vertex.position.x;
            centroidZ += vertex.position.z;
        }
        centroidX /= playerPositions.Length;
        centroidZ /= playerPositions.Length;

        Vector3 centroid = new Vector3(centroidX, 0, centroidZ);

        return centroid;
    }

    void ActivateParticleSystems(){
        foreach (Transform player in playerPositions){
            GameObject particleSystem = player.transform.Find("Spirit Bond Particles").gameObject;
            particleSystem.SetActive(true);
        }
    }

    void DeactivateParticleSystems(){
        foreach (Transform player in playerPositions){
            GameObject particleSystem = player.transform.Find("Spirit Bond Particles").gameObject;
            particleSystem.SetActive(false);
        }
    }
}
