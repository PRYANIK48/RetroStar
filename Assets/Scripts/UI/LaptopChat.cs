﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LaptopChat : MonoBehaviour
{
    public InputField inputField;
    public TMP_Text chatOutput;
    public GameObject ChatRoot;

    [SerializeField] GameObject LaptopUI;

    private Dictionary<string, string> responses;

    string yellow = "<color=#FFFFAA>";
    string close = "</color>";


    void Start()
    {
        #region Словарь
        responses = new Dictionary<string, string>()
        {
            {"prunus",yellow + "Открыт новый фон в меню!" + close},
            {"malus",  yellow + "Фон отобран!" + close},
            {"Lololoshka", "Ты думаешь, всё настолько просто?"},
            {"Лололошка", "Ты думаешь, всё настолько просто?"},
            {"lololowka", "Ты думаешь, всё настолько просто?"},
            {"Lo", "Ты дум, вс настк прост?"},
            {"Ло", "Ты дум, вс настк прост?"},
            {"JDH", "?отсорп окьлотсан ёсв, ьшеамуд ыТ"},
            {"reverto", "Откат операционной системы до Windows 98...\n1%... 2%... 3%..."},
            {"Построить комнату", "Пожалуйста, уточните ваш запрос. Я глупая утка, которая ни на что не способна, кроме как строить комнаты и рекламировать свою компанию!"},
            {"REHOBOAM", "Хотел бы я столько же оперативной памяти, эх!"},
            {"РЕХОБОАМ", "Хотел бы я столько же оперативной памяти, эх!"},
            {"Междумирец", "Первый? Второй? Третий?  ... Четвертый?"},
            {"Глорп", "ГЛООООООООРП"},
            {"Рома", "Похоже на имя какого-то ютубера из другого мира, снимающего кубическую игру десяток лет подряд. Шучу, это же невозможно! Ха-ха!"},
            {"Роман", "Похоже на имя какого-то ютубера из другого мира, снимающего кубическую игру десяток лет подряд. Шучу, это же невозможно! Ха-ха!"},
            {"Роман Фильченков", "Похоже на имя какого-то ютубера из другого мира, снимающего кубическую игру десяток лет подряд. Шучу, это же невозможно! Ха-ха!"},
            {"Рома Фильченков", "Похоже на имя какого-то ютубера из другого мира, снимающего кубическую игру десяток лет подряд. Шучу, это же невозможно! Ха-ха!"},
            {"Дальше", "Больше!"},
            {"Токсик", "Тут, вообще-то, нет пароля. К твоему счастью."},
            {"Toxic", "Тут, вообще-то, нет пароля. К твоему счастью."},
            {"Джошуа", "Он с нами в одной комнате, да?"},
            {"Дискриминант", "D=(b×b)-4ac\nЭто неправильно, но у меня нет символа квадрата!"},
            {"Lololoshka Retro Star", "Да."},
            {"Retro star", "Да."},
            {"Kvolm", "Не думаю, что такое существует."},
            {"Кволм", "Не думаю, что такое существует."},
            {"Междумирье", "[Данные удалены]"},
            {"поэна", "[Данные удалены]"},
            {"видомния", "[Данные удалены]"},
            {"голос времени", "[Данные удалены]"},
            {"Пчелиные ножки", "[Ошибка]"},
            {"Да", "Это был риторический вопрос."},
            {"Нет", "А я и не собирался."},
            {"Запрос", "Как оригинально! Нет."},
            {"Minecraft", "Установить вам новейшую версию вирусов без майнкрафта?"},
            {"Майнкрафт", "Установить вам новейшую версию вирусов без майнкрафта?"},
            {"Фляжка", "Ты же... имеешь в виду реальную фляжку, так..?"},
            {"Фляга", "Ты же... имеешь в виду реальную фляжку, так..?"},
            {"Камыш", "Нет, не рогоз. Думаю, тут имеется в виду именно камыш..."},
            {"Тася", "Нет, не рогоз. Думаю, тут имеется в виду именно камыш..."},
            {"Rumaruka", "Вот он реально первый!"},
            {"Румарука", "Вот он реально первый!"},
            {"Ромарука", "рУмарука!"},
            {"FreedomDeath", "О, вы хотите взять у меня интервью?"},
            {"Дюпы по Полочкам", "О, вы хотите взять у меня интервью?"},
            {"Тот самый тестер", "О, вы хотите взять у меня интервью?"},
            {"TimeConqueror", "(молчит)"},
            {"Андрей который молчит", "(молчит)"},
            {"Андрей", "Который из?"},
            {"MrLololoshka EDIT", "И куда он едет? Не понятно. Возможно, он едет на каком-то бесконечном поезде."},
            {"EDIT", "И куда он едет? Не понятно. Возможно, он едет на каком-то бесконечном поезде."},
            {"Эдит", "И куда он едет? Не понятно. Возможно, он едет на каком-то бесконечном поезде."},
            {"Лололошка Эдит", "И куда он едет? Не понятно. Возможно, он едет на каком-то бесконечном поезде."},
            {"Мистер Лололошка Эдит", "И куда он едет? Не понятно. Возможно, он едет на каком-то бесконечном поезде."},
            {"Mr Lololoshka EDIT", "И куда он едет? Не понятно. Возможно, он едет на каком-то бесконечном поезде."},
            {"TheAnd", "Анд? Не знаю, я называю его Энд."},
            {"Анд", "Анд? Не знаю, я называю его Энд."},
            {"Зе Анд", "Анд? Не знаю, я называю его Энд."},
            {"ЗеАнд", "Анд? Не знаю, я называю его Энд."},
            {"The And", "Анд? Не знаю, я называю его Энд."},
            {"May", "Хм, а возможно ли кинуть страйк на компьютер? Мне стоит бояться?"},
            {"Мэй", "Хм, а возможно ли кинуть страйк на компьютер? Мне стоит бояться?"},
            {"Stakov", "А вы слышали, что он вернулся?"},
            {"Стаков", "А вы слышали, что он вернулся?"},
            {"Рэйнард", "Скорее всего, вы неправильно ставите ударение."},
            {"Рейнард", "Скорее всего, вы неправильно ставите ударение."},
            {"Reynard", "Скорее всего, вы неправильно ставите ударение."},
            {"Калеф", "Не грибочек?!"},
            {"Kalef", "Не грибочек?!"},
            { "dimka show", "Ну, давай! Устрой битву Горбатка и Начала!" },
            { "dimkashow", "Ну, давай! Устрой битву Горбатка и Начала!" },
            { "димка шоу", "Ну, давай! Устрой битву Горбатка и Начала!" },
            { "димкашоу", "Ну, давай! Устрой битву Горбатка и Начала!" },
            { "стринги джодаха", "Почему?" },
            { "стринги смотрящего", "ПОЧЕМУ?!" },
            { "скинт", "Вы хотите заказать доставку скинта на дом?" },
            { "антискинт", "Пожалуйста, не берите его с собой в порталы. Я серьезно. Хотя, кто меня послушает... берите на здоровье." },
            { "анти-скинт", "Пожалуйста, не берите его с собой в порталы. Я серьезно. Хотя, кто меня послушает... берите на здоровье." },
            { "анти скинт", "Пожалуйста, не берите его с собой в порталы. Я серьезно. Хотя, кто меня послушает... берите на здоровье." },
            { "скинтонит", "Пожалуйста, не берите его с собой в порталы. Я серьезно. Хотя, кто меня послушает... берите на здоровье." },
            { "меч", yellow + "Заказ оформлен успешно! Доставка завершена." + close },
            { "деревянный меч", yellow +"Заказ оформлен успешно! Доставка завершена." + close },
            { "оружие",  yellow + "Заказ оформлен успешно! Доставка завершена." +close },
            { "помогите", "А помогите себе сами!" },
            { "помощь", "А помогите себе сами!" },
            { "help", "А помогите себе сами!" },
            { "sos", "А помогите себе сами!" },
            { "поддержка", "А помогите себе сами!" },
            { "синклит", "Мой календарь даёт сбой." },
            { "смотрящий", "Нет, в меня не встроена веб-камера." },
            { "джодах", "Фигурка... она... двигается? Но как это возможно?" },
            { "новое поколение", "Лефтарион, да? Где-то я уже слышал это имя... задолго до..." },
            { "лефтарион", "Лефтарион, да? Где-то я уже слышал это имя... задолго до..." },
            { "гектор", "Жив." },
            { "доктор блэк", "А вы играли в визуальную новеллу?" },
            { "блэк", "А вы играли в визуальную новеллу?" },
            { "арнир", "Сколько ему лет?!" },
            { "горбатик", "Ты любишь иглобрюхов? Мы любим иглобрюхов!" },
            { "игра бога", "Это правда была просто игра в шахматы? Интересно, а доска сделана из древа жизни?" },
            { "бастиан", "Пельмени..." },
            { "картер", "Мне кажется, где-то он всё-таки есть." },
            { "люциус", "Полубог и его змея! Звучит как отличный дуэт." },
            { "айко", "Полубог и его змея! Звучит как отличный дуэт." },
            { "идеальный мир", "Кажется, это был намёк на что-то." },
            { "тринадцать огней", "Эй! Они ведь так и не озеленили его!" },
            { "последняя реальность", "Знаешь, я чувствую родство с этим словосочетанием." },
            { "сердце вселенной", "Прыгнули, прыгнули!" },
            { "точка невозврата", "Этого ещё не произошло." },
            { "подсказка", "Не-а! Думай сам." },
            { "пм", "Звучит опасно." },
            { "первичная материя", "Звучит опасно." },
            { "радан", "Как там звали его игрушку?" },
            { "райя", "Ура-ура!" },
            { "райя-прайм", "Ура-ура!" },
            { "райя прайм", "Ура-ура!" },
            { "бурис", "Хара!" },
            { "бурис!", "Хара!" },
            { "хара", "Бурис!" },
            { "хара!", "Бурис!" },
            { "сдерик", "Что-то здесь не так..." },
            { "окетра", "У меня дежавью. Странно, у ноутбука разве может быть дежавью?" },
            { "то'ифэтун", "Вы хотя-бы знаете, сколько стоят их маски? Да они богачи!" },
            { "тоифэтун", "Вы хотя-бы знаете, сколько стоят их маски? Да они богачи!" },
            { "то ифэтун", "Вы хотя-бы знаете, сколько стоят их маски? Да они богачи!" },
            { "дженна", "Ох... Мой процессор... он... БА-БАХ! Шутка." },
            { "дилан", "Да где он только не побывал! Даже во мне. Ой..." },
            { "брэндон", "А может, это и к лучшему. Сами подумайте!" },
            { "невер", "Кар!" },
            { "невер из мора", "Кар!" },
            { "беренгарий", "Крутой мужик." },
            { "сайрисса", "Мальчик, или девочка? Выкидыш!" },
            { "мать", "Да не мать она!" },
            { "отец", "Шутки про хлеб... о да-а-а..." },
            { "тадмавриэль", "Шутки про хлеб... о да-а-а..." },
            { "сын", "Донор." },
            { "эграссель", "Донор." },
            { "эграсса", "Донор." },
            { "шангрин", "Essensio!" },
            { "анекдот", "Знаю я одну шутку про экстримала... рассказать?" },
            { "расскажи анекдот", "Знаю я одну шутку про экстримала... рассказать?" },
            { "анекдоты", "Знаю я одну шутку про экстримала... рассказать?" },
            { "зарплата", "" },
            { "люциус эво", "Это оказался не он..." },
            { "эво люциус", "Это оказался не он..." },
            { "эволюциус", "Это оказался не он..." },
            { "хиробрин", "Не оборачивайся." },
            { "херобрин", "Не оборачивайся." },
            { "архимаг", "Финал не завтра." },
            { "archimage", "Финал не завтра." },
            { "саня", "Финал не завтра." },
            { "повелитель времени", "Откуда..." },
            { "владыка пространства", "Ты..." },
            { "вершитель правосудия", "Знаешь?" },
            { "dota 2", "Звучит как... что-то невероятное. Но я не хочу больше это слышать." },
            { "dota", "Звучит как... что-то невероятное. Но я не хочу больше это слышать." },
            { "дота", "Звучит как... что-то невероятное. Но я не хочу больше это слышать." },
            { "дота 2", "Звучит как... что-то невероятное. Но я не хочу больше это слышать." },
            { "dip-waw", "Чмок в пупок!" },
            { "dip waw", "Чмок в пупок!" },
            { "d1p-waw", "Чмок в пупок!" },
            { "d1p waw", "Чмок в пупок!" },
            { "дип-вав", "Чмок в пупок!" },
            { "дип вав", "Чмок в пупок!" }

        };
        #endregion
    }

    public void OnInputSubmitted()
    {
        string input = inputField.text.ToLower().Trim();
        
        inputField.text = "";

        if (string.IsNullOrEmpty(input))
            return;
        
        ChatRoot.SetActive(false);
        
        string response = GetResponse(input);
        chatOutput.text= response;

        ChatRoot.SetActive(true);
    }

    private string GetResponse(string input)
    {
        foreach (var entry in responses)
        {
            if (input.Contains(entry.Key.ToLower()))
                return entry.Value;
        }
        
        return "<color=#676767>" + "Ничего не найдено." + close;
    }
    public void DisableInput()
    {
        MainInputManager.InputSystem.PlayerMap.Disable();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            MainInputManager.InputSystem.PlayerMap.Enable();
            LaptopUI.SetActive(false);
            //Да ужасно,мне лень пока переделывать
        }
    }

}
