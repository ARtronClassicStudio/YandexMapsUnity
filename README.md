# Yandex Maps Unity
Яндекс карты(Статическая) для Unity.

# Системные Требования:
не ниже Unity 2020.3.14f1 (LTS)

# Примеры:

# Загрузка карты по кнопке (UI)
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
# Добавление маркеров на карте
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

# Доступный список Yandex API:
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
