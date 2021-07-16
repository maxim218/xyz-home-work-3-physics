# XYZ

## Домашняя работа 8

Действие на сцене: `LevelA`

### Написанные скрипты

1) `BarrelControl` - управление разрушаемой бочкой

2) `SwordControl` - управление мечом, подбираемым героем на карте

3) `HeroKnifeControl` - скрипт на герое, создающий экземпляры мечей для удара

4) `AttackSword` - управление ударом конкретного меча, контроль удара бочек

### Управление

Удар - клавиша `R`

### Особенности

Герой имеет возможность наносить удары только после подбора на карте меча.

При ударе происходят следующие действия:

- создаётся новый экземпляр меча около рук героя

- автоматически запускается анимация меча 

- в середине анимации прямо из анимации вызывается функция, проверяющая касание мечом бочек и наносящая урон

- в конце анимации вызывается функция удаления экземпляра меча 

Также имеется особенность: после создания меча происходит блокировка, запрещающая создавать новые мечи. При удалении меча в конце его анимации блокировка снимается.


