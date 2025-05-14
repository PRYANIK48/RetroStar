using UnityEngine;
using UnityEngine.UI;

public class LanguageButton : MonoBehaviour
{
    public Sprite[] flags;
    public Image flagButton;
    public int whatLang = 0;

    public void ChangeLang()
    {
        whatLang++;
        

        if (whatLang == flags.Length)
        {
            whatLang = 0;
            
        }
        flagButton.sprite = flags[whatLang];
    }

    //Это я для себя если что.
    //Добавить систему сохранения через префсы то какой сейчас язык. Добавить скрипт LanguageManager, Который будет проверять какой сейчас язык и менять всё на сцене на перевод, ИСПРАВИТЬ БАГ С ЕДИНИЧНОЙ СМЕНОЙ.

}
