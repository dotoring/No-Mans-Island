using System.Runtime.CompilerServices;
using UnityEngine;

public class EatingFood : MonoBehaviour
{
    //PlayerClass pc;


    [SerializeField] protected int Meat_f;
    [SerializeField] protected int Meat_r;

    [SerializeField] protected int Roast_f;

    [SerializeField] protected int Frog_f;
    [SerializeField] protected int Frog_r;

    [SerializeField] protected int Insect_f;
    [SerializeField] protected int Insect_r;


    protected int Eaten_f;
    protected int Eaten_r;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Meat"))
        {
            if (other.gameObject.name.Contains("Roast")) Eaten_f = Roast_f;
            else
            {
                Eaten_f = Meat_f;
                Eaten_r = Meat_r;
            }
        }
        else if (other.gameObject.CompareTag("Aniaml"))
        {

        }
    }
}
