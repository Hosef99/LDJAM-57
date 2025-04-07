using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerData : MonoBehaviour{
    public static PlayerData Instance;
    public PlayerController playerController;
    public Light2D light2D;
    public int goldCount = 0;
    public int fossil1Count = 0;
    public int fossil2Count = 0;
    public int fossil3Count = 0;
    public int fossil4Count = 0;
    public int fossil5Count = 0;
    public int fossil6Count = 0;
    public int sliverCount = 0;
    public int diamondCount = 0;
    public int redStoneCount = 0;
    public int stamina = 0;
    public int vision = 1;
    public int bombCount = 0;
    public int cardSlots = 5;

    public int bombCapcity = 1;
    

    

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }




    public void OnUpdate(){
        
    }


    public void VisionIncrease(int level)
    {
        switch (level)
        {
            case 0:
                light2D.falloffIntensity = 1;
                break;
            case 1:
                light2D.falloffIntensity = 0.8f;
                break;
            case 2:
                light2D.falloffIntensity = 0.65f;
                break;
            case 3:
                light2D.falloffIntensity = 0.5f;
                break;

        }
        
        
    }
    
    public void Boom(int level)
    {
        switch (level)
        {
            case 0:
                light2D.falloffIntensity = 1;
                break;
            case 1:
                light2D.falloffIntensity = 0.8f;
                break;
            case 2:
                light2D.falloffIntensity = 0.65f;
                break;
            case 3:
                light2D.falloffIntensity = 0.5f;
                break;

        }
        
        
    }
    
    

}
