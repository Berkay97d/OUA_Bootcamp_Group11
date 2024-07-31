using _Scripts;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    [SerializeField] private Button button1;
    [SerializeField] private Button button2;
    [SerializeField] private Button button3;

    private bool[] buttonUsed = new bool[3];
    private bool moveSuccessful = false;

    void Start()
    {
        button1.onClick.AddListener(() => OnButtonClick(0));
        button2.onClick.AddListener(() => OnButtonClick(1));
        button3.onClick.AddListener(() => OnButtonClick(2));

        UpdateButtonColors();
        
    }

    void Update()
    {
        // Gerekli hamlenin başarılı olup olmadığını kontrol eder
    }

    void OnButtonClick(int buttonIndex)
    {
        if (!buttonUsed[buttonIndex])
        {
            buttonUsed[buttonIndex] = false;
            UpdateButtonColors();

            // Burada gerekli hamlenin başarılı olup olmadığını kontrol et
            //moveSuccessful = CheckMoveSuccess();
            moveSuccessful = true; 

            // Hamle başarılı olduysa herhangi bir şey yapma
            // Hamle başarısız olduysa butonu tekrar aktif hale getirmek için 
            // kodu aşağıda bulunan ResetButtons fonksiyonu kullan
        }
    }

    void UpdateButtonColors()
    {
        UpdateButtonColor(button1, buttonUsed[0]);
        UpdateButtonColor(button2, buttonUsed[1]);
        UpdateButtonColor(button3, buttonUsed[2]);
    }

    void UpdateButtonColor(Button button, bool used)
    {
        ColorBlock colors = button.colors;

        if (used)
        {
            colors.normalColor = Color.red;
            colors.highlightedColor = Color.red;
            colors.pressedColor = Color.red;
            button.interactable = false;
        }
        else
        {
            colors.normalColor = Color.white;
            colors.highlightedColor = Color.green;
            colors.pressedColor = Color.green;
            button.interactable = true;
        }

        button.colors = colors;
    }

    public void ResetButtons()
    {
        for (int i = 0; i < buttonUsed.Length; i++)
        {
            buttonUsed[i] = false;
        }
        UpdateButtonColors();
    }

    // Gerekli hamlenin başarılı olup olmadığını kontrol eden bir fonksiyon 
    // bool CheckMoveSuccess()
    // {
    //     // Hamlenin başarılı olup olmadığını kontrol edin ve true/false döndürür
    // }
}