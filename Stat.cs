using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
	[SerializeField] private int baseValue;
	private List<int> modifiers = new List<int>();

	public int GetValue()
	{
		int finalValue = baseValue;
		modifiers.ForEach(x => finalValue += x);
		return finalValue;
	}

	public void AddModifier(int modifier)
	{
		if (modifier != 0) modifiers.Add(modifier);
	}

	public void RemoveModifier(int modifier)
	{
		if (modifier != 0) modifiers.Remove(modifier);
	}
}
