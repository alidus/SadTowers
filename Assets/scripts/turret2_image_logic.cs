using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class turret2_image_logic : MonoBehaviour
    {
    private Image this_ui_image;
    private game_logic game_logic;
    private int this_turret_index = 1;
    // Use this for initialization
    void Start()
        {
        this_ui_image = gameObject.GetComponent<Image>();
        game_logic = GameObject.Find("Game_Logic").GetComponent<game_logic>();

        }
    public void MouseOn()
        {
        if (game_logic.selected_turret_index != this_turret_index)
            {
            this.this_ui_image.color = new Color(0, 1, 1, 0.4f);
            }
        }
    public void MouseOff()
        {
        if (game_logic.selected_turret_index != this_turret_index)
            {
            this.this_ui_image.color = new Color(1, 1, 1, 0.4f);
            }
        }
    public void Select()
        {
        if (game_logic.selected_turret_index != this_turret_index)
            {
            game_logic.ChangeSelectedTurretTo(this_turret_index);
            }
        else
            {
            game_logic.ChangeSelectedTurretTo(-1);
            }

        }

    }
