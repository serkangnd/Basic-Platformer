using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    private Animator _animator;
    private bool isOpened;
    [SerializeField] private GameObject dropObject;


    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }


    public void OpenChest()
    {
        if (!isOpened)
        {
           _animator.SetBool("isOpen", true);         
        }
        else
        {
            Debug.Log("Chest already opened");
        }
    }


    public void DropItem()
    {
        Instantiate(dropObject, transform.position, Quaternion.identity);
    }

}
