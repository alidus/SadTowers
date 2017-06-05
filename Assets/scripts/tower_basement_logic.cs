using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class tower_basement_logic : MonoBehaviour {
    private game_logic game_logic;
    GameObject turret;
    Canvas option_menu;
    private GameObject tower1;
    private GameObject tower2;
    private GameObject tower2_1;
    private GameObject tower3;
    private GameObject options_panel_go;
    private GameObject remove_button;
    private GameObject upgrade_button;
    private GameObject upgrade_button_price;
    private GameObject upgrade_button_price_panel;
    private data_store_logic DataStore;
    private float OM_fade_time = 0.2f;
    private float OM_transparency = 0.9f;
    private int state; // 0 - empty, 1 - tower_built
	// Use this for initialization
	void Start () {
        DataStore = GameObject.Find("DataStore").GetComponent<data_store_logic>();
        game_logic = GameObject.Find("GameLogic").GetComponent<game_logic>();
        option_menu = gameObject.transform.Find("basement_functions_canva").GetComponent<Canvas>();
        upgrade_button = gameObject.transform.Find("basement_functions_canva").transform.Find("options_panel").transform.Find("upgrade_button").gameObject;
        upgrade_button_price_panel = upgrade_button.transform.Find("price_panel").gameObject;
        upgrade_button_price = upgrade_button_price_panel.transform.Find("price").gameObject;
        option_menu.enabled = false;
        options_panel_go = option_menu.transform.Find("options_panel").gameObject;
        remove_button = options_panel_go.transform.Find("remove_button").gameObject;
        options_panel_go.GetComponent<Image>().CrossFadeAlpha(0f, 0f, false);
        remove_button.GetComponent<Image>().CrossFadeAlpha(0f, 0f, false);
        upgrade_button.GetComponent<Image>().CrossFadeAlpha(0f, 0f, false);
        upgrade_button_price_panel.GetComponent<Image>().CrossFadeAlpha(0f, 0f, false);
        upgrade_button_price.GetComponent<Text>().CrossFadeAlpha(0f, 0f, false);
        }
    void DeactivateUpgradeButton()
        {
        Color deact_color = new Color(1, 1, 1, 0.1f);
        upgrade_button.GetComponent<Image>().color = deact_color;
        upgrade_button_price_panel.GetComponent<Image>().color = deact_color;
        upgrade_button_price.GetComponent<Text>().color = deact_color;
        upgrade_button_price.GetComponent<Text>().text = "-";
        }
    void ActivateUpgradeButton(int price)
        {
        upgrade_button.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        upgrade_button_price_panel.GetComponent<Image>().color = new Color(1, 0.76f, 0, 0.66f);
        upgrade_button_price.GetComponent<Text>().color = new Color(0, 0, 0, 1);
        upgrade_button_price.GetComponent<Text>().text = price.ToString();
        }
    // Basement box was clicked by mouse
    void OnMouseDown()
        {
        // Case if tower is set up
        if (state == 1)
            {
            
            ShowHideOptionsMenu();
            return;
            }
        // Different actions for different selected towers
        switch (game_logic.selected_turret_index)
            {
            case 0:
                if (!TryToBuildTower("game_units/towers/tower_1", DataStore.getTowerData(0)))
                    {
                    DeactivateUpgradeButton();
                    }
                break;
            case 1:
                if (!TryToBuildTower("game_units/towers/tower_2", DataStore.getTowerData(1)))
                    {
                    DeactivateUpgradeButton();
                    }
                else 
                {
                    ActivateUpgradeButton(DataStore.getTowerData(4).cost);
                    }
                break;
            case 2:
                if (!TryToBuildTower("game_units/towers/tower_3", DataStore.getTowerData(2)))
                    {
                    DeactivateUpgradeButton();
                    }
                break;
            case 3:
                if (!TryToBuildTower("game_units/towers/tower_4", DataStore.getTowerData(3)))
                    {
                    DeactivateUpgradeButton();
                    }
                break;
            }
        }

    bool TryToBuildTower(string modelPath, data_store_logic.TowerData data)
        {
        turret = Resources.Load(modelPath) as GameObject;
        int turret_cost = data.cost;
        if (turret_cost <= game_logic.money)
            {
            Instantiate(turret, transform.position, transform.rotation, transform);
            game_logic.SubMoney(turret_cost);
            state = 1;
            return true;
            }
        return false;
        }
    void ShowHideOptionsMenu()
        {
        if (!option_menu.enabled)
            {
            ChangeOptionsMenuActiveState();
            options_panel_go.GetComponent<Image>().CrossFadeAlpha(OM_transparency, OM_fade_time, false);
            remove_button.GetComponent<Image>().CrossFadeAlpha(OM_transparency, OM_fade_time, false);
            upgrade_button.GetComponent<Image>().CrossFadeAlpha(OM_transparency, OM_fade_time, false);
            upgrade_button_price_panel.GetComponent<Image>().CrossFadeAlpha(OM_transparency, OM_fade_time, false);
            upgrade_button_price.GetComponent<Text>().CrossFadeAlpha(OM_transparency, OM_fade_time, false);
            }
        else
            {
            Invoke("ChangeOptionsMenuActiveState", OM_fade_time);
            options_panel_go.GetComponent<Image>().CrossFadeAlpha(0f, OM_fade_time, false);
            remove_button.GetComponent<Image>().CrossFadeAlpha(0f, OM_fade_time, false);
            upgrade_button.GetComponent<Image>().CrossFadeAlpha(0f, OM_fade_time, false);
            upgrade_button_price_panel.GetComponent<Image>().CrossFadeAlpha(0f, OM_fade_time, false);
            upgrade_button_price.GetComponent<Text>().CrossFadeAlpha(0f, OM_fade_time, false);
            }
        }
    void ChangeOptionsMenuActiveState()
        {
        if (option_menu.enabled)
            {
            option_menu.enabled = false;
            }
        else
            {
            option_menu.enabled = true;
            }
        }
    // Removing tower
    public void RemoveTower(bool not_upgrade=true)
        {
        foreach (Transform child in transform)
            {
            // tower object was found
            if (child.CompareTag("tower"))
                {
                DeactivateUpgradeButton();
                Destroy(child.gameObject);
                if (not_upgrade)
                    {
                    switch (child.name)
                        {
                        case "tower_1(Clone)":
                            print("+ money");
                            game_logic.AddMoney(DataStore.getTowerData(0).cost / 2);
                            break;
                        case "tower_2(Clone)":
                            print("+ money");
                            game_logic.AddMoney(DataStore.getTowerData(1).cost / 2);
                            break;
                        case "tower_2_1(Clone)":
                            print("+ money");
                            game_logic.AddMoney((DataStore.getTowerData(4).cost + DataStore.getTowerData(1).cost) / 2);
                            break;
                        case "tower_3(Clone)":
                            print("+ money");
                            game_logic.AddMoney(DataStore.getTowerData(2).cost / 2);
                            break;
                        case "tower_4(Clone)":
                            print("+ money");
                            game_logic.AddMoney(DataStore.getTowerData(3).cost / 2);
                            break;
                        default:
                            break;
                        }
                    }
                }
            }
        if (not_upgrade)
            {
            state = 0;
            }
        
        option_menu.enabled = false;
        }
    // Upgrading tower
    public void UpgradeTower()
        {
        GameObject upgraded_tower_object = null;
        int upgraded_tower_cost = 0;
        print("Upgrading tower");
        foreach (Transform child in transform)
            {
            if (child.CompareTag("tower"))
                {
                switch (child.name)
                    {
                    case "tower_1(Clone)":
                        print("found tower 1");
                       
                        break;
                    case "tower_2(Clone)":
                        print("found tower 2");
                        upgraded_tower_object = Resources.Load("game_units/towers/tower_2_1", typeof(GameObject)) as GameObject;
                        upgraded_tower_cost = DataStore.getTowerData(4).cost;
                        print("upgrade_cost = " + upgraded_tower_cost);
;                        break;
                    case "tower_3(Clone)":
                        print("found tower 1");

                        break;
                    }
                if (upgraded_tower_object!=null && upgraded_tower_cost <= game_logic.money)
                    {
                    RemoveTower(false);
                    Instantiate(upgraded_tower_object, transform.position, transform.rotation, transform);
                    DeactivateUpgradeButton();
                    print("- money3");
                    game_logic.SubMoney(upgraded_tower_cost);
                    return;
                    }
        
                }
            }
        }
	// Update is called once per frame
	void Update () {
	
	}
}
