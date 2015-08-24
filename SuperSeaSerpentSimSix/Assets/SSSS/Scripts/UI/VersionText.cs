using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VersionText : MonoBehaviour
{
    public const string Version = "v0.198";

	void Start ()
    {
        GetComponent<Text>().text = Version;
	}
}
