using UnityEngine.Audio;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Object Sounds")]
public class ObjectSounds : ScriptableObject
{
    public List<Sound> _objectSounds;
    public Dictionary<string, Sound> _dictSounds = new Dictionary<string, Sound>();

    public void LoadDictionary(GameObject gameObject)
    {
        for (int i = 0; i < _objectSounds.Capacity; i++)
        {
            if (!_dictSounds.ContainsKey(_objectSounds[i].name))
                _dictSounds.Add(_objectSounds[i].name, _objectSounds[i]);
            else
                Debug.Log("<color=yellow>Key already present</color>");
        }

        foreach (Sound s in _dictSounds.Values)
        {
            //Use this function to play a certain sound in script
            //FindObectOfType<AudioManager>().Play("name");
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
}
