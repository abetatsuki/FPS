using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

public class CharacterNamePresenter 
{
    public CharacterNamePresenter(CharacterNameView view
        , CharacterModel model,GameObject obj)
    {
        _nameView = view;
        _nameModel = model;
        _obj = obj;
    }
    private CharacterNameView _nameView;
    private GameObject _obj;
    private CharacterModel _nameModel;

    public void SetSubscribe()
    {
        _nameView.OnName
            .Subscribe(inputname =>
            {
                _nameModel.SetName(inputname);
                _nameView.SetDisPlayName(_nameModel.Name);
                _obj.name = inputname;
            })
            .AddTo(_obj);
    }
}
