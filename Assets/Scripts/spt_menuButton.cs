using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using VRStandardAssets.Utils;

public class spt_menuButton : MonoBehaviour
{

    [SerializeField]
    private VRInteractiveItem m_InteractiveItem;       // The interactive item for where the user should click to load the level.

    private Button btn;

    private void OnEnable()
    {
        m_InteractiveItem.OnClick += HandleClick;
        m_InteractiveItem.OnDown += HandleDown;
        m_InteractiveItem.OnUp += HandleUp;
    }

    private void OnDisable()
    {
        m_InteractiveItem.OnClick -= HandleClick;
        m_InteractiveItem.OnDown -= HandleDown;
        m_InteractiveItem.OnUp -= HandleUp;
    }

    // Use this for initialization
    virtual protected void Start()
    {
        btn = GetComponent<Button>();
    }

    // Update is called once per frame
    virtual protected void Update()
    {

    }

    virtual protected void HandleClick()
    {
        btn.image.color = new Color32(151, 0, 0, 255);
        Debug.Log(gameObject.name + " clicked!");
    }

    virtual protected void HandleDown()
    {
        
    }

    virtual protected void HandleUp()
    {
        
    }

}

