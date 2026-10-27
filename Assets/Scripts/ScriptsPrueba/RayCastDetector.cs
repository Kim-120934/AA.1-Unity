using UnityEngine;

public class RayCastDetector : MonoBehaviour
{
    public float distance = 5;
    public LayerMask mascara;
    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * distance, Color.red);

        RaycastHit choque;
        //posicion, direccion, informacion de choque, distancia maxima, mascara de colision
        bool haChocado = Physics.Raycast(transform.position, transform.forward, out choque, distance, mascara);

        if(haChocado)
        {
            Debug.Log("Ha chocado con"+ choque.collider.gameObject.name);
            Debug.DrawRay(transform.position, transform.forward * choque.distance, Color.green);
            if (choque.rigidbody != null) ;
            {
                choque.rigidbody.AddForce(transform.forward * 1000);
            }
        }

        else

        { 
          Debug.Log("No ha chocado con nada");
        
        }

    }
}
