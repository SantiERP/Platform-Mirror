using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pool<T>
{
    //We save How the bullet is created in this case (T)
    Func<T> _factoryMethod; 

    //We are going to save on this list all possible objects for it큦 use
    List<T> _currentStock;

    //We save HOW it turns on or off the Bullet once it큦 passed to the Factory or if the Favtory returns it
    Action<T> _turnOnCallback;
    Action<T> _turnOffCallback;

    public Pool(Func<T> factoryMethod, Action<T> turnOnCallback, Action<T> turnOffCallback, int initialAmount)
    {
        //We initialize the List
        _currentStock = new List<T>();

        //We save the Object큦 creation method
        _factoryMethod = factoryMethod;

        //We save what the Factory returned
        _turnOnCallback = turnOnCallback;
        _turnOffCallback = turnOffCallback;

        //We save in our objects to use list the intitial amount that the Factory returns
        for (int i = 0; i < initialAmount; i++)
        {
            //I execute and save the Bullet
            T newObj = _factoryMethod(); 

            //I Turn it off
            _turnOffCallback(newObj);

            //I make it available for use
            _currentStock.Add(newObj);
        }
    }

    public T GetObject()
    {
        T result;

        //If my List has no objects to turn on
        if (_currentStock.Count == 0)
        {
            result = _factoryMethod(); //I make a new one (Im going to return that one)
        }
        else
        {
            result = _currentStock[0]; //I took the first one
            _currentStock.RemoveAt(0); //I remove it, because it큦 going to be in use
        }

        _turnOnCallback(result); //I turn the object on

        return result; //I return it to the client
    }

    public void ReturnObject(T obj)
    {
        _turnOffCallback(obj); //We turn off the object

        _currentStock.Add(obj); //We return it to the prepared object list
    }
    

}
