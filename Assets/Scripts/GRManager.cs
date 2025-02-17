using Unity.Mathematics;
using UnityEngine;

public class GRManager : MonoBehaviour {
    public int GraveRobs = 2;
    public int LimbSelections = 2;

    public GameObject[] Graves;

    public static GRManager Instance;

    private void Awake() {
        if(Instance == null) Instance = this;
    } 

    private void GenerateGraves() {
        foreach(GameObject g in Graves) {
            // Some logic to decide what limbs go in each grave
        }
    }

    public void HandleGraveClick(Grave grave) {
        if(GraveRobs == 0) {
            Debug.Log("Cannot rob anymore graves!");
        } else {
            GraveRobs--;
            grave.Rob();
        }
    }
}