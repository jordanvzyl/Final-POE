using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    GameEngine ge;
    System.Random rnd = new System.Random();

    private int time = 0;
    private const int REFRESH_RATE = 60;

    const float PADDING = 5.12f;

    float x_Off, y_Off;

    // Use this for initialization
    void Start () {
        ge = new GameEngine(rnd.Next(15, 31), 4);
        x_Off = -Camera.main.orthographicSize;
        y_Off = Camera.main.orthographicSize;
        FillGrass();
        FillUnits(ge.getArray());
    }

    // Update is called once per frame
	void Update () {
        if(time % REFRESH_RATE == 0)
        {
            ge.playGame(time);
            Redraw(ge.getArray(), ge.getBuildingArray());
        }
        time++;
    }

    void FillGrass()
    {
        for (int y = 0; y < 20; y++)
        {
            for (int x = 0; x < 20; x++)
            {
                Instantiate(Resources.Load("grass"), new Vector3(x_Off + (x * PADDING), y_Off + (-y * PADDING), 0), Quaternion.identity);
            }
        }
    }

    void DestroyAll()
    {
        GameObject[] killAllUnits = GameObject.FindGameObjectsWithTag("Redraw");

        foreach (GameObject unit in killAllUnits)
        {
            Destroy(unit);
        }
    }

    void Redraw(Unit[] units, Building[] buildings)
    {
        DestroyAll();
        FillGrass();
        FillUnits(units);
        FillBuildings(buildings);
    }

    string DetermineHP(int hp, int maxHP)
    {
        string returnVal = "hp";
        double result = ((double)hp / (double)maxHP) * 20;
        int finalResult = Mathf.CeilToInt((float)result);
        returnVal += finalResult;
        return returnVal;
    }

    void FillBuildings(Building[] buildings)
    {
        for(int i = 0; i < buildings.Length; i++)
        {
            string[] buildingType = buildings[i].GetType().ToString().Split('.');
            string type = buildingType[buildingType.Length - 1];
            if(type == "FactoryBuilding")
            {
                FactoryBuilding temp = (FactoryBuilding)buildings[i];
                if(temp.Health > 0)
                {
                    if(temp.Team == "Hero")
                    {
                        Instantiate(Resources.Load("Factory_Hero"), new Vector3(x_Off + (temp.Pos_X * PADDING), y_Off + (-temp.Pos_Y * PADDING), -1), Quaternion.identity);
                        Instantiate(Resources.Load(DetermineHP(temp.Health, temp.MaxHealth)), new Vector3(x_Off + (temp.Pos_X * PADDING), y_Off + (-temp.Pos_Y * PADDING), -2), Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(Resources.Load("Factory_Enemy"), new Vector3(x_Off + (temp.Pos_X * PADDING), y_Off + (-temp.Pos_Y * PADDING), -1), Quaternion.identity);
                        Instantiate(Resources.Load(DetermineHP(temp.Health, temp.MaxHealth)), new Vector3(x_Off + (temp.Pos_X * PADDING), y_Off + (-temp.Pos_Y * PADDING), -2), Quaternion.identity);
                    }
                }
                else
                {
                    Instantiate(Resources.Load("grass"), new Vector3(x_Off + (temp.Pos_X * PADDING), y_Off + (-temp.Pos_Y * PADDING), 0), Quaternion.identity);
                }
            }
            else
            {
                ResourceBuilding temp = (ResourceBuilding)buildings[i];
                if(temp.Health > 0)
                {
                    if(temp.Team == "Hero")
                    {
                        Instantiate(Resources.Load("Resource_Hero"), new Vector3(x_Off + (temp.Pos_X * PADDING), y_Off + (-temp.Pos_Y * PADDING), -1), Quaternion.identity);
                        Instantiate(Resources.Load(DetermineHP(temp.Health, temp.MaxHealth)), new Vector3(x_Off + (temp.Pos_X * PADDING), y_Off + (-temp.Pos_Y * PADDING), -2), Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(Resources.Load("Resource_Enemy"), new Vector3(x_Off + (temp.Pos_X * PADDING), y_Off + (-temp.Pos_Y * PADDING), -1), Quaternion.identity);
                        Instantiate(Resources.Load(DetermineHP(temp.Health, temp.MaxHealth)), new Vector3(x_Off + (temp.Pos_X * PADDING), y_Off + (-temp.Pos_Y * PADDING), -2), Quaternion.identity);
                    }
                }
                else
                {
                    Instantiate(Resources.Load("grass"), new Vector3(x_Off + (temp.Pos_X * PADDING), y_Off + (-temp.Pos_Y * PADDING), 0), Quaternion.identity);
                }
            }
        }
    }

    void FillUnits(Unit[] units)
    {
        for (int i = 0; i < units.Length; i++)
        {
            string[] unitType = units[i].GetType().ToString().Split('.');
            string type = unitType[unitType.Length - 1];

            if(type == "RangedUnit")
            {
                RangedUnit temp = (RangedUnit)units[i];
                if(temp.Health > 0)
                {
                    if (temp.Team == "Hero")
                    {
                        Instantiate(Resources.Load("Ranged_Hero"), new Vector3(x_Off + (temp.Pos_X * PADDING), y_Off + (-temp.Pos_Y * PADDING), -1), Quaternion.identity);
                        Instantiate(Resources.Load(DetermineHP(temp.Health, temp.MaxHealth)), new Vector3(x_Off + (temp.Pos_X * PADDING), y_Off + (-temp.Pos_Y * PADDING), -2), Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(Resources.Load("Ranged_Enemy"), new Vector3(x_Off + (temp.Pos_X * PADDING), y_Off + (-temp.Pos_Y * PADDING), -1), Quaternion.identity);
                        Instantiate(Resources.Load(DetermineHP(temp.Health, temp.MaxHealth)), new Vector3(x_Off + (temp.Pos_X * PADDING), y_Off + (-temp.Pos_Y * PADDING), -2), Quaternion.identity);
                    }
                }
                else
                {
                    Instantiate(Resources.Load("grass"), new Vector3(x_Off + (temp.Pos_X * PADDING), y_Off + (-temp.Pos_Y * PADDING), 0), Quaternion.identity);
                }
                
            }
            if(type == "MeleeUnit")
            {
                MeleeUnit temp = (MeleeUnit)units[i];
                if(temp.Health > 0)
                {
                    if (temp.Team == "Hero")
                    {
                        Instantiate(Resources.Load("Melee_Hero"), new Vector3(x_Off + (temp.Pos_X * PADDING), y_Off + (-temp.Pos_Y * PADDING), -1), Quaternion.identity);
                        Instantiate(Resources.Load(DetermineHP(temp.Health, temp.MaxHealth)), new Vector3(x_Off + (temp.Pos_X * PADDING), y_Off + (-temp.Pos_Y * PADDING), -2), Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(Resources.Load("Melee_Enemy"), new Vector3(x_Off + (temp.Pos_X * PADDING), y_Off + (-temp.Pos_Y * PADDING), -1), Quaternion.identity);
                        Instantiate(Resources.Load(DetermineHP(temp.Health, temp.MaxHealth)), new Vector3(x_Off + (temp.Pos_X * PADDING), y_Off + (-temp.Pos_Y * PADDING), -2), Quaternion.identity);
                    }
                }
                else
                {
                    Instantiate(Resources.Load("grass"), new Vector3(x_Off + (temp.Pos_X * PADDING), y_Off + (-temp.Pos_Y * PADDING), 0), Quaternion.identity);
                }
            }
            if(type == "ArtilleryUnit")
            {
                ArtilleryUnit temp = (ArtilleryUnit)units[i];
                if(temp.Health > 0)
                {
                    if (temp.Team == "Hero")
                    {
                        Instantiate(Resources.Load("Artillery_Hero"), new Vector3(x_Off + (temp.Pos_X * PADDING), y_Off + (-temp.Pos_Y * PADDING), -1), Quaternion.identity);
                        Instantiate(Resources.Load(DetermineHP(temp.Health, temp.MaxHealth)), new Vector3(x_Off + (temp.Pos_X * PADDING), y_Off + (-temp.Pos_Y * PADDING), -2), Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(Resources.Load("Artillery_Enemy"), new Vector3(x_Off + (temp.Pos_X * PADDING), y_Off + (-temp.Pos_Y * PADDING), -1), Quaternion.identity);
                        Instantiate(Resources.Load(DetermineHP(temp.Health, temp.MaxHealth)), new Vector3(x_Off + (temp.Pos_X * PADDING), y_Off + (-temp.Pos_Y * PADDING), -2), Quaternion.identity);
                    }
                }
                else
                {
                    Instantiate(Resources.Load("grass"), new Vector3(x_Off + (temp.Pos_X * PADDING), y_Off + (-temp.Pos_Y * PADDING), 0), Quaternion.identity);
                }
            }
            if(type == "MedicUnit")
            {
                MedicUnit temp = (MedicUnit)units[i];
                if(temp.Health > 0)
                {
                    if (temp.Team == "Hero")
                    {
                        Instantiate(Resources.Load("Medic_Hero"), new Vector3(x_Off + (temp.Pos_X * PADDING), y_Off + (-temp.Pos_Y * PADDING), -1), Quaternion.identity);
                        Instantiate(Resources.Load(DetermineHP(temp.Health, temp.MaxHealth)), new Vector3(x_Off + (temp.Pos_X * PADDING), y_Off + (-temp.Pos_Y * PADDING), -2), Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(Resources.Load("Medic_Enemy"), new Vector3(x_Off + (temp.Pos_X * PADDING), y_Off + (-temp.Pos_Y * PADDING), -1), Quaternion.identity);
                        Instantiate(Resources.Load(DetermineHP(temp.Health, temp.MaxHealth)), new Vector3(x_Off + (temp.Pos_X * PADDING), y_Off + (-temp.Pos_Y * PADDING), -2), Quaternion.identity);
                    }
                }
                else
                {
                    Instantiate(Resources.Load("grass"), new Vector3(x_Off + (temp.Pos_X * PADDING), y_Off + (-temp.Pos_Y * PADDING), 0), Quaternion.identity);
                }
            }
            if(type == "ReconUnit")
            {
                ReconUnit temp = (ReconUnit)units[i];
                if(temp.Health > 0)
                {
                    if (temp.Team == "Hero")
                    {
                        Instantiate(Resources.Load("Recon_Hero"), new Vector3(x_Off + (temp.Pos_X * PADDING), y_Off + (-temp.Pos_Y * PADDING), -1), Quaternion.identity);
                        Instantiate(Resources.Load(DetermineHP(temp.Health, temp.MaxHealth)), new Vector3(x_Off + (temp.Pos_X * PADDING), y_Off + (-temp.Pos_Y * PADDING), -2), Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(Resources.Load("Recon_Enemy"), new Vector3(x_Off + (temp.Pos_X * PADDING), y_Off + (-temp.Pos_Y * PADDING), -1), Quaternion.identity);
                        Instantiate(Resources.Load(DetermineHP(temp.Health, temp.MaxHealth)), new Vector3(x_Off + (temp.Pos_X * PADDING), y_Off + (-temp.Pos_Y * PADDING), -2), Quaternion.identity);
                    }
                }
                else
                {
                    Instantiate(Resources.Load("grass"), new Vector3(x_Off + (temp.Pos_X * PADDING), y_Off + (-temp.Pos_Y * PADDING), 0), Quaternion.identity);
                }
            }
            if(type == "StormTrooperUnit")
            {
                StormTrooperUnit temp = (StormTrooperUnit)units[i];
                if(temp.Health > 0)
                {
                    if (temp.Team == "Hero")
                    {
                        Instantiate(Resources.Load("StormTrooper_Hero"), new Vector3(x_Off + (temp.Pos_X * PADDING), y_Off + (-temp.Pos_Y * PADDING), -1), Quaternion.identity);
                        Instantiate(Resources.Load(DetermineHP(temp.Health, temp.MaxHealth)), new Vector3(x_Off + (temp.Pos_X * PADDING), y_Off + (-temp.Pos_Y * PADDING), -2), Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(Resources.Load("StormTrooper_Enemy"), new Vector3(x_Off + (temp.Pos_X * PADDING), y_Off + (-temp.Pos_Y * PADDING), -1), Quaternion.identity);
                        Instantiate(Resources.Load(DetermineHP(temp.Health, temp.MaxHealth)), new Vector3(x_Off + (temp.Pos_X * PADDING), y_Off + (-temp.Pos_Y * PADDING), -2), Quaternion.identity);
                    }
                }
                else
                {
                    Instantiate(Resources.Load("grass"), new Vector3(x_Off + (temp.Pos_X * PADDING), y_Off + (-temp.Pos_Y * PADDING), 0), Quaternion.identity);
                }
            }
            if(type == "SupportUnit")
            {
                SupportUnit temp = (SupportUnit)units[i];
                if(temp.Health > 0)
                {
                    if (temp.Team == "Hero")
                    {
                        Instantiate(Resources.Load("Support_Hero"), new Vector3(x_Off + (temp.Pos_X * PADDING), y_Off + (-temp.Pos_Y * PADDING), -1), Quaternion.identity);
                        Instantiate(Resources.Load(DetermineHP(temp.Health, temp.MaxHealth)), new Vector3(x_Off + (temp.Pos_X * PADDING), y_Off + (-temp.Pos_Y * PADDING), -2), Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(Resources.Load("Support_Enemy"), new Vector3(x_Off + (temp.Pos_X * PADDING), y_Off + (-temp.Pos_Y * PADDING), -1), Quaternion.identity);
                        Instantiate(Resources.Load(DetermineHP(temp.Health, temp.MaxHealth)), new Vector3(x_Off + (temp.Pos_X * PADDING), y_Off + (-temp.Pos_Y * PADDING), -2), Quaternion.identity);
                    }
                }
                else
                {
                    Instantiate(Resources.Load("grass"), new Vector3(x_Off + (temp.Pos_X * PADDING), y_Off + (-temp.Pos_Y * PADDING), 0), Quaternion.identity);
                }
            }
            
        }
    }
}
