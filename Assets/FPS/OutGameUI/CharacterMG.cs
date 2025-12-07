using UnityEngine;

public class CharacterMG : MonoBehaviour
{
    private CharacterModel _model;
    private CharacterNamePresenter _presenter;
    [SerializeField] private CharacterNameView _view;

    private void Start()
    {
        _model = new CharacterModel();
        _presenter = new CharacterNamePresenter(_view,_model,gameObject);
        _presenter.SetSubscribe();
    }
}
