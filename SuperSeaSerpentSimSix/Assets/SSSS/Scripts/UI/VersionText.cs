﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VersionText : MonoBehaviour
{
    public const string Version = "v1.000";

	void Start ()
    {
        GetComponent<Text>().text = Version;
	}
}
