using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VersionText : MonoBehaviour
{
    public const string Version = "v0.197";

	void Start ()
    {
        GetComponent<Text>().text = Version;
	}
}
