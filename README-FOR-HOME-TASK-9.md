# XYZ

# Домашняя работа №9

В начале запускаем сцену - `LevelA`

## Изменял скрипты

1) `HeroControl`

2) `ControlHealth`

3) `GuiInfoRender`

4) `SessionStoreControl`

5) `WinGameControl`

## Описание изменений

### HeroControl

В методе `Start` задаю количество денег героя с помощью хранимых в сессии данных.

### ControlHealth

В методе `Start` задаю здоровье героя с помощью хранимых в сессии данных.

### GuiInfoRender

В методе `OnGUI` осуществляется циклическая отрисовка информации о количестве денег и здоровья героя.

### SessionStoreControl

В методе `Start` запрещаем удалять объект при смене сцены с помощью `DontDestroyOnLoad(gameObject);`

В методе `SetStoreValues` идёт извлечение текущих свойств героя (денег и здоровья) и сохранение их в поля сессионного объекта.

### WinGameControl

Основная задача - при касании героя осуществлять переход на следующий уровень.

Перед переходом на новый уровень вызывается метод сохранения данных в сессию.
