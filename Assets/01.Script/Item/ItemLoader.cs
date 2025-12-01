using Newtonsoft.Json.Linq;
using UnityEngine;
using System.IO;

public class ItemLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 1) JSON 문자열 가져오기 (예: Resources 또는 직접 경로에서)
        TextAsset itemJson = Resources.Load<TextAsset>("Item"); // Resources/Item.json
        string jsonText = itemJson.text;

        // 2) 최상위 JToken / JObject 파싱
        JToken root = JToken.Parse(jsonText);      // 또는 JObject.Parse(jsonText)

        // 3) "players" 배열 꺼내기
        JArray players = (JArray)root["players"];

        // 4) 각 아이템 돌면서 정보 읽기
        foreach (JToken p in players)
        {
            int id = (int)p["ID"];
            string name = (string)p["Name"];
            string type = (string)p["Type"];

            if (type == "item")
            {
                Debug.Log($"Currency - ID: {id}, Name: {name}");
            }
        }
    }
}
