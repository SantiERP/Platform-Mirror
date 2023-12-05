using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenGameplay : IScreen
{
    Dictionary<Behaviour, bool> _beforeActivation;
    Dictionary<Rigidbody, object[]> _rigbeforeActivation;

    Transform _root;

    public ScreenGameplay(Transform root)
    {
        _root = root;

        _beforeActivation = new Dictionary<Behaviour, bool>();
        _rigbeforeActivation = new Dictionary<Rigidbody, object[]>();

    }

    public void Activate()
    {
        Mirrors.TimeSpeed = 0.01f;

        foreach (var pair in _beforeActivation)
        {
            if(pair.Key !=null)pair.Key.enabled = pair.Value;
        }
        foreach (var rig in _root.GetComponentsInChildren<Rigidbody>())
        {
            if (_rigbeforeActivation.ContainsKey(rig))
            {                
                rig.constraints = (RigidbodyConstraints)_rigbeforeActivation[rig][1];
                rig.velocity = (Vector3)_rigbeforeActivation[rig][0];
            }
        }
    }

    public void Desactivate()
    {
        Mirrors.TimeSpeed = 0;
        foreach (var behaviour in _root.GetComponentsInChildren<Behaviour>())
        {
            _beforeActivation[behaviour] = behaviour.enabled;

            behaviour.enabled = false;
        }
        foreach (var rig in _root.GetComponentsInChildren<Rigidbody>())
        {
            if (!_rigbeforeActivation.ContainsKey(rig))
            {
                _rigbeforeActivation.Add(rig, new object[2]);
            }

            _rigbeforeActivation[rig][0] = rig.velocity;
            _rigbeforeActivation[rig][1] = rig.constraints;
            rig.constraints = RigidbodyConstraints.FreezeAll;
        }

    }

    public void Free()
    {
        GameObject.Destroy(_root.gameObject);
    }
}
