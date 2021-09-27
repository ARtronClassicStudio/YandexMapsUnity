# Yandex Maps Unity Beta
Плагин Яндекс карты(Статическая) для Unity.

Так как Яндекс карты является русскоязычным сервисом то нет смысла переводить страницу и комментари в dll на английский язык. 
Если возникнут вопросы то пишите [тут](https://github.com/ARtronClassicStudio/YandexMapsUnity/issues)
# Установка:
[Cкачайте пакет](https://github.com/ARtronClassicStudio/YandexMapsUnity/releases/download/Beta/YandexMapsBeta.unitypackage) и импортируйте его, после компиляции dll установка будет завершена. Смотрите [примеры](https://github.com/ARtronClassicStudio/YandexMapsUnity#%D0%B7%D0%B0%D0%B3%D1%80%D1%83%D0%B7%D0%BA%D0%B0-%D0%BA%D0%B0%D1%80%D1%82%D1%8B-%D0%BF%D0%BE-%D0%BA%D0%BD%D0%BE%D0%BF%D0%BA%D0%B5-ui) или посмотрите [видео урок](https://youtu.be/RWlZnqLix_4).
# Системные Требования:
Не ниже Unity 2020.3.14f1 (LTS).

# Примеры:

# Загрузка карты по кнопке (UI):
![](Resources/1.gif)

```c#
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using YandexMaps;

public class Sample : MonoBehaviour
{
    public RawImage image; 
    public Map.TypeMap typeMap; 
    public Map.TypeMapLayer mapLayer; 

    public void LoadMap()
    {
        Map.EnabledLayer = true; 
        Map.Size = 4; 
        Map.SetTypeMap = typeMap;
        Map.SetTypeMapLayer = mapLayer;
        Map.LoadMap();
        StartCoroutine(GetTexture());
    }

    IEnumerator GetTexture()
    {
        yield return new WaitForSeconds(1f);
        image.texture = Map.GetTexture;
    }

}
```
# Добавление маркеров на карту:
![](Resources/2.gif)

```C#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YandexMaps;

public class Sample : MonoBehaviour
{
    public RawImage image;
    public Map.TypeMap typeMap;
    public Map.TypeMapLayer mapLayer;
    public List<Vector2> markers = new List<Vector2>();

    public void LoadMap()
    {
        Map.EnabledLayer = true;
        Map.Size = 4;
        Map.SetTypeMap = typeMap;
        Map.SetTypeMapLayer = mapLayer;
        Map.SetMarker = markers;
        Map.LoadMap();
        StartCoroutine(GetTexture());
    }

    IEnumerator GetTexture()
    {
        yield return new WaitForSeconds(1f);
        image.texture = Map.GetTexture;

    }
}
```


# Все доступные на данный момент Yandex API:
```C#
# Map.EnabledLayer : bool 
Map.GetTexture : Texture (Только для чтение)
Map.GetTexture2D : Texture2D (Только для чтение)
Map.Height : int 
Map.Width : int
Map.Latitude : float
Map.Longitude : float
Map.LoadMap() : Метод
Map.UpdateLoadMap() : Метод
Map.SetMarker : List<Vector2>()
Map.SetTypeMap : enum
Map.SetTypeMapLayer : enum
Map.Size : int
Map.TypeMap : enum   
Map.TypeMapLayer : enum
```
