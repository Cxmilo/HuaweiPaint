using UnityEngine;
using System.Collections;

namespace PaintCraft.Editor
{
	public static class MenuLinks
	{
		[UnityEditor.MenuItem("Window/PaintCraft/Documentation", false, 110)]
		public static void OpenDocsPortal()
		{
			string url= "http://docs.paintcraft.in/";
			Application.OpenURL(url);
		}

		[UnityEditor.MenuItem("Window/PaintCraft/Video Tutorials", false, 120)]
		public static void OpenVideoTutorials()
		{
			string url= "https://www.youtube.com/playlist?list=PLGeCxyvL-w7TeR1wJTirbFSVMXgslN12C";
			Application.OpenURL(url);
		}

		[UnityEditor.MenuItem("Window/PaintCraft/Support/Forum", false, 130)]
		public static void OpenSupportForum()
		{
			string url= "https://forum.unity.com/threads/released-paintcraft-multiplatform-coloring-book-drawing-app-constructor.404998/";
			Application.OpenURL(url);
		}
		
		[UnityEditor.MenuItem("Window/PaintCraft/Support/Mail", false, 140)]
		public static void OpenSupportMail()
		{
			string url= "mailto:support@paintcraft.in";
			Application.OpenURL(url);
		}		
	}
}