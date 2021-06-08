### XYZ - Домашняя работа 6

### Исправление недочётов

Исправленные файлы:

- HeroHealthControl.cs

- PlayerMoving.cs

- EnemyControl.cs

- HeartLive.cs

- PortalControl.cs

Получение ссылки на объект главного героя

```
HeroControl heroControl = (HeroControl)FindObjectOfType(typeof(HeroControl));
_hero = heroControl.gameObject;
```

Теперь за здоровье героя отвечает компонент HeroHealthControl

```
HeroHealthControl heroHealthControl = _hero.GetComponent<HeroHealthControl>();
heroHealthControl.AddThreeLives();
```

Теперь контроль касания героя я проверяю так

```
private void OnTriggerEnter2D(Collider2D col) {
    if(col.gameObject == _hero) MoveToExit();
}
```

Коллизии я использую для проверки касания врага в компоненте EnemyControl

