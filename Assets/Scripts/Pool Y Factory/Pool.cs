using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pool<T>
{
    //Guardamos COMO se crea la bullet en este caso (T)
    Func<T> _factoryMethod; 

    //Vamos a guardar en esta lista todos los objetos disponibles para su uso
    List<T> _currentStock;

    //Guardo COMO se prende o apaga la bala una vez se la pase al Factory o el Factory me la devuelva
    Action<T> _turnOnCallback;
    Action<T> _turnOffCallback;

    public Pool(Func<T> factoryMethod, Action<T> turnOnCallback, Action<T> turnOffCallback, int initialAmount)
    {
        //Inicializamos la Lista
        _currentStock = new List<T>();

        //Guardamos el metodo de creacion del objeto
        _factoryMethod = factoryMethod;

        //Guardamos lo que nos paso el Factory
        _turnOnCallback = turnOnCallback;
        _turnOffCallback = turnOffCallback;

        //Guardamos en nuestra lista de objetos a usar la cantidad inicial que nos pasa el Factory
        for (int i = 0; i < initialAmount; i++)
        {
            //Ejecuto y guardo la bala resultante del metodo
            T newObj = _factoryMethod(); 

            //La apago
            _turnOffCallback(newObj);

            //La dejo disponible para usar
            _currentStock.Add(newObj);
        }
    }

    public T GetObject()
    {
        T result;

        //Si mi lista no tiene objetos para prender
        if (_currentStock.Count == 0)
        {
            //Debug.Log("No tiene objetos para prender");
            result = _factoryMethod(); //Creo uno nuevo (va a ser ese el que le voy a dar al cliente)
        }
        else
        {
            //Debug.Log("instancia el objeto");
            result = _currentStock[0]; //Tomo el primero
            _currentStock.RemoveAt(0); //Lo remuevo porque va a estar en uso
        }
        //Debug.Log("prendete");
        _turnOnCallback(result); //Prendo el objeto

        return result; //Se la devuelvo al cliente
    }

    public void ReturnObject(T obj)
    {
        _turnOffCallback(obj); //Apagamos el objeto

        _currentStock.Add(obj); //Lo devolvemos a la lista de objetos preparados
    }
    

}
