## ДЗ - локализация меню и диалогов

### Описание

Сцена - `MainMenuScene`

### Адреса файлов с переводами

```
http://195.19.40.118/XYZ/english.json
```

```
http://195.19.40.118/XYZ/russian.json
```

### Скрипты

1) `SpeakerDialog` - в методе Start добавлена логика на проверку локализации

2) `LocalizationDontDestroy` - хранит состояние локализации (ENG или RUS) между сценами

3) `ButtonMenuControl` - добавлен метод SetButtonText для задания текста в кнопке

4) `SliderControl` - добавлен метод SetPrefixString для изменения префикса у надписи, отображающей числовое значение слайдера

5) `LocaleChangeManager` - управляет хранением элементов, у которых нужно изменить локализацию текста, а также изменяет локализацию у хранимых элементов

6) `LocaleManager` - получает ресурс в формате JSON, парсит его, осуществляет поиск переводимого элемента по ключу

7) `DownloadLocale` - собственное окно, отвечает за загрузку переводов с удалённого сервера (Window -> Download Locale)
