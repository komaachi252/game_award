﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option_Cursor : MonoBehaviour
{
    int m_current_index = 0;  //  インデックス
    float m_pos_y;  //  初期Ｙ座標
    public GameObject[] m_select_bars;  //  選択肢
    bool m_is_pos_set;
    bool m_is_init_pos_y;
    void Start()
    {
        m_current_index = 0;
        //  初期Ｙ座標を保持しておく
        m_is_pos_set = false;
        foreach (var bar in m_select_bars)
        {
            bar.GetComponent<Title_Select_Bar>().Set_Index(m_current_index);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //  一度だけ呼び、Y座標を取得する
        if(!m_is_init_pos_y && this.gameObject.transform.parent.gameObject.transform.localScale.x == 1.0f)
        { 
            m_pos_y = this.gameObject.transform.position.y;
            m_is_init_pos_y = true;
        }
        if(!m_is_pos_set && this.gameObject.transform.parent.gameObject.transform.localScale.x == 1.0f)
        {
            m_is_pos_set = true;
            this.gameObject.GetComponent<RectTransform>().position = new Vector3(this.transform.position.x, m_pos_y, this.transform.position.z);
            //  BGMを選択中にする
            foreach (var bar in m_select_bars)
            {
                bar.GetComponent<Title_Select_Bar>().Set_Index(m_current_index);
            }
        }

        Cursor_Input();
    }
    private void FixedUpdate()
    {
        //  常に回転する
        this.transform.Rotate(0, 0, -1);
    }

    void Cursor_Input()
    {
        var idx = m_current_index;
        if (Input.GetKeyDown(KeyCode.W))
        {
            idx--;
            if (idx < 0)
            {
                idx = 2;
            }
            FindObjectOfType<Audio_Manager>().Play("select");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            idx++;
            if (idx > 2)
            {
                idx = 0;
            }
            FindObjectOfType<Audio_Manager>().Play("select");
        }
        if (Input.GetKeyDown(KeyCode.Return) && idx == 0)
        {

        }
        if (Input.GetKeyDown(KeyCode.Return) && idx == 1)
        {
            //FindObjectOfType<Audio_Manager>().Play("enter");
            //m_fade.GetComponent<Game_Fade>().Fade_Start(20, true, "WorldScene");
        }
        if (Input.GetKeyDown(KeyCode.Return) && idx == 2)
        {
            FindObjectOfType<Audio_Manager>().Play("cancel");
            m_is_pos_set = false;
            m_current_index = 0;
            foreach (var bar in m_select_bars)
            {
                bar.GetComponent<Title_Select_Bar>().Reset_Scale();
            }
            this.GetComponentInParent<Option_Menu>().Close_Menu();
            return;
        }
        Cursor_Move(idx);
    }

    void Cursor_Move(int new_cursor_idx)
    {
        //  同じ場合は戻す
        if (m_current_index == new_cursor_idx) return;
        foreach (var bar in m_select_bars)
        {
            bar.GetComponent<Title_Select_Bar>().Set_Index(new_cursor_idx);
        }
        //  インデックスに応じてＹ座標を移動
        m_current_index = new_cursor_idx;
        this.gameObject.GetComponent<RectTransform>().position = new Vector3(this.transform.position.x, m_pos_y + m_current_index * -20.0f, this.transform.position.z);
    }
}