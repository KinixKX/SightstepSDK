
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

/// <summary>
/// Honestly, this was a re-used pooling script that I repurposed for this game. I would've named the class ArrowPooler or something, but I was too lazy to deal with the fix that this needs.
/// </summary>
public class ObjectPooler : UdonSharpBehaviour
{
    public Arrow[] pooledObjects; //Arrow objects

    //TODO: Once we get a list, we can do a dynamic array that holds only the active arrows, thus saving us some frames instead of having to iterate through the entire array of arrows...
    //cause I can feel the performance hit real bad...

    public int indexLocation = 0; ///This is used to keep track of what arrow needs to be used next
    public int nextArrow; ///This should keep track of the earliest still active arrow on the scene, will be used for the player input checks like we used to, as it's going to iterate 4 spaces back

    public Arrow GetArrow()
    {
        if (indexLocation >= pooledObjects.Length)
        {
            indexLocation = 1;
        }
        else
        {
            indexLocation++;
        }
        return pooledObjects[indexLocation - 1];
    }

    public void DeactiveAll()
    {
        foreach(Arrow a in pooledObjects)
        {
            a.gameObject.SetActive(false);
            a.canBeHit = false;
        }
    }

}