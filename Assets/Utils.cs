using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using System.Linq;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public static class Utils
{
    public static GameObject[] GetUIAtPosition(Vector2 position, GraphicRaycaster[] graphicRaycasters)
    {
        GameObject[] gameObjects = new GameObject[0];
        foreach (GraphicRaycaster raycaster in graphicRaycasters)
        {
            List<RaycastResult> result = new List<RaycastResult>();
            PointerEventData data = new PointerEventData(EventSystem.current);
            data.position = position;
            raycaster.Raycast(data, result);
            gameObjects = gameObjects.Concat(Array.ConvertAll(result.ToArray(), x => x.gameObject)).ToArray();
        }
        return gameObjects;
    }

    public static GameObject[] GetAllGameObjectsAtPoint(Vector2 position, GraphicRaycaster graphicRaycaster)
    {
        GameObject[] gameObjects = GetAllGameObjectsAtPoint(position);
        List<RaycastResult> result = new List<RaycastResult>();
        PointerEventData data = new PointerEventData(EventSystem.current);
        data.position = Camera.main.WorldToScreenPoint(position);
        graphicRaycaster.Raycast(data, result);

        return gameObjects.Concat(Array.ConvertAll(result.ToArray(), x => x.gameObject)).ToArray();
    }

    public static GameObject[] GetAllGameObjectsAtPoint(Vector2 position)
    {
        return Array.ConvertAll(Physics2D.OverlapPointAll(position), x => x.gameObject);
    }

	public static T CreateJaggedArray<T>(params int[] lengths)
	{
		return (T)InitializeJaggedArray(typeof(T).GetElementType(), 0, lengths);
	}

	private static object InitializeJaggedArray(Type type, int index, int[] lengths)
	{
		Array array = Array.CreateInstance(type, lengths[index]);
		Type elementType = type.GetElementType();

		if (elementType != null)
		{
			for (int i = 0; i < lengths[index]; i++)
			{
				array.SetValue(
					InitializeJaggedArray(elementType, index + 1, lengths), i);
			}
		}

		return array;
	}
    
    public static IEnumerable<T> GetEnumerableOfType<T>(params object[] constructorArgs) where T : class, IComparable
	{
		List<T> objects = new List<T>();
		foreach (Type type in 
			Assembly.GetAssembly(typeof(T)).GetTypes()
			.Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(T))))
		{
			objects.Add((T)Activator.CreateInstance(type, constructorArgs));
		}
		objects.Sort();
		return objects;
	}

	public static bool IsNullOrEmpty<T>(T[] array) where T: class
	{
		if (array == null || array.Length == 0)
			return true;
		else
			return array.All(item => item == null);
	}

	//Serialize
	public static string SerializeXML<T>(this T toSerialize) {
		XmlSerializer xml = new XmlSerializer (typeof(T));
		StringWriter writer = new StringWriter ();
		xml.Serialize (writer, toSerialize);
		return writer.ToString ();
	}

	//De-serialize
	public static T DeserializeXML<T>(this string toDeserialize) {
		XmlSerializer xml = new XmlSerializer (typeof(T));
		StringReader reader = new StringReader (toDeserialize);
		return (T)xml.Deserialize (reader);
	}

    public static T ParseEnum<T>(string value)
    {
        return (T)Enum.Parse(typeof(T), value, true);
    }

    public static string GetBackingFieldName(string propertyName)
    {
        return string.Format("<{0}>k__BackingField", propertyName);
    }

    public static FieldInfo GetBackingField(object obj, string propertyName)
    {
        return obj.GetType().GetField(GetBackingFieldName(propertyName), BindingFlags.Instance | BindingFlags.NonPublic);
    }

    public static BinaryFormatter GetBinaryFormatterWithSurrogates()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        SurrogateSelector surrogateSelector = new SurrogateSelector();
        binaryFormatter.SurrogateSelector = surrogateSelector;

        return binaryFormatter;
    }

    public static Vector2 GetScreenPosFromCamScreenPos(Vector2 position, Camera cam)
    {
        return new Vector2(position.x - cam.rect.x * Screen.width, position.y - cam.rect.y * Screen.height);
    }
}