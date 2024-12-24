using UnityEngine;

public class ColorSelector: MonoBehaviour
{
    [SerializeField] Material material;
    UnityEngine.UI.Toggle toggle;
    UIManager uIManager;

    public void Init(UIManager uIManager)
    {
        toggle = GetComponent<UnityEngine.UI.Toggle>();
        toggle.isOn = false;
        toggle.onValueChanged.AddListener(value => ChangeToggle(value));
        this.uIManager = uIManager;
    }

    public void ChangeToggle(bool value)
    {
        if(value)
        {
            if(uIManager.currentToggle == null)
            {
                uIManager.currentToggle = this;
            }
            else
            {
                uIManager.currentToggle.ToggleValueChange(false);
                uIManager.currentToggle = this;
            }
        }
        else
        {
            if(uIManager.currentToggle == this)
            {
                uIManager.currentToggle = null;
            }
        }
    }

    void ToggleValueChange (bool isActive)
    {
        toggle.isOn = isActive;
    }

    public Material GetMaterial()
    {
        return material;
    }
}
