using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "PathPoints")]

public class pathStorage : ScriptableObject {

	public List<Vector3> targetsOnPath;

	private void OnEnable()
	{
			targetsOnPath = targetsOnPath ?? new List<Vector3>();
	}

	public void StoreTarget( Vector3 toStore)
	{
		if(targetsOnPath.Count == 0 || targetsOnPath.Count > 0 && !targetsOnPath.Contains(toStore)){
			targetsOnPath.Add(toStore);
		}
	}

	public Vector3 [] GetDTArray ( bool dirty = false)
	{
		  Vector3 [] temp  = new Vector3[targetsOnPath.Count];
        for ( int i  = 0; i < targetsOnPath.Count; i ++){
					if(dirty){
						temp[i] = targetsOnPath[i];
					} else {
					  temp[i] = new Vector3 (Mathf.Round(targetsOnPath[i].x),Mathf.Round(targetsOnPath[i].y),Mathf.Round(targetsOnPath[i].z));
				  }
				}
				return temp;
	}
}
