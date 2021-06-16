### XYZ - Домашняя работа 6

### Исправление недочётов - попытка next

Переименовал компонент, отвечающий за здоровье

```
Было - HeroHealthControl
Стало - ControlHealth
```

Новая версия компонента управления здоровьем

```
public class ControlHealth : MonoBehaviour {
    [SerializeField] private int lives = 5;
    
    public int Lives => lives;

    public void AddLives(int value) {
        lives += value;
        string message = "Lives: " + lives;
        Debug.Log(message);
    }
}
```

Теперь количество жизней изменяется следующим образом

```
[SerializeField] private int liveDelta = -1;
...
_controlHealth.AddLives(liveDelta);
```

Количество жизней теперь получаю так

```
int heroLiveInt = _controlHealth.Lives;
```

В скрипте HeartLive вынес логику из OnDestroy

```
private void OnTriggerEnter2D(Collider2D col) {
    if (col.gameObject != _hero) return;
    ControlHealth controlHealth = _hero.GetComponent<ControlHealth>();
    controlHealth.AddLives(liveDelta);
    Destroy(gameObject);
}
```

Монетки разных стоимостей сделал разными префабами

Изменил скрипт MoneyControl

Теперь у каждой монетки есть своя стоимость

Стоимость монетки передаётся в метод увеличения количества собранных монет героем

```
public class MoneyControl : MonoBehaviour {
    [SerializeField] private int costs = 0;
    
    private void OnTriggerEnter2D(Collider2D other) {
        HeroControl heroControl = other.GetComponent<HeroControl>();
        if (!heroControl) return;
        heroControl.MoneyAdd(costs);
        Destroy(gameObject);
    }
}
```

Решил не усложнять с OnTriggerEnter2D в отдельном компоненте
