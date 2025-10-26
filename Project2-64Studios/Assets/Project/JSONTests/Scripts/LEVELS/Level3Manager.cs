using UnityEngine;

public class Level3Manager : Level3Json
{
    [SerializeField] private GameObject Wall1;
    [SerializeField] private GameObject Wall2;
    [SerializeField] private GameObject Wall3;
    [SerializeField] private GameObject Wall4;
    [SerializeField] private GameObject Wall5;

    private void Update()
    {
        jsonContent = new Level3Content(); 
        Debug.Log((jsonContent as Level3Content).FirstWallActive); 
        Wall1.SetActive((jsonContent as Level3Content).FirstWallActive);
        Wall2.SetActive((jsonContent as Level3Content).SecondWallActive);
        Wall3.SetActive((jsonContent as Level3Content).ThirdWallActive);
        Wall4.SetActive((jsonContent as Level3Content).ForthWallActive);
        Wall5.SetActive((jsonContent as Level3Content).FifthWallActive);
    }
}
