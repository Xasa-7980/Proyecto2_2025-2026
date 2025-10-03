
using System;
using System.Linq;
using UnityEngine;

[Serializable]
public struct SceneSettings
{
	[Serializable]
	public struct SceneDialog
	{
		public int logIndex;
		public bool hasSubLogs;
		public StoryTelling sceneLog;
        public StoryTelling[] subLog;
    }
	public int sceneId;
	public string sceneName;
	public int logIndex;
	public int subLogIndex;
	public SceneDialog[] sceneLogs;
	public string SelectedText()
    {
		for (int i = 0; i < sceneLogs.Length; i++)
		{
			if (!sceneLogs[i].hasSubLogs)
			{
				int index = logIndex;
				StoryTelling log = sceneLogs.Where((SceneDialog s) => s.sceneLog != null && s.sceneLog.status && index == s.logIndex).FirstOrDefault().sceneLog;
				log.status = false;
				string text = log.description;
				return text;
			}
			if(sceneLogs[i].subLog.Length < 0)
			{
				return "NoSubText wrong configuration";
			}
			else
			{
                for (int j = 0; j < sceneLogs[i].subLog.Length; j++)
                {
                    StoryTelling log = sceneLogs[i].subLog.Where((StoryTelling s) => s.status).FirstOrDefault();
                    log.status = false;
                    string text = log.description;
					subLogIndex = j;
                    return text;
                }
            }
        }
		return "No se devolvio un dialogo weba";
    }
	public void PlayText()
	{
		//Poner Texto al texto
		Debug.Log(SelectedText());
	}
}