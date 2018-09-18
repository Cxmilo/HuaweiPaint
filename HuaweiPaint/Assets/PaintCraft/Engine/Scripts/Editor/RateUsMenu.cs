using UnityEngine;
using System.Collections;
using UnityEditor;


public class RateUsMenu : MonoBehaviour {

	[MenuItem("Window/PaintCraft/RateUs", false, 200)]
	public static void Open() {
		//UnityEditorInternal.AssetStoreToolUtils.PreviewAssetStoreAssetBundleInInspector("u")		
		UnityEditorInternal.AssetStore.Open("content/63046");		
	}
	
}
