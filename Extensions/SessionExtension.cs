using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

public static class SessionExtensions
{
    // Metoda rozszerzająca ISession do zapisywania obiektów w formacie JSON
    public static void SetObjectAsJson(this ISession session, string key, object value)
    {
        session.SetString(key, JsonConvert.SerializeObject(value));
    }

    // Metoda rozszerzająca ISession do pobierania obiektów w formacie JSON
    public static T GetObjectFromJson<T>(this ISession session, string key)
    {
        var value = session.GetString(key);  // Pobieramy string z sesji
        return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);  // Deserializujemy do typu T
    }
}
