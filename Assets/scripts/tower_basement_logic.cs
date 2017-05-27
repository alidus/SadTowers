using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class tower_basement_logic : MonoBehaviour {
    private game_logic game_logic;
    GameObject turret;
    Canvas option_menu;
    public GameObject tower1;
    public GameObject tower2;
    public GameObject tower2_1;
    private GameObject options_panel_go;
    private GameObject remove_button;
    private GameObject upgrade_button;
    private GameObject upgrade_button_price;
    private GameObject upgrade_button_price_panel;
    private data_store_logic Data_Store;
    private float OM_fade_time = 0.2f;
    private float OM_transparency = 0.9f;
    private int state; // 0 - empty, 1 - tower_built
	// Use this for initialization
	void Start () {
        Data_Store = GameObject.Find("Data_Store").GetComponent<data_store_logic>();
        game_logic = GameObject.Find("Game_Logic").GetComponent<game_logic>();
        option_menu = gameObject.transform.FindChild("basement_functions_canva").GetComponent<Canvas>();
        upgrade_button = gameObject.transform.FindChild("basement_functions_canva").transform.FindChild("options_panel").transform.FindChild("upgrade_button").gameObject;
        upgrade_button_price_panel = upgrade_button.transform.FindChild("price_panel").gameObject;
        upgrade_button_price = upgrade_button_price_panel.transform.FindChild("price").gameObject;
        option_menu.enabled = false;
        options_panel_go = option_menu.transform.FindChild("options_panel").gameObject;
        remove_button = options_panel_go.transform.FindChild("remove_button").gameObject;
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
        // if not:
        int turret_cost;
        switch (game_logic.selected_turret_index)
            {
            case 0:
                turret = Resources.Load("game_units/towers/tower_1") as GameObject;
                turret_cost = Data_Store.data.tower_1.cost;
                print("cost determined = " + turret_cost.ToString());
                if (turret_cost <= game_logic.money)
                    {
                    Instantiate(turret, transform.position + new Vector3(0, 1.45f, 0), transform.rotation, transform);
                    game_logic.SubMoney(turret_cost);
                    state = 1;
                    //ActivateUpgradeButton(turret_cost);
                    DeactivateUpgradeButton();
                    }
                break;
            case 1:
                turret = Resources.Load("game_units/towers/tower_2") as GameObject;
                turret_cost = Data_Store.data.tower_2.cost;
                if (turret_cost <= game_logic.money)
                    {
                    Instantiate(turret, transform.position, transform.rotation, transform);
                    game_logic.SubMoney(turret_cost);
                    state = 1;
                    //TODO: fix this shit!!!!
                    ActivateUpgradeButton(Data_Store.data.tower_2_1.cost);
                    }
                break;
            }
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
                    if (child.name == "tower_1(Clone)")
                        {
                        print("+ money");
                        game_logic.AddMoney(Data_Store.data.tower_1.cost / 2); }
                    else if (child.name == "tower_2(Clone)")
                        {
                        print("+ money");
                        game_logic.AddMoney(Data_Store.data.tower_2.cost / 2); }
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
                        upgraded_tower_cost = Data_Store.data.tower_2_1.cost;
                        print("upgrade_cost = " + upgraded_tower_cost);
;                        break;
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
