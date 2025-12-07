using System;
using TMPro;
using UniRx;
using UnityEngine;

public class CharacterNameView : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private TMP_Text _nameLabel;

    public IObservable<string> OnName => _inputField.onEndEdit.AsObservable();

    public void SetDisPlayName(string name)
    {
        _nameLabel.text = name;
    }
}
